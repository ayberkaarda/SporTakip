using System.Drawing;

namespace SporTakip.Theme
{
    /// <summary>
    /// Renk token'ları. Formlar renkleri doğrudan değil, yalnızca bu token'lar üzerinden kullanır.
    /// Açık ve Koyu paletler ayrı ayrı seçildi; biri diğerinin tersi değildir.
    /// </summary>
    public sealed class ThemePalette
    {
        public string Ad { get; init; }
        public bool Koyu { get; init; }

        // Zeminler
        public Color Background { get; init; }
        public Color Surface { get; init; }
        public Color SurfaceAlt { get; init; }
        public Color Input { get; init; }
        public Color Border { get; init; }
        public Color BorderStrong { get; init; }

        // Metin
        public Color TextPrimary { get; init; }
        public Color TextSecondary { get; init; }
        public Color TextDisabled { get; init; }

        // Vurgu (tek vurgu rengi)
        public Color Accent { get; init; }
        public Color AccentHover { get; init; }
        public Color AccentPressed { get; init; }
        public Color OnAccent { get; init; }
        public Color AccentSubtle { get; init; }

        // Durum
        public Color Success { get; init; }
        public Color Warning { get; init; }
        public Color Error { get; init; }
        public Color ErrorSubtle { get; init; }

        // Tablo ve liste
        public Color SelectionBack { get; init; }
        public Color SelectionText { get; init; }
        public Color TableHeaderBack { get; init; }
        public Color TableHeaderText { get; init; }
        public Color TableZebra { get; init; }

        // Pencere düğmeleri ve gölge
        public Color TitleButtonHover { get; init; }
        public Color CloseHover { get; init; }
        public Color OnClose { get; init; }
        public Color Shadow { get; init; }

        private static Color Hex(string hex) => ColorTranslator.FromHtml(hex);

        /// <summary>Sakin, yüksek kontrastlı açık palet: nötr gri-beyaz yüzeyler ve tek vurgu olarak zümrüt.</summary>
        public static ThemePalette Acik { get; } = new ThemePalette
        {
            Ad = "Açık",
            Koyu = false,
            Background = Hex("#F3F4F6"),
            Surface = Hex("#FFFFFF"),
            SurfaceAlt = Hex("#EEF0F3"),
            Input = Hex("#FFFFFF"),
            Border = Hex("#DCE0E5"),
            BorderStrong = Hex("#B9C0C9"),
            TextPrimary = Hex("#1B1F24"),
            TextSecondary = Hex("#565E69"),
            TextDisabled = Hex("#8E959F"),
            Accent = Hex("#047857"),
            AccentHover = Hex("#065F46"),
            AccentPressed = Hex("#064E3B"),
            OnAccent = Hex("#FFFFFF"),
            AccentSubtle = Hex("#E2F3EB"),
            Success = Hex("#15803D"),
            Warning = Hex("#A15C07"),
            Error = Hex("#B42318"),
            ErrorSubtle = Hex("#FDEDEB"),
            SelectionBack = Hex("#D4EFE2"),
            SelectionText = Hex("#0B3B2B"),
            TableHeaderBack = Hex("#F3F4F6"),
            TableHeaderText = Hex("#3A414A"),
            TableZebra = Hex("#FAFBFC"),
            TitleButtonHover = Hex("#E5E8EC"),
            CloseHover = Hex("#C42B1C"),
            OnClose = Hex("#FFFFFF"),
            Shadow = Color.FromArgb(22, 16, 24, 40),
        };

        /// <summary>
        /// Koyu palet: grafit yüzeyler ve koyu zeminde rahat okunan nane yeşili vurgu.
        /// Vurgu üzerindeki metin koyudur (beyaz metin bu tonda yeterli kontrast vermez).
        /// </summary>
        public static ThemePalette KoyuPalet { get; } = new ThemePalette
        {
            Ad = "Koyu",
            Koyu = true,
            Background = Hex("#111316"),
            Surface = Hex("#1A1D21"),
            SurfaceAlt = Hex("#23272C"),
            Input = Hex("#202428"),
            Border = Hex("#2E3339"),
            BorderStrong = Hex("#454C55"),
            TextPrimary = Hex("#ECEEF1"),
            TextSecondary = Hex("#A3ABB5"),
            TextDisabled = Hex("#6B727B"),
            Accent = Hex("#3DD68C"),
            AccentHover = Hex("#62E2A4"),
            AccentPressed = Hex("#2BB673"),
            OnAccent = Hex("#07261A"),
            AccentSubtle = Hex("#163528"),
            Success = Hex("#4ADE80"),
            Warning = Hex("#F5B83D"),
            Error = Hex("#FF7B70"),
            ErrorSubtle = Hex("#3A2020"),
            SelectionBack = Hex("#1E4A37"),
            SelectionText = Hex("#EAFBF2"),
            TableHeaderBack = Hex("#202428"),
            TableHeaderText = Hex("#C8CED6"),
            TableZebra = Hex("#1E2125"),
            TitleButtonHover = Hex("#2A2E34"),
            CloseHover = Hex("#C42B1C"),
            OnClose = Hex("#FFFFFF"),
            Shadow = Color.FromArgb(90, 0, 0, 0),
        };
    }
}
