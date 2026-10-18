using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SporTakip.Theme
{
    public enum TemaModu
    {
        Sistem,
        Acik,
        Koyu,
    }

    /// <summary>
    /// Etkin temayı belirler ve değişince açık pencereleri yeniden boyar.
    /// Öncelik: kullanıcının seçimi (%LOCALAPPDATA%\SporTakip\tema.json) &gt; appsettings.json "Tema" &gt; Sistem.
    /// Sistem modunda Windows'un uygulama teması (açık/koyu) izlenir.
    /// </summary>
    public static class ThemeManager
    {
        private static bool _sistemIzleniyor;

        internal static string AyarDosyasi { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SporTakip", "tema.json");

        public static TemaModu Mod { get; private set; } = TemaModu.Sistem;

        public static ThemePalette Palet { get; private set; } = SistemKoyuMu() ? ThemePalette.KoyuPalet : ThemePalette.Acik;

        /// <summary>Palet değiştiğinde (açık formlar boyandıktan sonra) tetiklenir.</summary>
        public static event EventHandler Degisti;

        /// <summary>Açılışta, ilk pencereden önce çağrılır. <paramref name="ayarDegeri"/> appsettings.json'daki "Tema".</summary>
        public static void Baslat(string ayarDegeri)
        {
            TemaModu mod = KullaniciSecimiOku() ?? Coz(ayarDegeri) ?? TemaModu.Sistem;
            Ayarla(mod, kaydet: false);
            if (!_sistemIzleniyor)
            {
                _sistemIzleniyor = true;
                SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            }
        }

        /// <summary>Kullanıcının seçtiği modu uygular ve kalıcı olarak kaydeder.</summary>
        public static void Degistir(TemaModu mod) => Ayarla(mod, kaydet: true);

        public static TemaModu? Coz(string deger)
        {
            if (string.IsNullOrWhiteSpace(deger)) return null;
            switch (deger.Trim().ToLowerInvariant())
            {
                case "acik": case "açık": case "light": return TemaModu.Acik;
                case "koyu": case "dark": return TemaModu.Koyu;
                case "sistem": case "system": return TemaModu.Sistem;
                default: return null;
            }
        }

        private static void Ayarla(TemaModu mod, bool kaydet)
        {
            Mod = mod;
            if (kaydet) KullaniciSecimiYaz(mod);
            Yenile();
        }

        private static void Yenile()
        {
            bool koyu = Mod == TemaModu.Koyu || (Mod == TemaModu.Sistem && SistemKoyuMu());
            ThemePalette yeni = koyu ? ThemePalette.KoyuPalet : ThemePalette.Acik;
            bool farkli = !ReferenceEquals(yeni, Palet);
            Palet = yeni;
            // Mod değişse de palet aynıysa yeniden boyamaya gerek yok; menüdeki işaret için olay yine de gider.
            if (farkli)
            {
                foreach (Form form in System.Windows.Forms.Application.OpenForms.Cast<Form>().ToList())
                    if (!form.IsDisposed) ThemeApplier.Uygula(form);
            }
            Degisti?.Invoke(null, EventArgs.Empty);
        }

        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category != UserPreferenceCategory.General || Mod != TemaModu.Sistem) return;
            // Olay ayrı bir iş parçacığında gelebilir; boyama UI iş parçacığında yapılmalı.
            Form hedef = System.Windows.Forms.Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.IsHandleCreated && !f.IsDisposed);
            if (hedef == null) return;
            try { hedef.BeginInvoke(new Action(Yenile)); }
            catch (InvalidOperationException) { }
        }

        /// <summary>Windows "uygulama modu" koyu mu? Okunamazsa açık kabul edilir.</summary>
        public static bool SistemKoyuMu()
        {
            try
            {
                using RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                return key?.GetValue("AppsUseLightTheme") is int deger && deger == 0;
            }
            catch (Exception) { return false; }
        }

        private sealed class TemaAyari
        {
            public string Tema { get; set; }
        }

        private static TemaModu? KullaniciSecimiOku()
        {
            try
            {
                if (!File.Exists(AyarDosyasi)) return null;
                var ayar = JsonSerializer.Deserialize<TemaAyari>(File.ReadAllText(AyarDosyasi));
                return Coz(ayar?.Tema);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is JsonException)
            {
                ErrorHandler.Log(ex);
                return null;
            }
        }

        private static void KullaniciSecimiYaz(TemaModu mod)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(AyarDosyasi));
                File.WriteAllText(AyarDosyasi, JsonSerializer.Serialize(new TemaAyari { Tema = mod.ToString() }));
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                // Seçim kaydedilemese de bu oturumda uygulanır.
                ErrorHandler.Log(ex);
            }
        }
    }
}
