using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Domain;

namespace SporTakip
{
    /// <summary>Ölçüm giriş alanları ve ölçüm listeleri için ortak yardımcılar (takip ekleme/düzenleme ve sporcu detayı).</summary>
    internal static class OlcumUi
    {
        internal static readonly string[] OnerilenBirimler = { "kg", "cm", "%", "sn", "dk", "km", "bpm" };

        internal static string DegerMetni(decimal deger) => deger.ToString("0.##", CultureInfo.CurrentCulture);

        /// <summary>"Kilo: 95 kg" biçiminde tek ölçüm özeti.</summary>
        internal static string Ozet(string tur, decimal deger, string birim) =>
            string.Format("{0}: {1}", tur, DegerBirimMetni(deger, birim));

        /// <summary>"95 kg" ya da birim yoksa "95".</summary>
        internal static string DegerBirimMetni(decimal deger, string birim) =>
            string.IsNullOrWhiteSpace(birim) ? DegerMetni(deger) : DegerMetni(deger) + " " + birim.Trim();

        internal static string Ozet(IEnumerable<OlcumKaydi> olcumler) =>
            string.Join(", ", olcumler.Select(o => Ozet(o.Tur, o.Deger, o.Birim)));

        /// <summary>
        /// Değeri hem "72,5" hem "72.5" yazımıyla kabul eder (binlik ayırıcı desteklenmez;
        /// tr-TR'de "." binlik ayırıcı olduğundan "72.5" aksi halde 725 okunurdu).
        /// </summary>
        internal static bool DegerCoz(string metin, out decimal deger) =>
            DegerCoz(metin, CultureInfo.CurrentCulture, out deger);

        /// <summary>
        /// Saf ayrıştırma: yalnız baştaki/sondaki boşluk kırpılır; iç boşluk ("7 2,5", "72 500") veya
        /// birden fazla ayırıcı ("1.234,5") geçersizdir. Tek "," ya da "." ondalık ayırıcı sayılır.
        /// </summary>
        internal static bool DegerCoz(string metin, CultureInfo kultur, out decimal deger)
        {
            deger = 0;
            if (string.IsNullOrWhiteSpace(metin)) return false;
            string kirpik = metin.Trim();
            if (kirpik.Any(char.IsWhiteSpace)) return false;
            if (kirpik.Count(c => c == '.' || c == ',') > 1) return false;
            string ayirici = kultur.NumberFormat.NumberDecimalSeparator;
            string normal = kirpik.Replace(".", ayirici).Replace(",", ayirici);
            return decimal.TryParse(normal, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                kultur, out deger);
        }

        /// <summary>Tür önerilerini sporcunun mevcut türleriyle doldurur; kullanıcının yazdığı metin korunur.</summary>
        internal static async Task TurleriDoldurAsync(ComboBox combo, IOlcumService olcumService, int sporcuId,
            CancellationToken cancellationToken)
        {
            var turler = await olcumService.TurleriAsync(sporcuId, cancellationToken);
            if (combo.IsDisposed || cancellationToken.IsCancellationRequested) return;
            string yazilan = combo.Text;
            combo.BeginUpdate();
            combo.Items.Clear();
            combo.Items.AddRange(turler.Cast<object>().ToArray());
            combo.EndUpdate();
            combo.Text = yazilan;
        }

        private const int CB_SETCUEBANNER = 0x1703;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        /// <summary>WinForms ComboBox'ta PlaceholderText yok; düzenlenebilir kutuya Windows ipucu metni (cue banner) verir.</summary>
        internal static void IpucuAyarla(ComboBox combo, string ipucu)
        {
            void Uygula() => SendMessage(combo.Handle, CB_SETCUEBANNER, IntPtr.Zero, ipucu);
            if (combo.IsHandleCreated) Uygula();
            combo.HandleCreated += (s, e) => Uygula();
        }

        internal static void BirimleriDoldur(ComboBox combo)
        {
            combo.Items.Clear();
            combo.Items.AddRange(OnerilenBirimler.Cast<object>().ToArray());
        }

        /// <summary>Küçük ölçüm listesini SporcuDetayForm tablolarıyla aynı davranışa getirir ve sütunları ekler; renkleri ThemeApplier verir.</summary>
        internal static void GridHazirla(DataGridView grid)
        {
            grid.AutoGenerateColumns = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = true;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.StandardTab = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersHeight = 26;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.RowTemplate.Height = 22;
            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                WrapMode = DataGridViewTriState.False,
            };
            grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                WrapMode = DataGridViewTriState.False,
            };
            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOlcumTur", DataPropertyName = nameof(Olcum.Tur), HeaderText = "Tür", FillWeight = 50F,
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOlcumDeger", DataPropertyName = nameof(Olcum.Deger), HeaderText = "Değer", FillWeight = 28F,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "0.##", Alignment = DataGridViewContentAlignment.MiddleRight },
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOlcumBirim", DataPropertyName = nameof(Olcum.Birim), HeaderText = "Birim", FillWeight = 22F,
            });
        }

        /// <summary>
        /// Giriş kutularından bir ölçüm okur ve servis kurallarıyla doğrular; hatalar ErrorProvider'da
        /// (eşleşmeyenler mesaj kutusunda) gösterilir.
        /// </summary>
        /// <returns>Geçerliyse ölçüm (TakipId atanmamış), değilse null.</returns>
        internal static Olcum GirisiDogrula(IOlcumService olcumService, ComboBox tur, TextBox deger, ComboBox birim,
            ErrorProvider errors, IWin32Window owner)
        {
            errors.SetError(tur, string.Empty);
            errors.SetError(deger, string.Empty);
            errors.SetError(birim, string.Empty);
            if (!DegerCoz(deger.Text, out decimal sayi))
            {
                errors.SetError(deger, "Lütfen geçerli bir sayı girin (örn. 72,5).");
                return null;
            }
            var olcum = new Olcum
            {
                Tur = tur.Text.Trim(),
                Deger = sayi,
                Birim = string.IsNullOrWhiteSpace(birim.Text) ? null : birim.Text.Trim(),
            };
            var result = olcumService.Dogrula(olcum);
            return ValidationDisplay.Goster(result, errors, Alanlar(tur, deger, birim), owner) ? olcum : null;
        }

        internal static IReadOnlyDictionary<string, Control> Alanlar(ComboBox tur, TextBox deger, ComboBox birim) =>
            new Dictionary<string, Control>
            {
                [nameof(Olcum.Tur)] = tur,
                [nameof(Olcum.Deger)] = deger,
                [nameof(Olcum.Birim)] = birim,
            };

        internal static void GirisiTemizle(ComboBox tur, TextBox deger, ComboBox birim)
        {
            tur.Text = string.Empty;
            deger.Clear();
            birim.Text = string.Empty;
        }

        /// <summary>
        /// Ölçüm giriş kutularındayken Enter "Ölçüm Ekle"yi tetiklesin; çıkınca formun asıl kabul düğmesi geri gelsin.
        /// </summary>
        internal static void EnterIleEkle(Form form, Button ekleButonu, Button asilKabul, params Control[] girisler)
        {
            foreach (Control giris in girisler)
            {
                giris.Enter += (s, e) => form.AcceptButton = ekleButonu;
                // Odak değişiminde önce eski kutunun Leave'i, sonra yenisinin Enter'ı gelir;
                // giriş kutuları arasında geçerken ekleme düğmesi yeniden atanır.
                giris.Leave += (s, e) => form.AcceptButton = asilKabul;
            }
        }
    }
}
