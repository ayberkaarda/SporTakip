using SporTakip.Domain;

namespace SporTakip.Screenshots;

/// <summary>Invented athletes and records for the README screenshots; none of them are real people.</summary>
internal static class SampleData
{
    /// <summary>The athlete shown on the detail, weekly program and chart screens.</summary>
    public const int VitrinSporcuId = 4;

    public static SampleStore Olustur()
    {
        var store = new SampleStore();
        store.Sporcular.AddRange(
        [
            new Sporcu { Id = 1, Ad = "Burak Yalçın", SporDali = "Basketbol" },
            new Sporcu { Id = 2, Ad = "Deniz Aksoy", SporDali = "Yüzme" },
            new Sporcu { Id = 3, Ad = "Ece Tunç", SporDali = "Atletizm" },
            new Sporcu { Id = 4, Ad = "Kaan Yıldız", SporDali = "Fitness" },
            new Sporcu { Id = 5, Ad = "Melis Kara", SporDali = "Tenis" },
            new Sporcu { Id = 6, Ad = "Okan Çelik", SporDali = "Bisiklet" },
            new Sporcu { Id = 7, Ad = "Selin Erdem", SporDali = "Voleybol" },
            new Sporcu { Id = 8, Ad = "Zeynep Arslan", SporDali = "Okçuluk" },
        ]);

        int antrenmanId = 0;
        void Antrenman(int sporcuId, string ad, string detay, int? gun) =>
            store.Antrenmanlar.Add(new Antrenman { Id = ++antrenmanId, SporcuId = sporcuId, AntrenmanAdi = ad, Detaylar = detay, Gun = gun });

        int k = VitrinSporcuId;
        Antrenman(k, "Göğüs ve Triceps", "Bench press 4x8, dips 3x12", 1);
        Antrenman(k, "Tempo koşusu", "35 dk, nabız 150-160", 2);
        Antrenman(k, "Sırt ve Biceps", "Barfiks 4x6, kürek çekiş 4x10", 3);
        Antrenman(k, "Core", "Plank 3x60 sn, dead bug 3x12", 3);
        Antrenman(k, "Bacak", "Squat 5x5, leg press 3x12", 4);
        Antrenman(k, "Omuz", "Overhead press 4x8, yan açış 3x15", 5);
        Antrenman(k, "Bisiklet", "10 tur: 1 dk hızlı, 1 dk yavaş", 5);
        Antrenman(k, "Doğa yürüyüşü", "90 dk, tempolu", 6);
        Antrenman(k, "Mobilite", "15 dk esneme ve köpük rulo", null);
        Antrenman(2, "Teknik", "8x50 m serbest stil", 2);
        Antrenman(3, "Sprint", "6x60 m, tam dinlenme", 1);

        int takipId = 0, olcumId = 0;
        void Takip(int sporcuId, DateTime tarih, string aciklama, params (string Tur, decimal Deger, string Birim)[] olcumler)
        {
            int id = ++takipId;
            store.Takipler.Add(new Takip { Id = id, SporcuId = sporcuId, Tarih = tarih, Aciklama = aciklama });
            foreach (var (tur, deger, birim) in olcumler)
                store.Olcumler.Add(new Olcum { Id = ++olcumId, TakipId = id, Tur = tur, Deger = deger, Birim = birim });
        }

        Takip(k, new DateTime(2026, 9, 1), "Başlangıç ölçümü", ("Kilo", 86.4m, "kg"), ("Bel", 94m, "cm"));
        Takip(k, new DateTime(2026, 9, 11), "Program başladı", ("Kilo", 85.9m, "kg"));
        Takip(k, new DateTime(2026, 9, 21), "Squat 90 kg x 5", ("Kilo", 85.1m, "kg"));
        Takip(k, new DateTime(2026, 10, 1), "Kardiyo artırıldı", ("Kilo", 84.3m, "kg"), ("Bel", 92m, "cm"));
        Takip(k, new DateTime(2026, 10, 11), "Hafif sakatlık", ("Kilo", 84.6m, "kg"));
        Takip(k, new DateTime(2026, 10, 21), "Bench 80 kg x 8", ("Kilo", 83.7m, "kg"));
        Takip(k, new DateTime(2026, 10, 31), "Setler tamamlandı", ("Kilo", 83.1m, "kg"), ("Bel", 90m, "cm"));
        Takip(k, new DateTime(2026, 11, 10), "Aylık değerlendirme", ("Kilo", 82.4m, "kg"), ("Bel", 89m, "cm"));
        Takip(2, new DateTime(2026, 11, 3), "100 m: 1:12", ("Süre", 72m, "sn"));
        return store;
    }
}
