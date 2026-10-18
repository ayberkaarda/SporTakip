namespace SporTakip.Application.Validation;

/// <summary>
/// Metin alanlarının azami uzunlukları. database/001_initial.sql ile aynı olmalı;
/// NVARCHAR(MAX) kolonların (Antrenmanlar.Detaylar, Takipler.Aciklama) uzunluk sınırı yoktur.
/// </summary>
public static class ValidationConstants
{
    /// <summary>Sporcular.Ad NVARCHAR(100) NOT NULL</summary>
    public const int SporcuAdMaxLength = 100;

    /// <summary>Sporcular.SporDali NVARCHAR(100) NULL</summary>
    public const int SporDaliMaxLength = 100;

    /// <summary>Antrenmanlar.AntrenmanAdi NVARCHAR(100) NULL</summary>
    public const int AntrenmanAdiMaxLength = 100;

    /// <summary>Olcumler.Tur NVARCHAR(50) NOT NULL</summary>
    public const int TurMaxLength = 50;

    /// <summary>Olcumler.Birim NVARCHAR(20) NULL</summary>
    public const int BirimMaxLength = 20;
}
