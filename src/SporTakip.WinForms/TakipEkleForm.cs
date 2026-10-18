using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Domain;

namespace SporTakip
{
    public partial class TakipEkleForm : BorderlessForm
    {
        private readonly ISporcuService _sporcuService;
        private readonly ITakipService _takipService;
        private bool _isLoading;
        private readonly ErrorProvider _errors;

        public TakipEkleForm(ISporcuService sporcuService, ITakipService takipService)
        {
            _sporcuService = sporcuService;
            _takipService = takipService;
            InitializeComponent();
            if (components == null) components = new System.ComponentModel.Container();
            _errors = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
        }

        private async void TakipEkleForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            using (BeginBusy())
            {
                try { await SporcuComboHelper.DoldurAsync(cmbSporcular, _sporcuService, LifetimeToken); }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
                finally { _isLoading = false; }
            }
        }

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            if (_isLoading) return;
            _errors.Clear();
            bool valid = true;
            int? sporcuId = SporcuComboHelper.SeciliSporcuId(cmbSporcular);
            if (sporcuId == null)
            {
                _errors.SetError(cmbSporcular, "Lütfen bir sporcu seçin.");
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(txtAciklama.Text))
            {
                _errors.SetError(txtAciklama, "Lütfen takip açıklamasını girin.");
                valid = false;
            }
            if (!valid) return;
            using (BeginBusy())
            {
                try
                {
                    var takip = new Takip
                    {
                        SporcuId = sporcuId.Value,
                        Tarih = dtTarih.Value.Date,
                        Aciklama = txtAciklama.Text.Trim(),
                    };
                    var result = await _takipService.EkleAsync(takip, LifetimeToken);
                    // Form bu arada kapatıldıysa kontrollere dokunma, kapanmış form için mesaj gösterme.
                    if (IsClosedOrClosing) return;
                    if (!ValidationDisplay.Goster(result, _errors, new Dictionary<string, Control>
                        {
                            [nameof(Takip.SporcuId)] = cmbSporcular,
                            [nameof(Takip.Tarih)] = dtTarih,
                            [nameof(Takip.Aciklama)] = txtAciklama,
                        }, this)) return;
                    MessageBox.Show("Takip kaydı eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAciklama.Clear();
                    dtTarih.Value = DateTime.Today;
                    txtAciklama.Focus();
                }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
            }
        }

        private void btnkapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAsagial_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
