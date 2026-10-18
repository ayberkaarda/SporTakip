using System.Globalization;
using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Application.Validation;
using SporTakip.Domain;

namespace SporTakip.Application.Services;

public sealed class OlcumService : IOlcumService
{
    private const decimal MaxDeger = 9999999.99m;
    private static readonly CultureInfo Tr = CultureInfo.GetCultureInfo("tr-TR");
    private static readonly StringComparer TrIgnoreCase = StringComparer.Create(Tr, ignoreCase: true);

    private readonly IOlcumRepository _repository;
    private readonly ITakipRepository _takipRepository;

    public OlcumService(IOlcumRepository repository, ITakipRepository takipRepository)
    {
        _repository = repository;
        _takipRepository = takipRepository;
    }

    public Task<IReadOnlyList<Olcum>> TakipOlcumleriAsync(int takipId, CancellationToken ct = default)
        => _repository.ListByTakipIdAsync(takipId, ct);

    public async Task<IReadOnlyList<OlcumKaydi>> SporcuOlcumleriAsync(int sporcuId, string? tur = null, CancellationToken ct = default)
    {
        var liste = await _repository.ListBySporcuIdAsync(sporcuId, ct);
        var aranan = tur?.Trim();
        if (string.IsNullOrEmpty(aranan)) return liste;
        return liste.Where(o => TrIgnoreCase.Equals(o.Tur, aranan)).ToList();
    }

    public async Task<IReadOnlyList<string>> TurleriAsync(int sporcuId, CancellationToken ct = default)
    {
        var liste = await _repository.ListBySporcuIdAsync(sporcuId, ct);
        return liste.Select(o => o.Tur)
            .Distinct(TrIgnoreCase)
            .OrderBy(t => t, StringComparer.Create(Tr, ignoreCase: false))
            .ToList();
    }

    public ValidationResult Dogrula(Olcum olcum)
    {
        ArgumentNullException.ThrowIfNull(olcum);
        var errors = new List<ValidationError>();

        var tur = olcum.Tur?.Trim();
        if (string.IsNullOrEmpty(tur))
            errors.Add(new(nameof(Olcum.Tur), "Ölçüm türü boş olamaz."));
        else if (tur.Length > ValidationConstants.TurMaxLength)
            errors.Add(new(nameof(Olcum.Tur), $"Ölçüm türü en fazla {ValidationConstants.TurMaxLength} karakter olabilir."));

        if (Math.Abs(Yuvarla(olcum.Deger)) > MaxDeger)
            errors.Add(new(nameof(Olcum.Deger), "Değer 9999999,99 sınırını aşamaz."));

        var birim = olcum.Birim?.Trim();
        if (birim is not null && birim.Length > ValidationConstants.BirimMaxLength)
            errors.Add(new(nameof(Olcum.Birim), $"Birim en fazla {ValidationConstants.BirimMaxLength} karakter olabilir."));

        return errors.Count == 0 ? ValidationResult.Success : ValidationResult.Failure(errors);
    }

    public async Task<ValidationResult<int>> EkleAsync(Olcum olcum, CancellationToken ct = default)
    {
        var sonuc = await DogrulaTamAsync(olcum, ct);
        if (!sonuc.IsValid) return ValidationResult<int>.Failure(sonuc.Errors);

        Normalize(olcum);
        var id = await _repository.AddAsync(olcum, ct);
        return ValidationResult<int>.Ok(id);
    }

    public async Task<ValidationResult> GuncelleAsync(Olcum olcum, CancellationToken ct = default)
    {
        var sonuc = await DogrulaTamAsync(olcum, ct);
        if (!sonuc.IsValid) return sonuc;

        Normalize(olcum);
        var guncellendi = await _repository.UpdateAsync(olcum, ct);
        return guncellendi
            ? ValidationResult.Success
            : ValidationResult.Failure(new ValidationError(nameof(Olcum.Id), "Ölçüm kaydı bulunamadı."));
    }

    public Task<bool> SilAsync(int id, CancellationToken ct = default)
        => _repository.DeleteAsync(id, ct);

    private static decimal Yuvarla(decimal deger) => Math.Round(deger, 2, MidpointRounding.AwayFromZero);

    private static void Normalize(Olcum olcum)
    {
        olcum.Tur = olcum.Tur.Trim();
        olcum.Deger = Yuvarla(olcum.Deger);
        olcum.Birim = string.IsNullOrWhiteSpace(olcum.Birim) ? null : olcum.Birim.Trim();
    }

    private async Task<ValidationResult> DogrulaTamAsync(Olcum olcum, CancellationToken ct)
    {
        var errors = Dogrula(olcum).Errors.ToList();
        if (await _takipRepository.GetByIdAsync(olcum.TakipId, ct) is null)
            errors.Insert(0, new(nameof(Olcum.TakipId), "Takip kaydı bulunamadı."));
        return errors.Count == 0 ? ValidationResult.Success : ValidationResult.Failure(errors);
    }
}
