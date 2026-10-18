using SporTakip.Application.Validation;
using SporTakip.Domain;

namespace SporTakip.Application.Abstractions.Services;

public interface ISporcuService
{
    Task<IReadOnlyList<Sporcu>> ListeleAsync(CancellationToken cancellationToken = default);

    /// <summary>Ad veya spor dalında (tr-TR, büyük/küçük harf duyarsız) arar; boşsa tümü. Ad'a göre sıralı.</summary>
    Task<IReadOnlyList<Sporcu>> AraAsync(string? arama, CancellationToken cancellationToken = default);

    Task<Sporcu?> GetirAsync(int id, CancellationToken cancellationToken = default);

    ValidationResult Dogrula(Sporcu sporcu);

    /// <returns>Geçerliyse eklenen sporcunun Id'si.</returns>
    Task<ValidationResult<int>> EkleAsync(Sporcu sporcu, CancellationToken cancellationToken = default);

    Task<ValidationResult> GuncelleAsync(Sporcu sporcu, CancellationToken cancellationToken = default);

    /// <summary>Sporcuyu tüm antrenman ve takip kayıtlarıyla birlikte siler.</summary>
    Task<bool> SilAsync(int id, CancellationToken cancellationToken = default);
}
