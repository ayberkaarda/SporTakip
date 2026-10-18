using SporTakip.Application.Validation;
using SporTakip.Domain;

namespace SporTakip.Application.Abstractions.Services;

public interface IAntrenmanService
{
    Task<IReadOnlyList<Antrenman>> SporcuyaGoreListeleAsync(int sporcuId, CancellationToken cancellationToken = default);

    ValidationResult Dogrula(Antrenman antrenman);

    /// <returns>Geçerliyse eklenen kaydın Id'si.</returns>
    Task<ValidationResult<int>> EkleAsync(Antrenman antrenman, CancellationToken cancellationToken = default);

    Task<ValidationResult> GuncelleAsync(Antrenman antrenman, CancellationToken cancellationToken = default);

    Task<bool> SilAsync(int id, CancellationToken cancellationToken = default);
}
