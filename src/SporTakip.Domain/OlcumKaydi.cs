namespace SporTakip.Domain;

/// <summary>Ölçümün bağlı olduğu takip kaydının tarihiyle birlikte düz görünümü.</summary>
public sealed record OlcumKaydi(int Id, int TakipId, DateTime Tarih, string Tur, decimal Deger, string? Birim);
