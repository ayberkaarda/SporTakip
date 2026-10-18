using System;
using System.Collections.Generic;
using System.Drawing;

namespace SporTakip.Theme
{
    /// <summary>WCAG 2.x kontrast oranı hesabı ve paletteki metin/zemin çiftlerinin denetimi.</summary>
    public static class ThemeContrast
    {
        public const double AaNormalMetin = 4.5;

        public static double GoreliParlaklik(Color c)
        {
            static double Kanal(int v)
            {
                double s = v / 255.0;
                return s <= 0.03928 ? s / 12.92 : Math.Pow((s + 0.055) / 1.055, 2.4);
            }
            return 0.2126 * Kanal(c.R) + 0.7152 * Kanal(c.G) + 0.0722 * Kanal(c.B);
        }

        public static double Oran(Color on, Color arka)
        {
            double a = GoreliParlaklik(on), b = GoreliParlaklik(arka);
            return (Math.Max(a, b) + 0.05) / (Math.Min(a, b) + 0.05);
        }

        public sealed record Sonuc(string Cift, double Oran, bool Gecti);

        /// <summary>Arayüzde metin olarak kullanılan tüm çiftler (devre dışı metin WCAG gereği hariç).</summary>
        public static IReadOnlyList<Sonuc> Denetle(ThemePalette p)
        {
            var ciftler = new (string, Color, Color)[]
            {
                ("TextPrimary/Background", p.TextPrimary, p.Background),
                ("TextPrimary/Surface", p.TextPrimary, p.Surface),
                ("TextPrimary/SurfaceAlt", p.TextPrimary, p.SurfaceAlt),
                ("TextPrimary/Input", p.TextPrimary, p.Input),
                ("TextSecondary/Background", p.TextSecondary, p.Background),
                ("TextSecondary/Surface", p.TextSecondary, p.Surface),
                ("TextSecondary/SurfaceAlt", p.TextSecondary, p.SurfaceAlt),
                ("OnAccent/Accent", p.OnAccent, p.Accent),
                ("OnAccent/AccentHover", p.OnAccent, p.AccentHover),
                ("OnAccent/AccentPressed", p.OnAccent, p.AccentPressed),
                ("Accent/Surface", p.Accent, p.Surface),
                ("Error/Surface", p.Error, p.Surface),
                ("Error/ErrorSubtle", p.Error, p.ErrorSubtle),
                ("Success/Surface", p.Success, p.Surface),
                ("Warning/Surface", p.Warning, p.Surface),
                ("SelectionText/SelectionBack", p.SelectionText, p.SelectionBack),
                ("TableHeaderText/TableHeaderBack", p.TableHeaderText, p.TableHeaderBack),
                ("TextPrimary/TableZebra", p.TextPrimary, p.TableZebra),
                ("TextPrimary/TitleButtonHover", p.TextPrimary, p.TitleButtonHover),
                ("OnClose/CloseHover", p.OnClose, p.CloseHover),
            };
            var sonuc = new List<Sonuc>();
            foreach (var (ad, on, arka) in ciftler)
            {
                double oran = Oran(on, arka);
                sonuc.Add(new Sonuc(ad, Math.Round(oran, 2), oran >= AaNormalMetin));
            }
            return sonuc;
        }
    }
}
