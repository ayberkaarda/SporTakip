using System.Drawing;

namespace SporTakip.Theme
{
    /// <summary>
    /// Tipografi token'ları. Gövde boyutu formların AutoScaleDimensions (7x17) değerinin dayandığı
    /// Segoe UI 9.75 pt'dir; değiştirilirse tüm formların ölçeklemesi değişir.
    /// </summary>
    public static class ThemeTypography
    {
        public const string Aile = "Segoe UI";
        public const string AileSeminkalin = "Segoe UI Semibold";
        /// <summary>Pencere düğmesi simgeleri (Windows 10 ve sonrası).</summary>
        public const string SimgeAilesi = "Segoe MDL2 Assets";

        public const float BaslikBoyut = 14F;
        public const float BolumBoyut = 11F;
        public const float GovdeBoyut = 9.75F;
        public const float KucukBoyut = 8.75F;
        public const float SimgeBoyut = 9F;

        // Paylaşılan örnekler: formlar dispose etmez; uygulama boyunca yaşar.
        public static Font Baslik { get; } = new Font(AileSeminkalin, BaslikBoyut, FontStyle.Regular, GraphicsUnit.Point);
        public static Font Bolum { get; } = new Font(AileSeminkalin, BolumBoyut, FontStyle.Regular, GraphicsUnit.Point);
        public static Font Govde { get; } = new Font(Aile, GovdeBoyut, FontStyle.Regular, GraphicsUnit.Point);
        public static Font GovdeVurgulu { get; } = new Font(AileSeminkalin, GovdeBoyut, FontStyle.Regular, GraphicsUnit.Point);
        public static Font Kucuk { get; } = new Font(Aile, KucukBoyut, FontStyle.Regular, GraphicsUnit.Point);
        public static Font KucukVurgulu { get; } = new Font(AileSeminkalin, KucukBoyut, FontStyle.Regular, GraphicsUnit.Point);
        public static Font Bos { get; } = new Font(Aile, BolumBoyut, FontStyle.Regular, GraphicsUnit.Point);
        public static Font Simge { get; } = new Font(SimgeAilesi, SimgeBoyut, FontStyle.Regular, GraphicsUnit.Point);
    }

    /// <summary>Aralık, köşe yarıçapı ve gölge ölçekleri (mantıksal piksel; çizimde DPI'a çevrilir).</summary>
    public static class ThemeScale
    {
        public const int AralikXS = 4;
        public const int AralikS = 8;
        public const int AralikM = 12;
        public const int AralikL = 16;
        public const int AralikXL = 24;

        public const int KoseKucuk = 4;
        public const int KoseOrta = 6;
        public const int KoseBuyuk = 10;

        /// <summary>Kart gölgesinin aşağı kayması ve yayılımı.</summary>
        public const int GolgeKayma = 1;
        public const int GolgeYayilim = 3;

        public const int OdakHalkasi = 2;
    }
}
