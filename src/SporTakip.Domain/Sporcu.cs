namespace SporTakip.Domain;

/// <summary>Sporcular tablosundaki bir sporcu.</summary>
public class Sporcu
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public string? SporDali { get; set; }
}
