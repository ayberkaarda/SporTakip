using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SporTakip
{
    /// <summary>
    /// Uygulamadaki tüm pencerelerin ortak tabanı: kenarlıksız, sabit boyutlu,
    /// sürüklenebilir pencere ve ortak görsel stil tek yerde tanımlanır.
    /// </summary>
    public class BorderlessForm : Form
    {
        public static readonly Color AccentColor = Color.FromArgb(0, 41, 132);
        public static readonly Color BorderColor = Color.FromArgb(0, 41, 132);
        public static readonly Color CloseHoverColor = Color.FromArgb(232, 17, 35);

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
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            MinimumSize = Size;
            MaximumSize = Size;
            StyleTitleButtons();
            base.OnLoad(e);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var pen = new Pen(BorderColor))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            }
        }

        private void StyleTitleButtons()
        {
            foreach (Control control in Controls)
            {
                var button = control as Button;
                if (button == null) continue;

                bool isClose = string.Equals(button.Name, "btnkapat", StringComparison.OrdinalIgnoreCase);
                bool isMinimize = string.Equals(button.Name, "btnAsagial", StringComparison.OrdinalIgnoreCase);
                if (!isClose && !isMinimize) continue;

                button.TabStop = false;
                button.Cursor = Cursors.Hand;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;

                if (isClose)
                {
                    Color restFore = button.ForeColor;
                    button.FlatAppearance.MouseOverBackColor = CloseHoverColor;
                    button.FlatAppearance.MouseDownBackColor = Color.FromArgb(200, 15, 30);
                    button.MouseEnter += (s, ev) => button.ForeColor = Color.White;
                    button.MouseLeave += (s, ev) => button.ForeColor = restFore;
                }
            }
        }
    }
}
