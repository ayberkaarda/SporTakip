namespace SporTakip.Domain;

/// <summary>Takipler tablosundaki, bir sporcuya ait tarihli ilerleme kaydı.</summary>
public class Takip
{
    public int Id { get; set; }
    public int SporcuId { get; set; }
    public DateTime Tarih { get; set; }
    public string? Aciklama { get; set; }
}
