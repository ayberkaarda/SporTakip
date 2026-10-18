using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SporTakip.Theme
{
    /// <summary>Kontrolün görsel rolü; renk ve yazı tipi token'ları role göre seçilir.</summary>
    public enum KontrolRolu
    {
        Varsayilan,
        /// <summary>Pencere başlığı.</summary>
        Baslik,
        /// <summary>Bölüm başlığı (kart içi).</summary>
        Bolum,
        /// <summary>Alan etiketi.</summary>
        Etiket,
        /// <summary>Özet, açıklama gibi ikincil metin.</summary>
        IkincilMetin,
        /// <summary>Boş/yükleniyor durumunu gösteren büyük etiket (yüzey zeminli).</summary>
        BosDurum,
        Birincil,
        Ikincil,
        Tehlike,
        /// <summary>Küçült, tema gibi başlık çubuğu düğmeleri.</summary>
        PencereDugmesi,
        KapatDugmesi,
    }

    /// <summary>Kendi çizdiği parçaları (grafik, kartlar) olan formlar tema değişince bunu uygular.</summary>
    public interface IThemeAware
    {
        void TemaUygula(ThemePalette palet);
    }

    /// <summary>
    /// Kontrolleri etkin paletin token'larıyla boyar. Formlar renk ya da yazı tipi sabiti tutmaz;
    /// yalnızca gerekirse <see cref="Rol"/> ile kontrolün rolünü bildirir.
    /// </summary>
    public static class ThemeApplier
    {
        private sealed class Kutu<T> { public T Deger; }

        private sealed class ButonDurumu
        {
            public bool Uzerinde;
            public bool Basili;
        }

        private static readonly ConditionalWeakTable<Control, Kutu<KontrolRolu>> Roller = new();
        private static readonly ConditionalWeakTable<Control, object> Bagli = new();
        private static readonly ConditionalWeakTable<Button, ButonDurumu> Durumlar = new();
        private static readonly List<WeakReference<ErrorProvider>> HataSaglayicilari = new();
        private static readonly Dictionary<ThemePalette, Icon> HataIkonlari = new();

        private static readonly PropertyInfo OdakIpucuOzelligi =
            typeof(Control).GetProperty("ShowFocusCues", BindingFlags.Instance | BindingFlags.NonPublic);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        public static void Rol(Control kontrol, KontrolRolu rol)
        {
            Roller.AddOrUpdate(kontrol, new Kutu<KontrolRolu> { Deger = rol });
        }

        public static void Rol(KontrolRolu rol, params Control[] kontroller)
        {
            foreach (Control c in kontroller) Rol(c, rol);
        }

        /// <summary>Açıkça verilmiş rol, yoksa ada göre ortak varsayılan (tüm formlarda aynı adlar kullanılıyor).</summary>
        public static KontrolRolu RolAl(Control c)
        {
            if (Roller.TryGetValue(c, out Kutu<KontrolRolu> kutu)) return kutu.Deger;
            switch (c.Name)
            {
                case "lblBaslik": return KontrolRolu.Baslik;
                case "btnkapat": return KontrolRolu.KapatDugmesi;
                case "btnAsagial": return KontrolRolu.PencereDugmesi;
                case "btnKaydet": return KontrolRolu.Birincil;
            }
            if (c is Button) return KontrolRolu.Ikincil;
            if (c is Label) return KontrolRolu.Etiket;
            return KontrolRolu.Varsayilan;
        }

        public static bool BaslikDugmesiMi(Control c)
        {
            KontrolRolu rol = RolAl(c);
            return rol == KontrolRolu.PencereDugmesi || rol == KontrolRolu.KapatDugmesi;
        }

        /// <summary>Formu ve tüm alt kontrollerini etkin paletle boyar.</summary>
        public static void Uygula(Control kok)
        {
            if (kok == null || kok.IsDisposed) return;
            ThemePalette p = ThemeManager.Palet;
            kok.SuspendLayout();
            try
            {
                Boya(kok, p);
                if (kok is IThemeAware duyarli) duyarli.TemaUygula(p);
            }
            finally { kok.ResumeLayout(false); }
            HataSaglayicilariniGuncelle(p);
            kok.Invalidate(true);
        }

        private static void Boya(Control c, ThemePalette p)
        {
            switch (c)
            {
                case Form form:
                    form.BackColor = p.Background;
                    form.ForeColor = p.TextPrimary;
                    break;
                case Label label:
                    EtiketBoya(label, p);
                    break;
                case Button button:
                    ButonBoya(button, p);
                    break;
                case TextBox textBox:
                    textBox.BackColor = p.Input;
                    textBox.ForeColor = p.TextPrimary;
                    textBox.BorderStyle = BorderStyle.Fixed3D;
                    YerelTema(textBox, "DarkMode_CFD", null);
                    break;
                case ComboBox combo:
                    // Standard çizimde koyu alt tema (DarkMode_CFD) ok düğmesini de koyulaştırır; Flat'te düğme beyaz kalır.
                    combo.FlatStyle = FlatStyle.Standard;
                    combo.BackColor = p.Input;
                    combo.ForeColor = p.TextPrimary;
                    YerelTema(combo, "DarkMode_CFD", null);
                    break;
                case ListBox listBox:
                    ListeBoya(listBox, p);
                    break;
                case DataGridView grid:
                    TabloBoya(grid, p);
                    break;
                case DateTimePicker tarih:
                    tarih.CalendarMonthBackground = p.Surface;
                    tarih.CalendarForeColor = p.TextPrimary;
                    tarih.CalendarTitleBackColor = p.Accent;
                    tarih.CalendarTitleForeColor = p.OnAccent;
                    tarih.CalendarTrailingForeColor = p.TextDisabled;
                    break;
                case CheckBox checkBox:
                    checkBox.BackColor = Color.Transparent;
                    checkBox.ForeColor = p.TextPrimary;
                    break;
                case ScrollableControl kaydirilabilir when kaydirilabilir.AutoScroll:
                    YerelTema(kaydirilabilir, "DarkMode_Explorer", "Explorer");
                    break;
            }
            foreach (Control child in c.Controls) Boya(child, p);
        }

        private static void EtiketBoya(Label label, ThemePalette p)
        {
            KontrolRolu rol = RolAl(label);
            label.BackColor = rol == KontrolRolu.BosDurum ? p.Surface : Color.Transparent;
            switch (rol)
            {
                case KontrolRolu.Baslik:
                    label.Font = ThemeTypography.Baslik;
                    label.ForeColor = p.TextPrimary;
                    break;
                case KontrolRolu.Bolum:
                    label.Font = ThemeTypography.Bolum;
                    label.ForeColor = p.TextPrimary;
                    break;
                case KontrolRolu.IkincilMetin:
                    label.Font = ThemeTypography.Govde;
                    label.ForeColor = p.TextSecondary;
                    break;
                case KontrolRolu.BosDurum:
                    label.Font = ThemeTypography.Bos;
                    label.ForeColor = p.TextSecondary;
                    break;
                default:
                    label.Font = ThemeTypography.GovdeVurgulu;
                    label.ForeColor = p.TextSecondary;
                    break;
            }
        }

        private static void ListeBoya(ListBox liste, ThemePalette p)
        {
            liste.BackColor = p.Input;
            liste.ForeColor = p.TextPrimary;
            liste.BorderStyle = BorderStyle.None;
            YerelTema(liste, "DarkMode_Explorer", "Explorer");
            if (!IlkKez(liste)) return;
            liste.DrawMode = DrawMode.OwnerDrawFixed;
            liste.ItemHeight = TextRenderer.MeasureText("Ağ", liste.Font).Height + liste.LogicalToDeviceUnits(10);
            liste.DrawItem += (s, e) =>
            {
                if (e.Index < 0 || e.Index >= liste.Items.Count) return;
                ThemePalette pal = ThemeManager.Palet;
                bool secili = (e.State & DrawItemState.Selected) != 0;
                using (var zemin = new SolidBrush(secili ? pal.SelectionBack : pal.Input))
                    e.Graphics.FillRectangle(zemin, e.Bounds);
                string metin = liste.GetItemText(liste.Items[e.Index]);
                int ic = liste.LogicalToDeviceUnits(ThemeScale.AralikS);
                var alan = new Rectangle(e.Bounds.X + ic, e.Bounds.Y, Math.Max(0, e.Bounds.Width - ic * 2), e.Bounds.Height);
                const TextFormatFlags bicim = TextFormatFlags.Left | TextFormatFlags.VerticalCenter
                    | TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix;
                TextRenderer.DrawText(e.Graphics, metin, liste.Font, alan, secili ? pal.SelectionText : pal.TextPrimary, bicim);
                if (liste.HorizontalScrollbar)
                {
                    // Sahibi-çizimli listede yatay kaydırma genişliği elle verilmeli.
                    int gerek = TextRenderer.MeasureText(metin, liste.Font).Width + ic * 2;
                    if (gerek > liste.HorizontalExtent) liste.HorizontalExtent = gerek;
                }
            };
        }

        private static void TabloBoya(DataGridView grid, ThemePalette p)
        {
            grid.EnableHeadersVisualStyles = false;
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = p.Surface;
            grid.GridColor = p.Border;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Mevcut stilin kopyası üzerinde çalışılır (tasarımcıdaki hizalama korunur) ve geri atanır: eksik alanı olan
            // stilde DataGridView getter'ı geçici bir kopya döndürdüğünden doğrudan değiştirmek etkisiz kalır.
            var baslik = new DataGridViewCellStyle(grid.ColumnHeadersDefaultCellStyle);
            baslik.BackColor = p.TableHeaderBack;
            baslik.ForeColor = p.TableHeaderText;
            baslik.SelectionBackColor = p.TableHeaderBack;
            baslik.SelectionForeColor = p.TableHeaderText;
            baslik.Font = ThemeTypography.KucukVurgulu;
            baslik.Padding = new Padding(grid.LogicalToDeviceUnits(4), 0, 0, 0);
            grid.ColumnHeadersDefaultCellStyle = baslik;

            var hucre = new DataGridViewCellStyle(grid.DefaultCellStyle);
            hucre.BackColor = p.Surface;
            hucre.ForeColor = p.TextPrimary;
            hucre.SelectionBackColor = p.SelectionBack;
            hucre.SelectionForeColor = p.SelectionText;
            hucre.Font = ThemeTypography.Govde;
            hucre.Padding = new Padding(grid.LogicalToDeviceUnits(4), 0, grid.LogicalToDeviceUnits(4), 0);
            grid.DefaultCellStyle = hucre;

            // Satır ve başlık yükseklikleri yazı tipine göre: yüksek DPI'da metin kırpılmasın.
            int satir = TextRenderer.MeasureText("Ağ", ThemeTypography.Govde).Height + grid.LogicalToDeviceUnits(ThemeScale.AralikS);
            if (grid.RowTemplate.Height < satir) grid.RowTemplate.Height = satir;
            int baslikYuksekligi = TextRenderer.MeasureText("Ağ", ThemeTypography.KucukVurgulu).Height + grid.LogicalToDeviceUnits(ThemeScale.AralikM);
            if (grid.ColumnHeadersHeight < baslikYuksekligi) grid.ColumnHeadersHeight = baslikYuksekligi;

            grid.AlternatingRowsDefaultCellStyle.BackColor = p.TableZebra;
            grid.RowsDefaultCellStyle.BackColor = p.Surface;

            foreach (Control child in grid.Controls)
                if (child is ScrollBar) YerelTema(child, "DarkMode_Explorer", "Explorer");
            grid.Invalidate();
        }

        // ---- Düğmeler: tamamen token'larla çizilir (yuvarlatılmış köşe, hover/pressed, odak halkası) ----

        private static void ButonBoya(Button b, ThemePalette p)
        {
            KontrolRolu rol = RolAl(b);
            bool baslikDugmesi = rol == KontrolRolu.PencereDugmesi || rol == KontrolRolu.KapatDugmesi;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.UseVisualStyleBackColor = false;
            b.Cursor = Cursors.Hand;
            b.Font = baslikDugmesi ? ThemeTypography.Simge : ThemeTypography.GovdeVurgulu;
            if (baslikDugmesi) b.TabStop = false;
            b.BackColor = ZeminRengi(b);
            b.FlatAppearance.MouseOverBackColor = b.BackColor;
            b.FlatAppearance.MouseDownBackColor = b.BackColor;
            b.ForeColor = p.TextPrimary;
            if (!IlkKez(b)) return;
            var durum = Durumlar.GetOrCreateValue(b);
            b.MouseEnter += (s, e) => { durum.Uzerinde = true; b.Invalidate(); };
            b.MouseLeave += (s, e) => { durum.Uzerinde = false; durum.Basili = false; b.Invalidate(); };
            b.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { durum.Basili = true; b.Invalidate(); } };
            b.MouseUp += (s, e) => { durum.Basili = false; b.Invalidate(); };
            b.EnabledChanged += (s, e) => { if (!b.Enabled) { durum.Uzerinde = durum.Basili = false; } b.Invalidate(); };
            b.GotFocus += (s, e) => b.Invalidate();
            b.LostFocus += (s, e) => b.Invalidate();
            b.Paint += Buton_Paint;
        }

        private static void Buton_Paint(object sender, PaintEventArgs e)
        {
            var b = (Button)sender;
            ThemePalette p = ThemeManager.Palet;
            ButonDurumu d = Durumlar.GetOrCreateValue(b);
            KontrolRolu rol = RolAl(b);
            Graphics g = e.Graphics;
            Color zemin = ZeminRengi(b);
            g.Clear(zemin);
            var alan = new Rectangle(0, 0, b.Width, b.Height);

            if (rol == KontrolRolu.PencereDugmesi || rol == KontrolRolu.KapatDugmesi)
            {
                bool kapat = rol == KontrolRolu.KapatDugmesi;
                if (b.Enabled && (d.Uzerinde || d.Basili))
                    using (var dolgu = new SolidBrush(kapat ? p.CloseHover : p.TitleButtonHover))
                        g.FillRectangle(dolgu, alan);
                Color simgeRengi = kapat && d.Uzerinde ? p.OnClose : d.Uzerinde ? p.TextPrimary : p.TextSecondary;
                string simge = b.Name == "btnkapat" ? "" : b.Name == "btnAsagial" ? "" : b.Text;
                TextRenderer.DrawText(g, simge, ThemeTypography.Simge, alan, simgeRengi,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix);
                // Düğme pencere kenarına oturuyorsa formun çerçeve çizgisini düğmenin üstünde sürdür.
                if (b.Parent is Form ebeveyn)
                {
                    using var kenar = new Pen(p.BorderStrong);
                    if (b.Top == 0) g.DrawLine(kenar, 0, 0, b.Width, 0);
                    if (b.Right >= ebeveyn.ClientSize.Width) g.DrawLine(kenar, b.Width - 1, 0, b.Width - 1, b.Height);
                }
                return;
            }

            Color dolguRengi, kenarRengi, yaziRengi;
            switch (rol)
            {
                case KontrolRolu.Birincil:
                    dolguRengi = !b.Enabled ? p.SurfaceAlt : d.Basili ? p.AccentPressed : d.Uzerinde ? p.AccentHover : p.Accent;
                    kenarRengi = dolguRengi;
                    yaziRengi = b.Enabled ? p.OnAccent : p.TextDisabled;
                    break;
                case KontrolRolu.Tehlike:
                    dolguRengi = b.Enabled && (d.Uzerinde || d.Basili) ? p.ErrorSubtle : zemin;
                    kenarRengi = b.Enabled ? p.Error : p.Border;
                    yaziRengi = b.Enabled ? p.Error : p.TextDisabled;
                    break;
                default:
                    dolguRengi = !b.Enabled ? p.SurfaceAlt : d.Basili ? p.Border : d.Uzerinde ? p.TitleButtonHover : p.SurfaceAlt;
                    kenarRengi = b.Enabled && d.Uzerinde ? p.BorderStrong : p.Border;
                    yaziRengi = b.Enabled ? p.TextPrimary : p.TextDisabled;
                    break;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;
            int r = b.LogicalToDeviceUnits(ThemeScale.KoseOrta);
            var cerceve = new RectangleF(0.5f, 0.5f, b.Width - 1.5f, b.Height - 1.5f);
            using (GraphicsPath yol = Yuvarlak(cerceve, r))
            {
                using (var dolgu = new SolidBrush(dolguRengi)) g.FillPath(dolgu, yol);
                using (var kalem = new Pen(kenarRengi)) g.DrawPath(kalem, yol);
            }
            if (b.Focused && OdakIpucuGoster(b))
            {
                float k = b.LogicalToDeviceUnits(ThemeScale.OdakHalkasi);
                var halka = RectangleF.Inflate(cerceve, -k, -k);
                using GraphicsPath odak = Yuvarlak(halka, Math.Max(1, r - (int)k));
                using var kalem = new Pen(rol == KontrolRolu.Birincil ? p.OnAccent : p.Accent, k);
                g.DrawPath(kalem, odak);
            }
            g.SmoothingMode = SmoothingMode.None;
            TextRenderer.DrawText(g, b.Text, b.Font, alan, yaziRengi,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine
                | TextFormatFlags.EndEllipsis | TextFormatFlags.HidePrefix);
        }

        private static bool OdakIpucuGoster(Control c)
        {
            try { return OdakIpucuOzelligi == null || (bool)OdakIpucuOzelligi.GetValue(c); }
            catch (Exception) { return true; }
        }

        /// <summary>Kontrolün arkasında görünen renk: kart içindeyse yüzey, değilse ilk opak ebeveynin rengi.</summary>
        public static Color ZeminRengi(Control c)
        {
            if (c.Parent is BorderlessForm form && form.KartIcinde(c)) return ThemeManager.Palet.Surface;
            for (Control parent = c.Parent; parent != null; parent = parent.Parent)
                if (parent.BackColor.A == 255) return parent.BackColor;
            return ThemeManager.Palet.Background;
        }

        public static GraphicsPath Yuvarlak(RectangleF r, float yaricap)
        {
            float d = Math.Max(1f, Math.Min(yaricap * 2, Math.Min(r.Width, r.Height)));
            var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // ---- Yerel (Windows) tema: kaydırma çubukları, açılır listeler ----

        private static readonly ConditionalWeakTable<Control, Kutu<(string Koyu, string Acik)>> YerelTemalar = new();

        private static void YerelTema(Control c, string koyu, string acik)
        {
            bool ilk = !YerelTemalar.TryGetValue(c, out _);
            YerelTemalar.AddOrUpdate(c, new Kutu<(string, string)> { Deger = (koyu, acik) });
            if (ilk) c.HandleCreated += (s, e) => YerelTemaUygula(c);
            YerelTemaUygula(c);
        }

        private static void YerelTemaUygula(Control c)
        {
            if (!c.IsHandleCreated || !YerelTemalar.TryGetValue(c, out var kutu)) return;
            string ad = ThemeManager.Palet.Koyu ? kutu.Deger.Koyu : kutu.Deger.Acik;
            // Windows 10 1809 öncesinde koyu alt temalar yoktur; çağrı etkisiz kalır.
            try { SetWindowTheme(c.Handle, ad, null); }
            catch (Exception ex) when (ex is DllNotFoundException || ex is EntryPointNotFoundException) { }
        }

        private static bool IlkKez(Control c)
        {
            if (Bagli.TryGetValue(c, out _)) return false;
            Bagli.Add(c, null);
            return true;
        }

        // ---- ErrorProvider: hata simgesi paletin hata rengiyle çizilir ----

        /// <summary>Formların ErrorProvider'ları buraya kaydedilir; tema değişince simgeleri güncellenir.</summary>
        public static void HataSaglayici(ErrorProvider saglayici)
        {
            lock (HataSaglayicilari) HataSaglayicilari.Add(new WeakReference<ErrorProvider>(saglayici));
            saglayici.Icon = HataIkonu(ThemeManager.Palet);
        }

        private static void HataSaglayicilariniGuncelle(ThemePalette p)
        {
            Icon ikon = HataIkonu(p);
            lock (HataSaglayicilari)
            {
                HataSaglayicilari.RemoveAll(w => !w.TryGetTarget(out _));
                foreach (WeakReference<ErrorProvider> w in HataSaglayicilari)
                    if (w.TryGetTarget(out ErrorProvider ep) && !ReferenceEquals(ep.Icon, ikon)) ep.Icon = ikon;
            }
        }

        private static Icon HataIkonu(ThemePalette p)
        {
            lock (HataIkonlari)
            {
                if (HataIkonlari.TryGetValue(p, out Icon mevcut)) return mevcut;
                int boyut = Math.Max(16, SystemInformation.SmallIconSize.Width);
                using var bmp = new Bitmap(boyut, boyut);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                    using (var dolgu = new SolidBrush(p.Error)) g.FillEllipse(dolgu, 0.5f, 0.5f, boyut - 1.5f, boyut - 1.5f);
                    Color isaret = p.Koyu ? p.Background : Color.White;
                    using var font = new Font(ThemeTypography.AileSeminkalin, boyut * 0.5f, FontStyle.Bold, GraphicsUnit.Pixel);
                    using var firca = new SolidBrush(isaret);
                    using var bicim = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString("!", font, firca, new RectangleF(0, 0, boyut, boyut), bicim);
                }
                // İki palet için birer kez üretilir ve uygulama boyunca saklanır.
                Icon ikon = Icon.FromHandle(bmp.GetHicon());
                HataIkonlari[p] = ikon;
                return ikon;
            }
        }

        // ---- Menü (tema seçimi) ----

        public static void MenuBoya(ToolStrip menu)
        {
            ThemePalette p = ThemeManager.Palet;
            menu.Renderer = new TemaMenuCizici(p);
            menu.BackColor = p.Surface;
            menu.ForeColor = p.TextPrimary;
            menu.Font = ThemeTypography.Govde;
            foreach (ToolStripItem item in menu.Items) item.ForeColor = p.TextPrimary;
        }

        private sealed class TemaMenuCizici : ToolStripProfessionalRenderer
        {
            private readonly ThemePalette _p;

            public TemaMenuCizici(ThemePalette p) : base(new TemaRenkTablosu(p))
            {
                _p = p;
                RoundedEdges = false;
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = e.Item.Enabled ? _p.TextPrimary : _p.TextDisabled;
                base.OnRenderItemText(e);
            }

            protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
            {
                Rectangle r = e.ImageRectangle;
                TextRenderer.DrawText(e.Graphics, "", ThemeTypography.Simge, r, _p.Accent,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
            }
        }

        private sealed class TemaRenkTablosu : ProfessionalColorTable
        {
            private readonly ThemePalette _p;

            public TemaRenkTablosu(ThemePalette p)
            {
                _p = p;
                UseSystemColors = false;
            }

            public override Color ToolStripDropDownBackground => _p.Surface;
            public override Color MenuBorder => _p.Border;
            public override Color MenuItemBorder => _p.SurfaceAlt;
            public override Color MenuItemSelected => _p.SurfaceAlt;
            public override Color MenuItemSelectedGradientBegin => _p.SurfaceAlt;
            public override Color MenuItemSelectedGradientEnd => _p.SurfaceAlt;
            public override Color MenuItemPressedGradientBegin => _p.Border;
            public override Color MenuItemPressedGradientEnd => _p.Border;
            public override Color ImageMarginGradientBegin => _p.Surface;
            public override Color ImageMarginGradientMiddle => _p.Surface;
            public override Color ImageMarginGradientEnd => _p.Surface;
            public override Color SeparatorDark => _p.Border;
            public override Color SeparatorLight => _p.Surface;
            public override Color CheckBackground => _p.Surface;
            public override Color CheckSelectedBackground => _p.SurfaceAlt;
            public override Color CheckPressedBackground => _p.SurfaceAlt;
        }
    }
}
