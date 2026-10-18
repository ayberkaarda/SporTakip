using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Application.Validation;
using System.Globalization;
using SporTakip.Domain;

namespace SporTakip.Application.Services;

public sealed class SporcuService : ISporcuService
{
    private readonly ISporcuRepository _repository;

    public SporcuService(ISporcuRepository repository) => _repository = repository;

    public Task<IReadOnlyList<Sporcu>> ListeleAsync(CancellationToken cancellationToken = default)
        => _repository.GetAllAsync(cancellationToken);

    private static readonly CultureInfo Tr = CultureInfo.GetCultureInfo("tr-TR");

    public async Task<IReadOnlyList<Sporcu>> AraAsync(string? arama, CancellationToken cancellationToken = default)
    {
        var tumu = await _repository.GetAllAsync(cancellationToken);
        var terim = arama?.Trim();
        IEnumerable<Sporcu> sonuc = tumu;
        if (!string.IsNullOrEmpty(terim))
            sonuc = sonuc.Where(s => Icerir(s.Ad, terim) || Icerir(s.SporDali, terim));

        return sonuc
            .OrderBy(s => s.Ad ?? string.Empty, StringComparer.Create(Tr, ignoreCase: true))
            .ToList();
    }

    private static bool Icerir(string? metin, string terim)
        => !string.IsNullOrEmpty(metin) && Tr.CompareInfo.IndexOf(metin, terim, CompareOptions.IgnoreCase) >= 0;

    public Task<Sporcu?> GetirAsync(int id, CancellationToken cancellationToken = default)
        => _repository.GetByIdAsync(id, cancellationToken);

    public ValidationResult Dogrula(Sporcu sporcu)
    {
        ArgumentNullException.ThrowIfNull(sporcu);
        var errors = new List<ValidationError>();

        var ad = sporcu.Ad?.Trim();
        if (string.IsNullOrEmpty(ad))
            errors.Add(new(nameof(Sporcu.Ad), "Sporcu adı boş olamaz."));
        else if (ad.Length > ValidationConstants.SporcuAdMaxLength)
            errors.Add(new(nameof(Sporcu.Ad), $"Sporcu adı en fazla {ValidationConstants.SporcuAdMaxLength} karakter olabilir."));

        var dal = sporcu.SporDali?.Trim();
        if (string.IsNullOrEmpty(dal))
            errors.Add(new(nameof(Sporcu.SporDali), "Spor dalı boş olamaz."));
        else if (dal.Length > ValidationConstants.SporDaliMaxLength)
            errors.Add(new(nameof(Sporcu.SporDali), $"Spor dalı en fazla {ValidationConstants.SporDaliMaxLength} karakter olabilir."));

        return errors.Count == 0 ? ValidationResult.Success : ValidationResult.Failure(errors);
    }

    public async Task<ValidationResult<int>> EkleAsync(Sporcu sporcu, CancellationToken cancellationToken = default)
    {
        var sonuc = Dogrula(sporcu);
        if (!sonuc.IsValid) return ValidationResult<int>.Failure(sonuc.Errors);

        Temizle(sporcu);
        var id = await _repository.AddAsync(sporcu, cancellationToken);
        return ValidationResult<int>.Ok(id);
    }

    public async Task<ValidationResult> GuncelleAsync(Sporcu sporcu, CancellationToken cancellationToken = default)
    {
        var sonuc = Dogrula(sporcu);
        if (!sonuc.IsValid) return sonuc;

        Temizle(sporcu);
        var guncellendi = await _repository.UpdateAsync(sporcu, cancellationToken);
        return guncellendi
            ? ValidationResult.Success
            : ValidationResult.Failure(new ValidationError(nameof(Sporcu.Id), "Sporcu bulunamadı."));
    }

    public Task<bool> SilAsync(int id, CancellationToken cancellationToken = default)
        => _repository.SporcuyuTumuyleSilAsync(id, cancellationToken);

    private static void Temizle(Sporcu sporcu)
    {
        sporcu.Ad = sporcu.Ad.Trim();
        sporcu.SporDali = sporcu.SporDali?.Trim();
    }
}
