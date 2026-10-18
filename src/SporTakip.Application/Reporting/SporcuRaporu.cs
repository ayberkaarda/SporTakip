using SporTakip.Domain;

namespace SporTakip.Application.Reporting;

/// <summary>Bir sporcunun antrenman ve takip geçmişi (eski Sporcu.RaporAl() karşılığı).</summary>
public sealed record SporcuRaporu(
    Sporcu Sporcu,
    IReadOnlyList<Antrenman> Antrenmanlar,
    IReadOnlyList<Takip> Takipler);
