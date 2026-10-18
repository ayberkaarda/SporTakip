using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SporTakip
{
    /// <summary>
    /// Açılışta şema güncellemesi sürerken gösterilen küçük kenarlıksız bekleme penceresi.
    /// Verilen iş bitince kendiliğinden kapanır; kullanıcı iş bitmeden kapatamaz.
    /// </summary>
    internal sealed class VeritabaniHazirlikForm : BorderlessForm
    {
        private readonly Task _is;

        public VeritabaniHazirlikForm(Task bekle)
        {
            _is = bekle ?? throw new ArgumentNullException(nameof(bekle));
            SuspendLayout();
            var lblMesaj = new Label
            {
                AutoSize = false,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 162),
                ForeColor = AccentColor,
                Location = new Point(20, 18),
                Name = "lblMesaj",
                Size = new Size(280, 24),
                Text = "Veritabanı hazırlanıyor…",
                TextAlign = ContentAlignment.MiddleLeft,
            };
            var progress = new ProgressBar
            {
                Location = new Point(20, 52),
                MarqueeAnimationSpeed = 30,
                Name = "progress",
                Size = new Size(280, 8),
                Style = ProgressBarStyle.Marquee,
            };
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(320, 80);
            Controls.Add(progress);
            Controls.Add(lblMesaj);
            Name = "VeritabaniHazirlikForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Spor Istanbul | Veritabanı hazırlanıyor";
            ResumeLayout(false);
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Sonuç (hata dahil) çağıran tarafından görevden okunur; burada yalnızca bitişi beklenir.
            try { await _is; }
            catch (Exception) { }
            if (!IsClosedOrClosing) Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_is.IsCompleted && e.CloseReason == CloseReason.UserClosing) e.Cancel = true;
            base.OnFormClosing(e);
        }
    }
}
