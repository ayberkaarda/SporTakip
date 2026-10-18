using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Domain;

namespace SporTakip
{
    public partial class AntrenmanEkleForm : BorderlessForm
    {
        private readonly ISporcuService _sporcuService;
        private readonly IAntrenmanService _antrenmanService;
        private bool _isLoading;
        private readonly ErrorProvider _errors;

        public AntrenmanEkleForm(ISporcuService sporcuService, IAntrenmanService antrenmanService)
        {
            _sporcuService = sporcuService;
            _antrenmanService = antrenmanService;
            InitializeComponent();
            GunComboHelper.Doldur(cmbGun);
            if (components == null) components = new System.ComponentModel.Container();
            _errors = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
        }

        private async void AntrenmanEkleForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            using (BeginBusy())
            {
                try { await SporcuComboHelper.DoldurAsync(comboSporcular, _sporcuService, LifetimeToken); }
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
            int? sporcuId = SporcuComboHelper.SeciliSporcuId(comboSporcular);
            if (sporcuId == null)
            {
                _errors.SetError(comboSporcular, "Lütfen bir sporcu seçin.");
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(txtAntrenmanName.Text))
            {
                _errors.SetError(txtAntrenmanName, "Lütfen antrenman adını girin.");
                valid = false;
            }
            // Bilinçli: servis Detaylar'ı opsiyonel kabul etse de form (prompt.md Faz 1 B7) detay ister.
            if (string.IsNullOrWhiteSpace(txtDetaylar.Text))
            {
                _errors.SetError(txtDetaylar, "Lütfen antrenman detaylarını girin.");
                valid = false;
            }
            if (!valid) return;
            using (BeginBusy())
            {
                try
                {
                    var antrenman = new Antrenman
                    {
                        SporcuId = sporcuId.Value,
                        AntrenmanAdi = txtAntrenmanName.Text.Trim(),
                        Detaylar = txtDetaylar.Text.Trim(),
                        Gun = GunComboHelper.SeciliGun(cmbGun),
                    };
                    var result = await _antrenmanService.EkleAsync(antrenman, LifetimeToken);
                    // Form bu arada kapatıldıysa kontrollere dokunma, kapanmış form için mesaj gösterme.
                    if (IsClosedOrClosing) return;
                    if (!ValidationDisplay.Goster(result, _errors, new Dictionary<string, Control>
                        {
                            [nameof(Antrenman.SporcuId)] = comboSporcular,
                            [nameof(Antrenman.AntrenmanAdi)] = txtAntrenmanName,
                            [nameof(Antrenman.Detaylar)] = txtDetaylar,
                            [nameof(Antrenman.Gun)] = cmbGun,
                        }, this)) return;
                    MessageBox.Show("Antrenman kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAntrenmanName.Clear();
                    txtDetaylar.Clear();
                    GunComboHelper.Sec(cmbGun, null);
                    txtAntrenmanName.Focus();
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
