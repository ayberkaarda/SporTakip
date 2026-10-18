using SporTakip.Application.Reporting;
using SporTakip.Application.Validation;
using SporTakip.Domain;

namespace SporTakip.Application.Abstractions.Services;

public interface IAntrenmanService
{
    Task<IReadOnlyList<Antrenman>> SporcuyaGoreListeleAsync(int sporcuId, CancellationToken cancellationToken = default);

    /// <summary>Pazartesi-Pazar 7 giriş; atanmamışlar varsa Gun=0 'Atanmamış' girişi sonda.</summary>
    Task<IReadOnlyList<HaftalikProgramGunu>> HaftalikProgramAsync(int sporcuId, CancellationToken cancellationToken = default);

    ValidationResult Dogrula(Antrenman antrenman);

    /// <returns>Geçerliyse eklenen kaydın Id'si.</returns>
    Task<ValidationResult<int>> EkleAsync(Antrenman antrenman, CancellationToken cancellationToken = default);

    Task<ValidationResult> GuncelleAsync(Antrenman antrenman, CancellationToken cancellationToken = default);

    Task<bool> SilAsync(int id, CancellationToken cancellationToken = default);
}
