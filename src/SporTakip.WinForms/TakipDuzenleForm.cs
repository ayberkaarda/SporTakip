using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Application.Validation;
using SporTakip.Domain;

namespace SporTakip
{
    /// <summary>
    /// Var olan bir takip kaydının tarihini ve açıklamasını düzenler; kaydedilirse DialogResult.OK döner.
    /// Takibin ölçümleri aynı pencerede eklenir/güncellenir/silinir; bu işlemler hemen kaydedilir.
    /// </summary>
    public partial class TakipDuzenleForm : BorderlessForm
    {
        private readonly ITakipService _takipService;
        private readonly IOlcumService _olcumService;
        private readonly ErrorProvider _errors;
        private Takip _takip;
        private bool _listeBaglaniyor;

        public TakipDuzenleForm(ITakipService takipService, IOlcumService olcumService)
        {
            _takipService = takipService;
            _olcumService = olcumService;
            InitializeComponent();
            if (components == null) components = new System.ComponentModel.Container();
            _errors = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
            OlcumUi.GridHazirla(dgvOlcumler);
            OlcumUi.BirimleriDoldur(cmbOlcumBirim);
            OlcumUi.IpucuAyarla(cmbOlcumTur, "Tür");
            OlcumUi.IpucuAyarla(cmbOlcumBirim, "Birim");
            dgvOlcumler.SelectionChanged += dgvOlcumler_SelectionChanged;
            // Ölçüm kutularında Enter yeni ölçümü ekler; diğer alanlarda takibi kaydeder.
            OlcumUi.EnterIleEkle(this, btnOlcumEkle, btnKaydet, cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim);
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

        /// <summary>Pencere açıkken ölçüm eklendi, güncellendi veya silindiyse true (takip kaydedilmese de).</summary>
        public bool OlcumlerDegisti { get; private set; }

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

        private async void TakipDuzenleForm_Load(object sender, EventArgs e)
        {
            if (_takip == null) return;
            using (BeginBusy())
            {
                try
                {
                    await OlcumUi.TurleriDoldurAsync(cmbOlcumTur, _olcumService, _takip.SporcuId, LifetimeToken);
                    await OlcumleriListeleAsync(null);
                }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
            }
        }

        /// <param name="seciliId">Yenilemeden sonra seçili gelecek ölçüm; null ise seçim olmaz ve giriş kutuları boş kalır.</param>
        private async Task OlcumleriListeleAsync(int? seciliId)
        {
            var olcumler = await _olcumService.TakipOlcumleriAsync(_takip.Id, LifetimeToken);
            if (IsClosedOrClosing) return;
            _listeBaglaniyor = true;
            try
            {
                dgvOlcumler.DataSource = olcumler.ToList();
                dgvOlcumler.ClearSelection();
                dgvOlcumler.CurrentCell = null;
            }
            finally { _listeBaglaniyor = false; }
            foreach (DataGridViewRow row in dgvOlcumler.Rows)
            {
                if (row.DataBoundItem is Olcum o && o.Id == seciliId)
                {
                    dgvOlcumler.CurrentCell = row.Cells[0];
                    return;
                }
            }
        }

        // SelectionChanged sırasında CurrentRow henüz güncellenmemiş olabilir; seçili satırlardan okunur.
        private Olcum SeciliOlcum() =>
            dgvOlcumler.SelectedRows.Count > 0 ? dgvOlcumler.SelectedRows[0].DataBoundItem as Olcum : null;

        private void dgvOlcumler_SelectionChanged(object sender, EventArgs e)
        {
            if (_listeBaglaniyor) return;
            Olcum secili = SeciliOlcum();
            if (secili == null) return;
            // Seçilen ölçüm düzenlenmek üzere giriş kutularına gelir.
            _errors.Clear();
            cmbOlcumTur.Text = secili.Tur;
            txtOlcumDeger.Text = OlcumUi.DegerMetni(secili.Deger);
            cmbOlcumBirim.Text = secili.Birim ?? string.Empty;
        }

        private async void btnOlcumEkle_Click(object sender, EventArgs e)
        {
            if (_takip == null || IsClosedOrClosing) return;
            Olcum olcum = OlcumUi.GirisiDogrula(_olcumService, cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim, _errors, this);
            if (olcum == null) return;
            olcum.TakipId = _takip.Id;
            await OlcumKaydetAsync(async token =>
            {
                var sonuc = await _olcumService.EkleAsync(olcum, token);
                return (sonuc, sonuc.IsValid ? sonuc.Value : (int?)null);
            });
        }

        private async void btnOlcumGuncelle_Click(object sender, EventArgs e)
        {
            if (_takip == null || IsClosedOrClosing) return;
            Olcum secili = SeciliOlcum();
            if (secili == null)
            {
                MessageBox.Show(this, "Güncellemek için listeden bir ölçüm seçin.", "Seçim gerekli",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Olcum olcum = OlcumUi.GirisiDogrula(_olcumService, cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim, _errors, this);
            if (olcum == null) return;
            olcum.Id = secili.Id;
            olcum.TakipId = _takip.Id;
            await OlcumKaydetAsync(async token =>
            {
                ValidationResult sonuc = await _olcumService.GuncelleAsync(olcum, token);
                return (sonuc, (int?)olcum.Id);
            });
        }

        private async Task OlcumKaydetAsync(
            Func<System.Threading.CancellationToken, Task<(ValidationResult Sonuc, int? Id)>> kaydet)
        {
            using (BeginBusy())
            {
                try
                {
                    var (sonuc, id) = await kaydet(LifetimeToken);
                    if (IsClosedOrClosing) return;
                    if (!ValidationDisplay.Goster(sonuc, _errors,
                        OlcumUi.Alanlar(cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim), this)) return;
                    OlcumlerDegisti = true;
                    // Kaydedilen satır görünür olsun diye güncel satır yapılır.
                    await OlcumleriListeleAsync(id);
                    await OlcumUi.TurleriDoldurAsync(cmbOlcumTur, _olcumService, _takip.SporcuId, LifetimeToken);
                    if (IsClosedOrClosing) return;
                    // Yeni ölçüm girişine hazır olsun; seçim giriş kutularını tekrar doldurmasın.
                    dgvOlcumler.ClearSelection();
                    OlcumUi.GirisiTemizle(cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim);
                    cmbOlcumTur.Focus();
                }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
            }
        }

        private async void btnOlcumSil_Click(object sender, EventArgs e)
        {
            await OlcumSilAsync();
        }

        private async void dgvOlcumler_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                await OlcumSilAsync();
            }
        }

        private async Task OlcumSilAsync()
        {
            if (_takip == null || IsClosedOrClosing) return;
            Olcum secili = SeciliOlcum();
            if (secili == null)
            {
                MessageBox.Show(this, "Silmek için listeden bir ölçüm seçin.", "Seçim gerekli",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string mesaj = string.Format("\"{0}\" ölçümü silinecek. Devam edilsin mi?",
                OlcumUi.Ozet(secili.Tur, secili.Deger, secili.Birim));
            if (MessageBox.Show(this, mesaj, "Silme onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            using (BeginBusy())
            {
                try
                {
                    bool silindi = await _olcumService.SilAsync(secili.Id, LifetimeToken);
                    if (IsClosedOrClosing) return;
                    OlcumlerDegisti = true;
                    if (!silindi)
                        MessageBox.Show(this, "Ölçüm bulunamadı (başka bir yerden silinmiş olabilir). Liste yenileniyor.",
                            "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _errors.Clear();
                    OlcumUi.GirisiTemizle(cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim);
                    await OlcumleriListeleAsync(null);
                }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
            }
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
