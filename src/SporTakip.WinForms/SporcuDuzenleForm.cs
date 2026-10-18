using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Application.Validation;
using SporTakip.Domain;

namespace SporTakip
{
    /// <summary>Var olan bir sporcunun adını ve spor dalını düzenler; kaydedilirse DialogResult.OK döner.</summary>
    public partial class SporcuDuzenleForm : BorderlessForm
    {
        private readonly ISporcuService _sporcuService;
        private readonly ErrorProvider _errors;
        private int? _sporcuId;

        public SporcuDuzenleForm(ISporcuService sporcuService)
        {
            _sporcuService = sporcuService;
            InitializeComponent();
            txtAd.MaxLength = ValidationConstants.SporcuAdMaxLength;
            txtSporDali.MaxLength = ValidationConstants.SporDaliMaxLength;
            if (components == null) components = new System.ComponentModel.Container();
            _errors = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
        }

        /// <summary>Pencere gösterilmeden önce düzenlenecek sporcuyu verir.</summary>
        public void Yukle(Sporcu sporcu)
        {
            ArgumentNullException.ThrowIfNull(sporcu);
            _sporcuId = sporcu.Id;
            txtAd.Text = sporcu.Ad ?? string.Empty;
            txtSporDali.Text = sporcu.SporDali ?? string.Empty;
        }

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            if (_sporcuId == null || IsClosedOrClosing) return;
            _errors.Clear();
            bool valid = true;
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                _errors.SetError(txtAd, "Lütfen sporcu adını girin.");
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(txtSporDali.Text))
            {
                _errors.SetError(txtSporDali, "Lütfen spor dalını girin.");
                valid = false;
            }
            if (!valid) return;
            using (BeginBusy())
            {
                try
                {
                    var sporcu = new Sporcu { Id = _sporcuId.Value, Ad = txtAd.Text.Trim(), SporDali = txtSporDali.Text.Trim() };
                    var result = await _sporcuService.GuncelleAsync(sporcu, LifetimeToken);
                    // Form bu arada kapatıldıysa kontrollere dokunma, kapanmış form için mesaj gösterme.
                    if (IsClosedOrClosing) return;
                    if (!ValidationDisplay.Goster(result, _errors, new Dictionary<string, Control>
                        {
                            [nameof(Sporcu.Ad)] = txtAd,
                            [nameof(Sporcu.SporDali)] = txtSporDali,
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
