using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Domain;

namespace SporTakip
{
    /// <summary>Var olan bir takip kaydının tarihini ve açıklamasını düzenler; kaydedilirse DialogResult.OK döner.</summary>
    public partial class TakipDuzenleForm : BorderlessForm
    {
        private readonly ITakipService _takipService;
        private readonly ErrorProvider _errors;
        private Takip _takip;

        public TakipDuzenleForm(ITakipService takipService)
        {
            _takipService = takipService;
            InitializeComponent();
            if (components == null) components = new System.ComponentModel.Container();
            _errors = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
            // Çok satırlı kutuda Enter yeni satır ekler; kaydetmek için butona tıklanır veya Ctrl+Enter kullanılır.
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    btnKaydet.PerformClick();
                }
            };
        }

        /// <summary>Pencere gösterilmeden önce düzenlenecek kaydı verir. Kaydın kopyası üzerinde çalışılır.</summary>
        public void Yukle(Takip takip)
        {
            ArgumentNullException.ThrowIfNull(takip);
            _takip = new Takip
            {
                Id = takip.Id,
                SporcuId = takip.SporcuId,
                Tarih = takip.Tarih,
                Aciklama = takip.Aciklama,
            };
            // DateTimePicker'ın aralığı dışındaki (örn. varsayılan) tarihler hata vermesin.
            DateTime tarih = takip.Tarih.Date;
            dtTarih.Value = tarih >= dtTarih.MinDate && tarih <= dtTarih.MaxDate ? tarih : DateTime.Today;
            txtAciklama.Text = takip.Aciklama ?? string.Empty;
        }

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            if (_takip == null || IsClosedOrClosing) return;
            _errors.Clear();
            if (string.IsNullOrWhiteSpace(txtAciklama.Text))
            {
                _errors.SetError(txtAciklama, "Lütfen takip açıklamasını girin.");
                return;
            }
            using (BeginBusy())
            {
                try
                {
                    var guncel = new Takip
                    {
                        Id = _takip.Id,
                        SporcuId = _takip.SporcuId,
                        Tarih = dtTarih.Value.Date,
                        Aciklama = txtAciklama.Text.Trim(),
                    };
                    var result = await _takipService.GuncelleAsync(guncel, LifetimeToken);
                    // Form bu arada kapatıldıysa kontrollere dokunma, kapanmış form için mesaj gösterme.
                    if (IsClosedOrClosing) return;
                    if (!ValidationDisplay.Goster(result, _errors, new Dictionary<string, Control>
                        {
                            [nameof(Takip.Tarih)] = dtTarih,
                            [nameof(Takip.Aciklama)] = txtAciklama,
                        }, this)) return;
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
            }
        }

        private void btnkapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
