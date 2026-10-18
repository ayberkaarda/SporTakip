using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Application.Validation;
using SporTakip.Domain;
using SporTakip.Theme;

namespace SporTakip
{
    /// <summary>Var olan bir antrenman kaydının adını ve detaylarını düzenler; kaydedilirse DialogResult.OK döner.</summary>
    public partial class AntrenmanDuzenleForm : BorderlessForm
    {
        private readonly IAntrenmanService _antrenmanService;
        private readonly ErrorProvider _errors;
        private Antrenman _antrenman;

        public AntrenmanDuzenleForm(IAntrenmanService antrenmanService)
        {
            _antrenmanService = antrenmanService;
            InitializeComponent();
            GunComboHelper.Doldur(cmbGun);
            txtAntrenmanAdi.MaxLength = ValidationConstants.AntrenmanAdiMaxLength;
            if (components == null) components = new System.ComponentModel.Container();
            _errors = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
            ThemeApplier.HataSaglayici(_errors);
            KartEkle(lblAntrenmanAdi, txtAntrenmanAdi, lblDetaylar, txtDetaylar, lblGun, cmbGun, btnKaydet, btnIptal);
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
        public void Yukle(Antrenman antrenman)
        {
            ArgumentNullException.ThrowIfNull(antrenman);
            _antrenman = new Antrenman
            {
                Id = antrenman.Id,
                SporcuId = antrenman.SporcuId,
                AntrenmanAdi = antrenman.AntrenmanAdi,
                Detaylar = antrenman.Detaylar,
                Gun = antrenman.Gun,
            };
            txtAntrenmanAdi.Text = antrenman.AntrenmanAdi ?? string.Empty;
            txtDetaylar.Text = antrenman.Detaylar ?? string.Empty;
            GunComboHelper.Sec(cmbGun, antrenman.Gun);
        }

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            if (_antrenman == null || IsClosedOrClosing) return;
            _errors.Clear();
            bool valid = true;
            if (string.IsNullOrWhiteSpace(txtAntrenmanAdi.Text))
            {
                _errors.SetError(txtAntrenmanAdi, "Lütfen antrenman adını girin.");
                valid = false;
            }
            // Ekleme formuyla aynı kural (prompt.md Faz 1 B7): form detay ister.
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
                    var guncel = new Antrenman
                    {
                        Id = _antrenman.Id,
                        SporcuId = _antrenman.SporcuId,
                        AntrenmanAdi = txtAntrenmanAdi.Text.Trim(),
                        Detaylar = txtDetaylar.Text.Trim(),
                        Gun = GunComboHelper.SeciliGun(cmbGun),
                    };
                    var result = await _antrenmanService.GuncelleAsync(guncel, LifetimeToken);
                    // Form bu arada kapatıldıysa kontrollere dokunma, kapanmış form için mesaj gösterme.
                    if (IsClosedOrClosing) return;
                    if (!ValidationDisplay.Goster(result, _errors, new Dictionary<string, Control>
                        {
                            [nameof(Antrenman.AntrenmanAdi)] = txtAntrenmanAdi,
                            [nameof(Antrenman.Detaylar)] = txtDetaylar,
                            [nameof(Antrenman.Gun)] = cmbGun,
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
