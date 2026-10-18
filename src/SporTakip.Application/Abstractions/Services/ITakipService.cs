using SporTakip.Application.Validation;
using SporTakip.Domain;

namespace SporTakip.Application.Abstractions.Services;

public interface ITakipService
{
    /// <summary>Tarihe göre azalan sırada.</summary>
    Task<IReadOnlyList<Takip>> SporcuyaGoreListeleAsync(int sporcuId, CancellationToken cancellationToken = default);

    ValidationResult Dogrula(Takip takip);

    /// <returns>Geçerliyse eklenen kaydın Id'si.</returns>
    Task<ValidationResult<int>> EkleAsync(Takip takip, CancellationToken cancellationToken = default);

    Task<ValidationResult> GuncelleAsync(Takip takip, CancellationToken cancellationToken = default);

    Task<bool> SilAsync(int id, CancellationToken cancellationToken = default);
}
