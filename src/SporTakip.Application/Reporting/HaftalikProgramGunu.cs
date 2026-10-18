using SporTakip.Domain;

namespace SporTakip.Application.Reporting;

/// <summary>Haftalık programın bir günü. Gun=0 atanmamış antrenmanları temsil eder.</summary>
public sealed record HaftalikProgramGunu(int Gun, string GunAdi, IReadOnlyList<Antrenman> Antrenmanlar);
