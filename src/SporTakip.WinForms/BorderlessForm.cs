using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SporTakip.Theme;

namespace SporTakip
{
    /// <summary>
    /// Uygulamadaki tüm pencerelerin ortak tabanı: kenarlıksız, sabit boyutlu,
    /// sürüklenebilir pencere. Renk ve yazı tipleri Theme klasöründeki token'lardan gelir.
    /// </summary>
    public class BorderlessForm : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int WS_SYSMENU = 0x80000;
        private const int CS_DROPSHADOW = 0x20000;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public BorderlessForm()
        {
            // Kenarlık ayarlarının tek kaynağı (B9): kenarlık yok, boyut sabit,
            // ekranı kaplatma yok. Alt sınıflar bu ayarları tekrar yazmamalı.
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            ControlBox = false;
            DoubleBuffered = true;
            // AutoScaleDimensions (7x17) bu gövde yazı tipine göre; tüm formlar aynı tabanı kullanır.
            Font = ThemeTypography.Govde;
            _lifetimeToken = _lifetime.Token;
        }

        private readonly CancellationTokenSource _lifetime = new CancellationTokenSource();
        // Token kaynak dispose edildikten sonra da güvenle okunabilsin diye baştan saklanır.
        private readonly CancellationToken _lifetimeToken;
        private readonly object _lifetimeLock = new object();
        private bool _lifetimeEnded;
        private readonly Dictionary<Button, bool> _busyStates = new Dictionary<Button, bool>();
        private int _busyCount;

        /// <summary>Form kapanınca iptal edilir; servis çağrılarına verilir. Dispose sonrası da okunabilir.</summary>
        protected CancellationToken LifetimeToken => _lifetimeToken;

        /// <summary>
        /// Form kapatıldı/dispose edildi mi? Await sonrası kontrollere erişmeden ve
        /// MessageBox göstermeden önce bakılır.
        /// </summary>
        protected bool IsClosedOrClosing => IsDisposed || Disposing || _lifetimeToken.IsCancellationRequested;

        /// <summary>
        /// Süren bir işlem boyunca başlık düğmeleri dışındaki tüm butonları devre dışı bırakır;
        /// dönen nesne dispose edilince eski durumlarına döner. İç içe çağrılar sayılır.
        /// </summary>
        protected IDisposable BeginBusy()
        {
            if (_busyCount++ == 0)
            {
                _busyStates.Clear();
                foreach (Button button in ActionButtons(this))
                {
                    _busyStates[button] = button.Enabled;
                    button.Enabled = false;
                }
                UseWaitCursor = true;
            }
            return new BusyScope(this);
        }

        private void EndBusy()
        {
            if (--_busyCount > 0 || IsDisposed) return;
            foreach (KeyValuePair<Button, bool> state in _busyStates)
                if (!state.Key.IsDisposed) state.Key.Enabled = state.Value;
            _busyStates.Clear();
            UseWaitCursor = false;
        }

        private static IEnumerable<Button> ActionButtons(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button && !IsTitleButton(button)) yield return button;
                foreach (Button child in ActionButtons(control)) yield return child;
            }
        }

        private static bool IsTitleButton(Button button) => ThemeApplier.BaslikDugmesiMi(button);

        /// <summary>İşlem form kapandığı için iptal edildiyse true; bu durumda hata gösterilmez.</summary>
        protected bool IsClosingCancellation(Exception exception) =>
            exception is OperationCanceledException && (_lifetimeToken.IsCancellationRequested || IsDisposed);

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            CancelLifetime(dispose: false);
            base.OnFormClosed(e);
        }

        protected override void Dispose(bool disposing)
        {
            // Ana form hem kapanışta WinForms tarafından hem de DI kapsamı tarafından
            // dispose edilebilir; iptal/dispose idempotent olmalı.
            if (disposing) CancelLifetime(dispose: true);
            base.Dispose(disposing);
        }

        private void CancelLifetime(bool dispose)
        {
            lock (_lifetimeLock)
            {
                if (_lifetimeEnded) return;
                if (!_lifetime.IsCancellationRequested) _lifetime.Cancel();
                if (dispose)
                {
                    _lifetime.Dispose();
                    _lifetimeEnded = true;
                }
            }
        }

        private sealed class BusyScope : IDisposable
        {
            private BorderlessForm _owner;

            public BusyScope(BorderlessForm owner) => _owner = owner;

            public void Dispose()
            {
                _owner?.EndBusy();
                _owner = null;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // Görev çubuğundan simge durumuna küçültme/geri yükleme çalışsın.
                cp.Style |= WS_MINIMIZEBOX | WS_SYSMENU;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            // Ölçekleme (DPI) uygulandıktan sonraki son boyutu kilitle.
            LockSize();
            ThemeApplier.Uygula(this);
            base.OnLoad(e);
        }

        protected override void OnDpiChanged(DpiChangedEventArgs e)
        {
            // PerMonitorV2: pencere farklı DPI'lı bir ekrana taşındığında WinForms
            // formu yeniden ölçekler. Kilitli boyut buna engel olmasın; ölçekleme
            // bittikten sonra yeni boyutu tekrar kilitle.
            MinimumSize = Size.Empty;
            MaximumSize = Size.Empty;
            base.OnDpiChanged(e);
            LockSize();
            Invalidate();
        }

        private void LockSize()
        {
            MinimumSize = Size;
            MaximumSize = Size;
        }

        /// <summary>Pencereyi fareyle sürükler (B10).</summary>
        protected void BeginDrag()
        {
            if (!IsHandleCreated) return;
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && e.Clicks == 1) BeginDrag();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            HookDrag(e.Control);
        }

        private void HookDrag(Control control)
        {
            // Etiketler, resimler ve paneller pencerenin boş alanı gibi davranır.
            if (control is Label || control is PictureBox || control is Panel)
            {
                control.MouseDown += DragSource_MouseDown;
                foreach (Control child in control.Controls) HookDrag(child);
            }
        }

        private void DragSource_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1) BeginDrag();
        }

        // ---- Kartlar: ilgili kontrolleri yüzey renginde, yuvarlatılmış bir zemin üzerinde gruplar ----

        private readonly List<Control[]> _kartlar = new List<Control[]>();

        /// <summary>
        /// Verilen kontrollerin çevresine kart çizilir. Kart sınırı kontrollerin kapladığı alandan
        /// hesaplanır; böylece DPI ölçeklemesinde kontrollerle birlikte büyür.
        /// </summary>
        protected void KartEkle(params Control[] icerik)
        {
            if (icerik == null || icerik.Length == 0) return;
            _kartlar.Add(icerik);
            Invalidate();
        }

        private Rectangle KartSiniri(Control[] icerik)
        {
            Rectangle sinir = Rectangle.Empty;
            foreach (Control c in icerik)
                sinir = sinir.IsEmpty ? c.Bounds : Rectangle.Union(sinir, c.Bounds);
            int pay = LogicalToDeviceUnits(ThemeScale.AralikL);
            sinir.Inflate(pay, pay);
            return sinir;
        }

        /// <summary>Kontrol bir kartın içinde mi? (Sahibi-çizimli düğmelerin zemin rengi için.)</summary>
        internal bool KartIcinde(Control c)
        {
            if (c.Parent != this) return false;
            foreach (Control[] kart in _kartlar)
                if (Array.IndexOf(kart, c) >= 0 || KartSiniri(kart).Contains(c.Bounds)) return true;
            return false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ThemePalette p = ThemeManager.Palet;
            Graphics g = e.Graphics;
            float r = LogicalToDeviceUnits(ThemeScale.KoseBuyuk);
            int kayma = LogicalToDeviceUnits(ThemeScale.GolgeKayma);
            int yayilim = LogicalToDeviceUnits(ThemeScale.GolgeYayilim);
            foreach (Control[] kart in _kartlar)
            {
                Rectangle sinir = KartSiniri(kart);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                // Yumuşak gölge: giderek saydamlaşan birkaç katman.
                for (int i = yayilim; i >= 1; i--)
                {
                    var golge = new RectangleF(sinir.X - i + 1, sinir.Y - i + 1 + kayma, sinir.Width + (i - 1) * 2, sinir.Height + (i - 1) * 2);
                    using var yolGolge = ThemeApplier.Yuvarlak(golge, r + i);
                    using var fircaGolge = new SolidBrush(Color.FromArgb(p.Shadow.A / (i + 1), p.Shadow));
                    g.FillPath(fircaGolge, yolGolge);
                }
                using (var yol = ThemeApplier.Yuvarlak(new RectangleF(sinir.X + 0.5f, sinir.Y + 0.5f, sinir.Width - 1, sinir.Height - 1), r))
                {
                    using (var dolgu = new SolidBrush(p.Surface)) g.FillPath(dolgu, yol);
                    using (var kalem = new Pen(p.Border)) g.DrawPath(kalem, yol);
                }
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            }
            // Kenarlığı kapatılan liste ve tabloların çerçevesi token rengiyle çizilir.
            using (var cerceve = new Pen(p.Border))
            {
                foreach (Control c in Controls)
                {
                    bool cerceveli = c.Visible && ((c is ListBox lb && lb.BorderStyle == BorderStyle.None)
                        || (c is DataGridView dgv && dgv.BorderStyle == BorderStyle.None));
                    if (cerceveli) g.DrawRectangle(cerceve, c.Left - 1, c.Top - 1, c.Width + 1, c.Height + 1);
                }
            }
            using (var pen = new Pen(p.BorderStrong))
            {
                g.DrawRectangle(pen, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            }
        }
    }
}
