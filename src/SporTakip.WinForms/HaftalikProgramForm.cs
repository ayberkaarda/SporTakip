using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Application.Reporting;
using SporTakip.Domain;

namespace SporTakip
{
    /// <summary>Bir sporcunun antrenmanlarını haftanın günlerine göre yan yana kartlarda gösterir.</summary>
    public class HaftalikProgramForm : BorderlessForm
    {
        // OlcumGrafikForm ile aynı koyu lacivert tema.
        private static readonly Color FormArkaPlan = Color.FromArgb(8, 22, 64);
        private static readonly Color KartArkaPlan = Color.FromArgb(14, 34, 88);
        private static readonly Color OgeArkaPlan = Color.FromArgb(24, 50, 116);
        private static readonly Color IkincilYazi = Color.FromArgb(170, 188, 228);
        private static readonly Color VurguRengi = Color.FromArgb(240, 122, 40);

        // Kartlar bu genişliğin altına inmez; pencere ekrana sığmazsa gün alanı yatay kayar.
        private const int KartMinGenislik = 118;

        private readonly IAntrenmanService _antrenmanService;
        private readonly System.ComponentModel.IContainer components = new System.ComponentModel.Container();
        private readonly ToolTip _ipucu;
        private readonly FlowLayoutPanel[] _gunListeleri = new FlowLayoutPanel[7];
        private readonly Label[] _gunSayilari = new Label[7];
        private readonly Panel[] _gunKartlari = new Panel[7];
        private int _sporcuId;

        private Label lblBaslik;
        private Label lblSporcu;
        private Label lblOzet;
        private Label lblBos;
        private Button btnkapat;
        private Panel pnlGunler;
        private TableLayoutPanel tblGunler;
        private Panel pnlAtanmamis;
        private Label lblAtanmamis;
        private FlowLayoutPanel flwAtanmamis;

        public HaftalikProgramForm(IAntrenmanService antrenmanService)
        {
            _antrenmanService = antrenmanService;
            _ipucu = new ToolTip(components) { AutoPopDelay = 15000, InitialDelay = 400 };
            InitializeComponent();
        }

        /// <summary>Pencere gösterilmeden önce hangi sporcunun programının gösterileceğini verir.</summary>
        public void Yukle(int sporcuId, string sporcuAdi)
        {
            _sporcuId = sporcuId;
            lblSporcu.Text = string.IsNullOrWhiteSpace(sporcuAdi) ? string.Empty : "— " + sporcuAdi;
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            lblBaslik = new Label
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 14F, FontStyle.Regular, GraphicsUnit.Point, 162),
                ForeColor = Color.White,
                Location = new Point(18, 14),
                Name = "lblBaslik",
                Text = "Haftalık Program",
            };
            lblSporcu = new Label
            {
                AutoEllipsis = true,
                BackColor = Color.Transparent,
                ForeColor = IkincilYazi,
                Location = new Point(182, 19),
                Name = "lblSporcu",
                Size = new Size(420, 20),
            };
            lblOzet = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent,
                ForeColor = IkincilYazi,
                Location = new Point(600, 46),
                Name = "lblOzet",
                Size = new Size(360, 20),
                TextAlign = ContentAlignment.MiddleRight,
            };
            btnkapat = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent,
                DialogResult = DialogResult.Cancel,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 162),
                ForeColor = Color.White,
                Location = new Point(934, 0),
                Name = "btnkapat",
                Size = new Size(46, 32),
                TabStop = false,
                Text = "×",
                UseVisualStyleBackColor = false,
            };
            btnkapat.FlatAppearance.BorderSize = 0;
            btnkapat.FlatAppearance.MouseOverBackColor = CloseHoverColor;

            // Gün kartları: 7 eşit sütun; dar pencerede KartMinGenislik altına inmez, kaydırılır.
            tblGunler = new TableLayoutPanel
            {
                BackColor = Color.Transparent,
                ColumnCount = 7,
                RowCount = 1,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Name = "tblGunler",
            };
            tblGunler.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            for (int i = 0; i < 7; i++)
            {
                tblGunler.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / 7));
                tblGunler.Controls.Add(GunKartiOlustur(i), i, 0);
            }
            pnlGunler = new Panel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = true,
                AutoScrollMinSize = new Size(KartMinGenislik * 7, 0),
                BackColor = Color.Transparent,
                Location = new Point(14, 72),
                Name = "pnlGunler",
                Size = new Size(952, 368),
            };
            pnlGunler.Controls.Add(tblGunler);
            KoyuKaydirmaCubugu(pnlGunler);

            // Güne atanmamış antrenmanlar: kartların altında ayrı şerit, yalnızca varsa görünür.
            lblAtanmamis = new Label
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 162),
                ForeColor = Color.White,
                Location = new Point(10, 8),
                Name = "lblAtanmamis",
                Text = GunComboHelper.AtanmamisMetni,
            };
            flwAtanmamis = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = true,
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.LeftToRight,
                Location = new Point(6, 30),
                Name = "flwAtanmamis",
                Size = new Size(936, 60),
                WrapContents = true,
            };
            pnlAtanmamis = new Panel
            {
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = KartArkaPlan,
                Location = new Point(18, 448),
                Name = "pnlAtanmamis",
                Size = new Size(944, 96),
                Visible = false,
            };
            KoyuKaydirmaCubugu(flwAtanmamis);
            pnlAtanmamis.Controls.Add(flwAtanmamis);
            pnlAtanmamis.Controls.Add(lblAtanmamis);
            pnlAtanmamis.Paint += (s, e) => KenarCiz(e.Graphics, pnlAtanmamis, IkincilYazi, dashed: true);

            lblBos = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = KartArkaPlan,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162),
                ForeColor = IkincilYazi,
                Location = new Point(18, 76),
                Name = "lblBos",
                Size = new Size(944, 468),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false,
            };

            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = FormArkaPlan;
            CancelButton = btnkapat;
            ClientSize = new Size(980, 560);
            Controls.Add(lblBos);
            Controls.Add(pnlAtanmamis);
            Controls.Add(pnlGunler);
            Controls.Add(btnkapat);
            Controls.Add(lblOzet);
            Controls.Add(lblSporcu);
            Controls.Add(lblBaslik);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 162);
            Name = "HaftalikProgramForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Spor Istanbul | Haftalık Program";
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel GunKartiOlustur(int index)
        {
            var baslik = new Label
            {
                AutoEllipsis = true,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 162),
                ForeColor = Color.White,
                Location = new Point(10, 10),
                Name = "lblGun" + (index + 1),
                Size = new Size(110, 22),
                Text = GunComboHelper.GunAdlari[index],
            };
            var sayi = new Label
            {
                BackColor = Color.Transparent,
                ForeColor = IkincilYazi,
                Location = new Point(10, 32),
                Name = "lblGunSayi" + (index + 1),
                Size = new Size(110, 18),
                Text = "—",
            };
            var liste = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = true,
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.TopDown,
                Location = new Point(4, 56),
                Name = "flwGun" + (index + 1),
                Size = new Size(122, 300),
                WrapContents = false,
            };
            liste.ClientSizeChanged += (s, e) => OgeGenislikleriniAyarla(liste);
            KoyuKaydirmaCubugu(liste);
            var kart = new Panel
            {
                BackColor = KartArkaPlan,
                Dock = DockStyle.Fill,
                Margin = new Padding(4, 4, 4, 4),
                Name = "pnlGun" + (index + 1),
                Size = new Size(130, 360),
            };
            kart.Controls.Add(liste);
            kart.Controls.Add(sayi);
            kart.Controls.Add(baslik);
            kart.Resize += (s, e) =>
            {
                int w = Math.Max(0, kart.ClientSize.Width - baslik.Left * 2);
                baslik.Width = w;
                sayi.Width = w;
            };
            kart.Paint += (s, e) =>
            {
                if (Equals(kart.Tag, true))
                {
                    // Bugünün kartı: üstte turuncu şerit.
                    using var brush = new SolidBrush(VurguRengi);
                    e.Graphics.FillRectangle(brush, 0, 0, kart.ClientSize.Width, kart.LogicalToDeviceUnits(4));
                }
            };
            _gunListeleri[index] = liste;
            _gunSayilari[index] = sayi;
            _gunKartlari[index] = kart;
            return kart;
        }

        protected override void OnLoad(EventArgs e)
        {
            // Pencere çalışma alanına sığmıyorsa küçült (taban sınıf boyutu OnLoad'da kilitler);
            // gün kartları en az KartMinGenislik kalır, sığmazsa yatay kaydırılır.
            Rectangle alan = (Owner != null ? Screen.FromControl(Owner) : Screen.FromPoint(Cursor.Position)).WorkingArea;
            int bosluk = LogicalToDeviceUnits(24);
            var hedef = new Size(Math.Min(Width, alan.Width - bosluk), Math.Min(Height, alan.Height - bosluk));
            if (hedef != Size) Size = hedef;
            pnlGunler.AutoScrollMinSize = new Size(LogicalToDeviceUnits(KartMinGenislik) * 7, 0);
            int bugun = ((int)DateTime.Today.DayOfWeek + 6) % 7; // Pazartesi=0 ... Pazar=6
            _gunKartlari[bugun].Tag = true;
            base.OnLoad(e);
            if (Owner != null && StartPosition == FormStartPosition.CenterParent) CenterToParent();
            ProgramiYukle();
        }

        private async void ProgramiYukle()
        {
            lblOzet.Text = "Yükleniyor…";
            IReadOnlyList<HaftalikProgramGunu> program;
            using (BeginBusy())
            {
                try { program = await _antrenmanService.HaftalikProgramAsync(_sporcuId, LifetimeToken); }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { if (!IsClosedOrClosing) lblOzet.Text = string.Empty; ErrorHandler.Show(ex); return; }
                catch (OperationCanceledException) { return; }
            }
            if (IsClosedOrClosing) return;
            Goster(program ?? Array.Empty<HaftalikProgramGunu>());
        }

        /// <summary>Program verisini kartlara yerleştirir. Gun=1..7 günler, diğerleri (0) atanmamış şeridi.</summary>
        internal void Goster(IReadOnlyList<HaftalikProgramGunu> program)
        {
            SuspendLayout();
            try
            {
                int toplam = 0;
                for (int i = 0; i < 7; i++)
                {
                    var antrenmanlar = program.Where(g => g.Gun == i + 1)
                        .SelectMany(g => g.Antrenmanlar ?? (IReadOnlyList<Antrenman>)Array.Empty<Antrenman>()).ToList();
                    Doldur(_gunListeleri[i], antrenmanlar, sabitGenislik: false);
                    _gunSayilari[i].Text = antrenmanlar.Count == 0 ? "Dinlenme" : antrenmanlar.Count + " antrenman";
                    toplam += antrenmanlar.Count;
                }

                var atanmamis = program.Where(g => g.Gun < 1 || g.Gun > 7)
                    .SelectMany(g => g.Antrenmanlar ?? (IReadOnlyList<Antrenman>)Array.Empty<Antrenman>()).ToList();
                Doldur(flwAtanmamis, atanmamis, sabitGenislik: true);
                lblAtanmamis.Text = string.Format("{0} ({1}) · düzenleme penceresinden bir güne atanabilir",
                    GunComboHelper.AtanmamisMetni, atanmamis.Count);
                pnlAtanmamis.Visible = atanmamis.Count > 0;
                // Şerit yoksa gün kartları alttaki boşluğu da kullanır.
                pnlGunler.Height = (pnlAtanmamis.Visible ? pnlAtanmamis.Top - LogicalToDeviceUnits(8) : ClientSize.Height - LogicalToDeviceUnits(16)) - pnlGunler.Top;

                int hepsi = toplam + atanmamis.Count;
                lblOzet.Text = hepsi == 0 ? string.Empty
                    : string.Format("{0} antrenman · {1} gün dolu", hepsi, _gunListeleri.Count(l => l.Controls.Count > 0));
                if (hepsi == 0)
                {
                    lblBos.Text = "Bu sporcu için henüz antrenman yok." + Environment.NewLine
                        + "Antrenman eklerken bir gün seçerek haftalık program oluşturabilirsiniz.";
                    lblBos.Visible = true;
                    lblBos.BringToFront();
                }
                else lblBos.Visible = false;
            }
            finally { ResumeLayout(true); }
        }

        private void Doldur(FlowLayoutPanel liste, IReadOnlyList<Antrenman> antrenmanlar, bool sabitGenislik)
        {
            liste.SuspendLayout();
            while (liste.Controls.Count > 0) liste.Controls[0].Dispose();
            foreach (Antrenman antrenman in antrenmanlar)
            {
                var oge = new AntrenmanOgesi(antrenman, sabitGenislik ? 3 : 4)
                {
                    BackColor = OgeArkaPlan,
                    ForeColor = Color.White,
                    DetayRengi = IkincilYazi,
                    Margin = new Padding(liste.LogicalToDeviceUnits(4)),
                };
                if (sabitGenislik) oge.Width = liste.LogicalToDeviceUnits(214);
                string ipucu = (antrenman.AntrenmanAdi ?? string.Empty)
                    + (string.IsNullOrWhiteSpace(antrenman.Detaylar) ? string.Empty : Environment.NewLine + antrenman.Detaylar.Trim());
                _ipucu.SetToolTip(oge, ipucu);
                liste.Controls.Add(oge);
            }
            if (!sabitGenislik) OgeGenislikleriniAyarla(liste);
            else foreach (AntrenmanOgesi oge in liste.Controls) oge.YuksekligiAyarla();
            liste.ResumeLayout(true);
        }

        private static void OgeGenislikleriniAyarla(FlowLayoutPanel liste)
        {
            if (liste.FlowDirection != FlowDirection.TopDown) return;
            // Önce tam genişlikle ölçülür; içerik sığmıyorsa dikey kaydırma çubuğuna yer bırakılır
            // (aksi halde çubuk çıkınca yatay kaydırma da doğar).
            var ogeler = liste.Controls.OfType<AntrenmanOgesi>().ToList();
            if (ogeler.Count == 0) return;
            int tam = liste.ClientSize.Width + (liste.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 0);
            void Uygula(int genislik)
            {
                foreach (AntrenmanOgesi oge in ogeler)
                {
                    int w = Math.Max(20, genislik - oge.Margin.Horizontal);
                    if (oge.Width != w) oge.Width = w;
                    oge.YuksekligiAyarla();
                }
            }
            Uygula(tam);
            int yukseklik = ogeler.Sum(o => o.Height + o.Margin.Vertical) + liste.Padding.Vertical;
            if (yukseklik > liste.ClientSize.Height) Uygula(tam - SystemInformation.VerticalScrollBarWidth);
        }

        [System.Runtime.InteropServices.DllImport("uxtheme.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        /// <summary>Koyu temada açık renkli kaydırma çubukları göze batmasın (Windows 10 1809+; eski sürümde etkisiz).</summary>
        private static void KoyuKaydirmaCubugu(Control control)
        {
            void Uygula()
            {
                try { SetWindowTheme(control.Handle, "DarkMode_Explorer", null); }
                catch (Exception ex) when (ex is DllNotFoundException || ex is EntryPointNotFoundException) { }
            }
            if (control.IsHandleCreated) Uygula();
            control.HandleCreated += (s, e) => Uygula();
        }

        private static void KenarCiz(Graphics g, Control control, Color renk, bool dashed)
        {
            using var pen = new Pen(Color.FromArgb(90, renk)) { DashStyle = dashed ? DashStyle.Dash : DashStyle.Solid };
            g.DrawRectangle(pen, 0, 0, control.ClientSize.Width - 1, control.ClientSize.Height - 1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) components.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>Tek antrenman: kalın ad + en fazla birkaç satır detay özeti; yüksekliği metne göre ayarlanır.</summary>
        private sealed class AntrenmanOgesi : Control
        {
            private const TextFormatFlags AdBicimi =
                TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix | TextFormatFlags.TextBoxControl;
            private readonly string _ad;
            private readonly string _detay;
            private readonly int _detaySatir;
            private Font _adFont;

            public AntrenmanOgesi(Antrenman antrenman, int detaySatir)
            {
                _ad = string.IsNullOrWhiteSpace(antrenman.AntrenmanAdi) ? "(adsız)" : antrenman.AntrenmanAdi.Trim();
                _detay = Ozet(antrenman.Detaylar);
                _detaySatir = detaySatir;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
                    | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
                SetStyle(ControlStyles.Selectable, false);
                Cursor = Cursors.Default;
            }

            [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
            public Color DetayRengi { get; set; }

            private Font AdFont => _adFont ??= new Font(Font, FontStyle.Bold);

            private int Ic => LogicalToDeviceUnits(8);

            /// <summary>Satır sonları ve fazla boşluklar tek satırlık özete indirgenir.</summary>
            private static string Ozet(string detay)
            {
                if (string.IsNullOrWhiteSpace(detay)) return string.Empty;
                var parcalar = detay.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => string.Join(" ", p.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)))
                    .Where(p => p.Length > 0);
                return string.Join(" · ", parcalar);
            }

            public void YuksekligiAyarla()
            {
                int w = Math.Max(1, Width - Ic * 2);
                int h = Ic + AdYuksekligi(w);
                if (_detay.Length > 0) h += LogicalToDeviceUnits(2) + DetayYuksekligi(w);
                h += Ic;
                if (Height != h) Height = h;
            }

            private int AdYuksekligi(int w)
            {
                int satir = TextRenderer.MeasureText("Ag", AdFont).Height;
                int olcu = TextRenderer.MeasureText(_ad, AdFont, new Size(w, int.MaxValue), AdBicimi).Height;
                return Math.Min(olcu, satir * 2);
            }

            private int DetayYuksekligi(int w)
            {
                int satir = TextRenderer.MeasureText("Ag", Font).Height;
                int olcu = TextRenderer.MeasureText(_detay, Font, new Size(w, int.MaxValue), AdBicimi).Height;
                return Math.Min(olcu, satir * _detaySatir);
            }

            protected override void OnFontChanged(EventArgs e)
            {
                _adFont?.Dispose();
                _adFont = null;
                base.OnFontChanged(e);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.Clear(Parent?.BackColor is Color pb && pb != Color.Transparent ? pb : KartArkaPlan);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                int r = LogicalToDeviceUnits(6);
                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                using (GraphicsPath path = Yuvarlak(rect, r))
                using (var brush = new SolidBrush(BackColor))
                    g.FillPath(brush, path);
                using (var serit = new SolidBrush(VurguRengi))
                    g.FillRectangle(serit, 0, r, LogicalToDeviceUnits(3), Math.Max(0, Height - r * 2));
                g.SmoothingMode = SmoothingMode.None;

                int w = Math.Max(1, Width - Ic * 2);
                int adH = AdYuksekligi(w);
                TextRenderer.DrawText(g, _ad, AdFont, new Rectangle(Ic, Ic, w, adH), ForeColor, AdBicimi);
                if (_detay.Length > 0)
                {
                    int y = Ic + adH + LogicalToDeviceUnits(2);
                    TextRenderer.DrawText(g, _detay, Font, new Rectangle(Ic, y, w, DetayYuksekligi(w)), DetayRengi, AdBicimi);
                }
            }

            private static GraphicsPath Yuvarlak(Rectangle r, int yaricap)
            {
                int d = Math.Max(1, yaricap * 2);
                var path = new GraphicsPath();
                path.AddArc(r.X, r.Y, d, d, 180, 90);
                path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                return path;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing) _adFont?.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
