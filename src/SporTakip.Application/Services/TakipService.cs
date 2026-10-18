using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Application.Validation;
using SporTakip.Domain;

namespace SporTakip.Application.Services;

public sealed class TakipService : ITakipService
{
    private static readonly DateTime MinTarih = new(1753, 1, 1);

    private readonly ITakipRepository _repository;
    private readonly ISporcuRepository _sporcuRepository;

    public TakipService(ITakipRepository repository, ISporcuRepository sporcuRepository)
    {
        _repository = repository;
        _sporcuRepository = sporcuRepository;
    }

    public Task<IReadOnlyList<Takip>> SporcuyaGoreListeleAsync(int sporcuId, CancellationToken cancellationToken = default)
        => _repository.ListBySporcuIdAsync(sporcuId, cancellationToken);

    public ValidationResult Dogrula(Takip takip)
    {
        ArgumentNullException.ThrowIfNull(takip);
        var errors = new List<ValidationError>();

        if (takip.Tarih < MinTarih)
            errors.Add(new(nameof(Takip.Tarih), "Tarih 1753 öncesi olamaz."));
        else if (takip.Tarih.Date > DateTime.Today)
            errors.Add(new(nameof(Takip.Tarih), "Tarih bugünden ileri olamaz."));

        if (string.IsNullOrWhiteSpace(takip.Aciklama))
            errors.Add(new(nameof(Takip.Aciklama), "Açıklama boş olamaz."));

        return errors.Count == 0 ? ValidationResult.Success : ValidationResult.Failure(errors);
    }

    public async Task<ValidationResult<int>> EkleAsync(Takip takip, CancellationToken cancellationToken = default)
    {
        var sonuc = await DogrulaTamAsync(takip, cancellationToken);
        if (!sonuc.IsValid) return ValidationResult<int>.Failure(sonuc.Errors);

        takip.Aciklama = takip.Aciklama!.Trim();
        var id = await _repository.AddAsync(takip, cancellationToken);
        return ValidationResult<int>.Ok(id);
    }

    public async Task<ValidationResult> GuncelleAsync(Takip takip, CancellationToken cancellationToken = default)
    {
        var sonuc = await DogrulaTamAsync(takip, cancellationToken);
        if (!sonuc.IsValid) return sonuc;

        takip.Aciklama = takip.Aciklama!.Trim();
        var guncellendi = await _repository.UpdateAsync(takip, cancellationToken);
        return guncellendi
            ? ValidationResult.Success
            : ValidationResult.Failure(new ValidationError(nameof(Takip.Id), "Takip kaydı bulunamadı."));
    }

    public Task<bool> SilAsync(int id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private async Task<ValidationResult> DogrulaTamAsync(Takip takip, CancellationToken cancellationToken)
    {
        var errors = Dogrula(takip).Errors.ToList();
        if (await _sporcuRepository.GetByIdAsync(takip.SporcuId, cancellationToken) is null)
            errors.Insert(0, new(nameof(Takip.SporcuId), "Sporcu bulunamadı."));
        return errors.Count == 0 ? ValidationResult.Success : ValidationResult.Failure(errors);
    }
}
