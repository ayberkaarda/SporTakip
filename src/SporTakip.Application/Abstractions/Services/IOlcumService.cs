using SporTakip.Application.Validation;
using SporTakip.Domain;

namespace SporTakip.Application.Abstractions.Services;

public interface IOlcumService
{
    Task<IReadOnlyList<Olcum>> TakipOlcumleriAsync(int takipId, CancellationToken ct = default);

    /// <summary>Tür verilirse (tr-TR, büyük/küçük harf duyarsız) yalnızca o türü döndürür.</summary>
    Task<IReadOnlyList<OlcumKaydi>> SporcuOlcumleriAsync(int sporcuId, string? tur = null, CancellationToken ct = default);

    /// <summary>Sporcunun ölçüm türleri; tekrarsız, tr-TR sıralı.</summary>
    Task<IReadOnlyList<string>> TurleriAsync(int sporcuId, CancellationToken ct = default);

    ValidationResult Dogrula(Olcum olcum);

    Task<ValidationResult<int>> EkleAsync(Olcum olcum, CancellationToken ct = default);

    Task<ValidationResult> GuncelleAsync(Olcum olcum, CancellationToken ct = default);

    Task<bool> SilAsync(int id, CancellationToken ct = default);
}
