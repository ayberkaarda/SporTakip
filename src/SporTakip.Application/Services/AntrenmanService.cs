using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Application.Abstractions.Services;
using System.Globalization;
using SporTakip.Application.Reporting;
using SporTakip.Application.Validation;
using SporTakip.Domain;

namespace SporTakip.Application.Services;

public sealed class AntrenmanService : IAntrenmanService
{
    private readonly IAntrenmanRepository _repository;
    private readonly ISporcuRepository _sporcuRepository;

    public AntrenmanService(IAntrenmanRepository repository, ISporcuRepository sporcuRepository)
    {
        _repository = repository;
        _sporcuRepository = sporcuRepository;
    }

    public Task<IReadOnlyList<Antrenman>> SporcuyaGoreListeleAsync(int sporcuId, CancellationToken cancellationToken = default)
        => _repository.ListBySporcuIdAsync(sporcuId, cancellationToken);

    private static readonly string[] GunAdlari =
        ["Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar"];

    public async Task<IReadOnlyList<HaftalikProgramGunu>> HaftalikProgramAsync(int sporcuId, CancellationToken cancellationToken = default)
    {
        var liste = await _repository.ListBySporcuIdAsync(sporcuId, cancellationToken);
        var karsilastirici = StringComparer.Create(new CultureInfo("tr-TR"), ignoreCase: true);

        IReadOnlyList<Antrenman> Sirala(IEnumerable<Antrenman> kaynak)
            => kaynak.OrderBy(a => a.AntrenmanAdi ?? string.Empty, karsilastirici).ThenBy(a => a.Id).ToList();

        var sonuc = new List<HaftalikProgramGunu>();
        for (var gun = 1; gun <= 7; gun++)
            sonuc.Add(new HaftalikProgramGunu(gun, GunAdlari[gun - 1], Sirala(liste.Where(a => a.Gun == gun))));

        var atanmamis = liste.Where(a => a.Gun is null or < 1 or > 7).ToList();
        if (atanmamis.Count > 0)
            sonuc.Add(new HaftalikProgramGunu(0, "Atanmamış", Sirala(atanmamis)));
        return sonuc;
    }

    public ValidationResult Dogrula(Antrenman antrenman)
    {
        ArgumentNullException.ThrowIfNull(antrenman);
        var errors = new List<ValidationError>();

        var adi = antrenman.AntrenmanAdi?.Trim();
        if (string.IsNullOrEmpty(adi))
            errors.Add(new(nameof(Antrenman.AntrenmanAdi), "Antrenman adı boş olamaz."));
        else if (adi.Length > ValidationConstants.AntrenmanAdiMaxLength)
            errors.Add(new(nameof(Antrenman.AntrenmanAdi), $"Antrenman adı en fazla {ValidationConstants.AntrenmanAdiMaxLength} karakter olabilir."));

        if (antrenman.Gun is < 1 or > 7)
            errors.Add(new(nameof(Antrenman.Gun), "Gün 1 ile 7 arasında olmalıdır."));

        return errors.Count == 0 ? ValidationResult.Success : ValidationResult.Failure(errors);
    }

    public async Task<ValidationResult<int>> EkleAsync(Antrenman antrenman, CancellationToken cancellationToken = default)
    {
        var sonuc = await DogrulaTamAsync(antrenman, cancellationToken);
        if (!sonuc.IsValid) return ValidationResult<int>.Failure(sonuc.Errors);

        Temizle(antrenman);
        var id = await _repository.AddAsync(antrenman, cancellationToken);
        return ValidationResult<int>.Ok(id);
    }

    public async Task<ValidationResult> GuncelleAsync(Antrenman antrenman, CancellationToken cancellationToken = default)
    {
        var sonuc = await DogrulaTamAsync(antrenman, cancellationToken);
        if (!sonuc.IsValid) return sonuc;

        Temizle(antrenman);
        var guncellendi = await _repository.UpdateAsync(antrenman, cancellationToken);
        return guncellendi
            ? ValidationResult.Success
            : ValidationResult.Failure(new ValidationError(nameof(Antrenman.Id), "Antrenman bulunamadı."));
    }

    public Task<bool> SilAsync(int id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private async Task<ValidationResult> DogrulaTamAsync(Antrenman antrenman, CancellationToken cancellationToken)
    {
        var errors = Dogrula(antrenman).Errors.ToList();
        if (await _sporcuRepository.GetByIdAsync(antrenman.SporcuId, cancellationToken) is null)
            errors.Insert(0, new(nameof(Antrenman.SporcuId), "Sporcu bulunamadı."));
        return errors.Count == 0 ? ValidationResult.Success : ValidationResult.Failure(errors);
    }

    private static void Temizle(Antrenman antrenman)
    {
        antrenman.AntrenmanAdi = antrenman.AntrenmanAdi?.Trim();
        antrenman.Detaylar = string.IsNullOrWhiteSpace(antrenman.Detaylar) ? null : antrenman.Detaylar.Trim();
    }
}
