namespace SporTakip.Domain;

/// <summary>Antrenmanlar tablosundaki, bir sporcuya ait antrenman programı.</summary>
public class Antrenman
{
    public int Id { get; set; }
    public int SporcuId { get; set; }
    public string? AntrenmanAdi { get; set; }
    public string? Detaylar { get; set; }

    /// <summary>Haftanın günü: 1=Pazartesi ... 7=Pazar, null=atanmamış.</summary>
    public int? Gun { get; set; }
}
