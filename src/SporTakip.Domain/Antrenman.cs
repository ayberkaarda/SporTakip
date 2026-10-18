namespace SporTakip.Domain;

/// <summary>Antrenmanlar tablosundaki, bir sporcuya ait antrenman programı.</summary>
public class Antrenman
{
    public int Id { get; set; }
    public int SporcuId { get; set; }
    public string? AntrenmanAdi { get; set; }
    public string? Detaylar { get; set; }
}
