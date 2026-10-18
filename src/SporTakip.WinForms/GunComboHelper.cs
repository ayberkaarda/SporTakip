using System.Windows.Forms;

namespace SporTakip
{
    /// <summary>Antrenman.Gun (1=Pazartesi ... 7=Pazar, null=atanmamış) için ortak gün seçimi.</summary>
    internal static class GunComboHelper
    {
        internal static readonly string[] GunAdlari =
            { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };

        internal const string AtanmamisMetni = "Atanmamış";

        /// <summary>Sıra: 0=Atanmamış, 1..7=Pazartesi..Pazar; böylece SelectedIndex doğrudan Gun değeridir.</summary>
        internal static void Doldur(ComboBox combo, int? secili = null)
        {
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.BeginUpdate();
            combo.Items.Clear();
            combo.Items.Add(AtanmamisMetni);
            foreach (string ad in GunAdlari) combo.Items.Add(ad);
            combo.EndUpdate();
            Sec(combo, secili);
        }

        internal static void Sec(ComboBox combo, int? gun) =>
            combo.SelectedIndex = gun is >= 1 and <= 7 ? gun.Value : 0;

        internal static int? SeciliGun(ComboBox combo) =>
            combo.SelectedIndex is >= 1 and <= 7 ? combo.SelectedIndex : null;
    }
}
