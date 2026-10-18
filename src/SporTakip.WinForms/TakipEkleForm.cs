using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Domain;
using SporTakip.Theme;

namespace SporTakip
{
    public partial class TakipEkleForm : BorderlessForm
    {
        private readonly ISporcuService _sporcuService;
        private readonly ITakipService _takipService;
        private readonly IOlcumService _olcumService;
        private bool _isLoading;
        private int _turIstegi;
        private readonly ErrorProvider _errors;
        // Takip kaydedilince sırayla eklenecek ölçümler (TakipId henüz yok).
        private readonly BindingList<Olcum> _bekleyenOlcumler = new BindingList<Olcum>();
        // Takip + ölçüm kaydı sürerken pencere kapanmaz (yarım kayıt olmasın); kapatma isteği sonra uygulanır.
        private bool _kayitSuruyor;
        private bool _kapatmaIstendi;

        public TakipEkleForm(ISporcuService sporcuService, ITakipService takipService, IOlcumService olcumService)
        {
            _sporcuService = sporcuService;
            _takipService = takipService;
            _olcumService = olcumService;
            InitializeComponent();
            if (components == null) components = new System.ComponentModel.Container();
            _errors = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
            ThemeApplier.HataSaglayici(_errors);
            ThemeApplier.Rol(lblOlcumBaslik, KontrolRolu.Bolum);
            ThemeApplier.Rol(btnOlcumCikar, KontrolRolu.Tehlike);
            KartEkle(lblSporcu, cmbSporcular, lblTarih, dtTarih, label1, txtAciklama, btnKaydet);
            KartEkle(lblOlcumBaslik, lblOlcumTur, cmbOlcumTur, lblOlcumDeger, txtOlcumDeger, lblOlcumBirim, cmbOlcumBirim,
                btnOlcumEkle, btnOlcumCikar, dgvOlcumler);
            OlcumUi.GridHazirla(dgvOlcumler);
            OlcumUi.BirimleriDoldur(cmbOlcumBirim);
            dgvOlcumler.DataSource = _bekleyenOlcumler;
            // Ölçüm kutularında Enter ölçümü listeye ekler; diğer alanlarda takibi kaydeder.
            OlcumUi.EnterIleEkle(this, btnOlcumEkle, btnKaydet, cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim);
        }

        private async void TakipEkleForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            using (BeginBusy())
            {
                try { await SporcuComboHelper.DoldurAsync(cmbSporcular, _sporcuService, LifetimeToken); }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { return; }
                finally { _isLoading = false; }
            }
            if (IsClosedOrClosing) return;
            cmbSporcular.SelectedIndexChanged += cmbSporcular_SelectedIndexChanged;
            await TurOnerileriniYukleAsync();
        }

        private async void cmbSporcular_SelectedIndexChanged(object sender, EventArgs e)
        {
            await TurOnerileriniYukleAsync();
        }

        /// <summary>Seçili sporcunun daha önce kullandığı ölçüm türlerini öneri olarak getirir.</summary>
        private async System.Threading.Tasks.Task TurOnerileriniYukleAsync()
        {
            if (_isLoading || IsClosedOrClosing) return;
            int istek = ++_turIstegi;
            int? sporcuId = SporcuComboHelper.SeciliSporcuId(cmbSporcular);
            if (sporcuId == null) return;
            try
            {
                var turler = await _olcumService.TurleriAsync(sporcuId.Value, LifetimeToken);
                // Seçim bu arada değiştiyse eski sonucu gösterme.
                if (istek != _turIstegi || IsClosedOrClosing) return;
                string yazilan = cmbOlcumTur.Text;
                cmbOlcumTur.BeginUpdate();
                cmbOlcumTur.Items.Clear();
                cmbOlcumTur.Items.AddRange(turler.Cast<object>().ToArray());
                cmbOlcumTur.EndUpdate();
                cmbOlcumTur.Text = yazilan;
            }
            catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
            catch (OperationCanceledException) { }
        }

        private void btnOlcumEkle_Click(object sender, EventArgs e)
        {
            if (_kayitSuruyor || IsClosedOrClosing) return;
            Olcum olcum = OlcumUi.GirisiDogrula(_olcumService, cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim, _errors, this);
            if (olcum == null) return;
            _bekleyenOlcumler.Add(olcum);
            OlcumUi.GirisiTemizle(cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim);
            cmbOlcumTur.Focus();
        }

        private void btnOlcumCikar_Click(object sender, EventArgs e)
        {
            SeciliOlcumuCikar();
        }

        private void dgvOlcumler_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                SeciliOlcumuCikar();
            }
        }

        private void SeciliOlcumuCikar()
        {
            if (_kayitSuruyor) return;
            if (dgvOlcumler.CurrentRow?.DataBoundItem is Olcum secili)
                _bekleyenOlcumler.Remove(secili);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Windows kapanırken engellenemez; diğer durumlarda kayıt bitince kapanır.
            if (_kayitSuruyor && e.CloseReason != CloseReason.WindowsShutDown && e.CloseReason != CloseReason.TaskManagerClosing)
            {
                e.Cancel = true;
                _kapatmaIstendi = true;
            }
            base.OnFormClosing(e);
        }

        /// <summary>Kayıt sürerken ölçüm listesi ve giriş kutuları da kilitlenir (BeginBusy yalnız butonları kapatır).</summary>
        private void KayitKilidi(bool kilitli)
        {
            _kayitSuruyor = kilitli;
            foreach (Control c in new Control[] { dgvOlcumler, cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim, cmbSporcular, dtTarih, txtAciklama })
                if (!c.IsDisposed) c.Enabled = !kilitli;
        }

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            if (_isLoading || _kayitSuruyor) return;
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
            // Kutuda yazılı kalmış ama listeye eklenmemiş ölçüm sessizce kaybolmasın.
            if (!string.IsNullOrWhiteSpace(cmbOlcumTur.Text) || !string.IsNullOrWhiteSpace(txtOlcumDeger.Text))
            {
                var cevap = MessageBox.Show(this,
                    "Ölçüm alanlarına yazılan değer henüz listeye eklenmedi. Kaydetmeden önce eklensin mi?",
                    "Eklenmemiş ölçüm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (cevap == DialogResult.Cancel) return;
                if (cevap == DialogResult.Yes)
                {
                    Olcum olcum = OlcumUi.GirisiDogrula(_olcumService, cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim, _errors, this);
                    if (olcum == null) return;
                    _bekleyenOlcumler.Add(olcum);
                }
                OlcumUi.GirisiTemizle(cmbOlcumTur, txtOlcumDeger, cmbOlcumBirim);
            }
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
                    List<string> kaydedilemeyen;
                    KayitKilidi(true);
                    try
                    {
                        var result = await _takipService.EkleAsync(takip, LifetimeToken);
                        // Form bu arada kapatıldıysa kontrollere dokunma, kapanmış form için mesaj gösterme.
                        if (IsClosedOrClosing) return;
                        if (!ValidationDisplay.Goster(result, _errors, new Dictionary<string, Control>
                            {
                                [nameof(Takip.SporcuId)] = cmbSporcular,
                                [nameof(Takip.Tarih)] = dtTarih,
                                [nameof(Takip.Aciklama)] = txtAciklama,
                            }, this)) return;

                        // Takip kaydedildi; ölçümler onun Id'siyle, pencere kapanışından bağımsız eklenir.
                        // Bir ölçüm başarısız olsa da takip tekrar kaydedilmesin diye form sıfırlanır ve eksikler bildirilir.
                        kaydedilemeyen = await OlcumleriKaydetAsync(result.Value);
                    }
                    finally
                    {
                        KayitKilidi(false);
                    }
                    if (IsDisposed) return;
                    string eksikMesaji = "Takip kaydı eklendi, ancak şu ölçümler kaydedilemedi:" + Environment.NewLine
                        + string.Join(Environment.NewLine, kaydedilemeyen) + Environment.NewLine + Environment.NewLine
                        + "Bunları Sporcu Detayı > Takip Düzenle ekranından ekleyebilirsiniz.";
                    if (_kapatmaIstendi)
                    {
                        // Kayıt sırasında ertelenen kapatma isteği şimdi uygulanır (aşağıdaki blokta).
                        if (kaydedilemeyen.Count > 0)
                            MessageBox.Show(this, eksikMesaji, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _bekleyenOlcumler.Clear();
                    }
                    else
                    {
                        if (kaydedilemeyen.Count == 0)
                            MessageBox.Show(this, "Takip kaydı eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show(this, eksikMesaji, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAciklama.Clear();
                        dtTarih.Value = DateTime.Today;
                        _bekleyenOlcumler.Clear();
                        txtAciklama.Focus();
                    }
                }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
            }
            if (_kapatmaIstendi && !IsClosedOrClosing)
            {
                // Kayıt sırasında ertelenen kapatma isteği (kayıt başarılı, hatalı ya da doğrulamada kalmış olsun).
                BeginInvoke(new Action(Close));
                return;
            }
            await TurOnerileriniYukleAsync();
        }

        /// <returns>Kaydedilemeyen ölçümlerin açıklamaları.</returns>
        private async System.Threading.Tasks.Task<List<string>> OlcumleriKaydetAsync(int takipId)
        {
            var kaydedilemeyen = new List<string>();
            foreach (Olcum bekleyen in _bekleyenOlcumler.ToList())
            {
                string ozet = OlcumUi.Ozet(bekleyen.Tur, bekleyen.Deger, bekleyen.Birim);
                try
                {
                    var olcum = new Olcum { TakipId = takipId, Tur = bekleyen.Tur, Deger = bekleyen.Deger, Birim = bekleyen.Birim };
                    // Takip zaten yazıldı; ölçümler pencere kapanış token'ı ile yarıda kesilmez.
                    var sonuc = await _olcumService.EkleAsync(olcum, CancellationToken.None);
                    if (!sonuc.IsValid)
                        kaydedilemeyen.Add(ozet + " (" + string.Join(" ", sonuc.Errors.Select(h => h.Message)) + ")");
                }
                catch (Exception ex)
                {
                    ErrorHandler.Log(ex);
                    kaydedilemeyen.Add(ozet + " (veritabanı hatası)");
                }
            }
            return kaydedilemeyen;
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
