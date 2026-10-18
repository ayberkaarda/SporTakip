namespace SporTakip.Domain;

/// <summary>Bir takip kaydına bağlı yapılandırılmış ölçüm (ör. Kilo, 72.50, kg).</summary>
public class Olcum
{
    public int Id { get; set; }
    public int TakipId { get; set; }
    public string Tur { get; set; } = string.Empty;
    public decimal Deger { get; set; }
    public string? Birim { get; set; }
}
