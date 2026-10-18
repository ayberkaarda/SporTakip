using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Domain;

namespace SporTakip
{
    public partial class SporcuDetayForm : BorderlessForm
    {
        private readonly ISporcuService _sporcuService;
        private readonly IAntrenmanService _antrenmanService;
        private readonly ITakipService _takipService;
        private readonly IOlcumService _olcumService;
        private readonly IRaporService _raporService;
        private readonly IServiceScopeFactory _scopeFactory;
        private bool _isLoading;
        private int _detayIstegi;
        // Takip Id -> "Kilo: 95 kg, Boy: 180 cm"; Ölçümler sütunu CellFormatting ile buradan doldurulur.
        private Dictionary<int, string> _olcumOzetleri = new Dictionary<int, string>();

        public SporcuDetayForm(ISporcuService sporcuService, IAntrenmanService antrenmanService, ITakipService takipService,
            IOlcumService olcumService, IRaporService raporService, IServiceScopeFactory scopeFactory)
        {
            _sporcuService = sporcuService;
            _antrenmanService = antrenmanService;
            _takipService = takipService;
            _olcumService = olcumService;
            _raporService = raporService;
            _scopeFactory = scopeFactory;
            InitializeComponent();
            // Sütunlar Designer'da tanımlı; DataSource'un özelliklerinden yeni sütun üretilmesin.
            dgvAntrenmanlar.AutoGenerateColumns = false;
            dgvTakipler.AutoGenerateColumns = false;
            dgvTakipler.CellFormatting += dgvTakipler_CellFormatting;
        }

        private async void SporcuDetayForm_Load(object sender, EventArgs e)
        {
            cmbSporcular.SelectedIndexChanged -= cmbSporcular_SelectedIndexChanged;
            cmbSporcular.SelectedIndexChanged += cmbSporcular_SelectedIndexChanged;
            _isLoading = true;
            using (BeginBusy())
            {
                try { await SporcuComboHelper.DoldurAsync(cmbSporcular, _sporcuService, LifetimeToken); }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { return; }
                finally { _isLoading = false; }
            }
            await ListeleDetaylarAsync();
        }

        private async Task ListeleDetaylarAsync()
        {
            if (_isLoading || IsClosedOrClosing) return;
            // Token baştan alınır; ikinci sorgu sırasında kaynak dispose edilmiş olsa da güvenli.
            CancellationToken token = LifetimeToken;
            // Seçim hızlı değişirse yalnızca son isteğin sonucu gösterilir.
            int istek = ++_detayIstegi;
            // Yenileme sonrası aynı kayıt seçili kalsın.
            int? seciliAntrenmanId = SeciliAntrenman()?.Id;
            int? seciliTakipId = SeciliTakip()?.Id;
            dgvAntrenmanlar.DataSource = null;
            dgvTakipler.DataSource = null;
            int? sporcuId = SporcuComboHelper.SeciliSporcuId(cmbSporcular);
            if (sporcuId == null) return;
            using (BeginBusy())
            {
                try
                {
                    var antrenmanlar = await _antrenmanService.SporcuyaGoreListeleAsync(sporcuId.Value, token);
                    var takipler = await _takipService.SporcuyaGoreListeleAsync(sporcuId.Value, token);
                    var olcumler = await _olcumService.SporcuOlcumleriAsync(sporcuId.Value, null, token);
                    if (istek != _detayIstegi || IsClosedOrClosing) return;
                    _olcumOzetleri = olcumler.GroupBy(o => o.TakipId)
                        .ToDictionary(g => g.Key, g => OlcumUi.Ozet(g.OrderBy(o => o.Id)));
                    dgvAntrenmanlar.DataSource = antrenmanlar.ToList();
                    dgvTakipler.DataSource = takipler.ToList();
                    SatiriSec(dgvAntrenmanlar, seciliAntrenmanId, (Antrenman a) => a.Id);
                    SatiriSec(dgvTakipler, seciliTakipId, (Takip t) => t.Id);
                }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
            }
        }

        private static void SatiriSec<T>(DataGridView grid, int? id, Func<T, int> idSecici) where T : class
        {
            if (id == null) return;
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.DataBoundItem is T kayit && idSecici(kayit) == id.Value)
                {
                    grid.CurrentCell = row.Cells[0];
                    return;
                }
            }
        }

        private void dgvTakipler_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colOlcumler.Index) return;
            if (dgvTakipler.Rows[e.RowIndex].DataBoundItem is Takip takip)
            {
                e.Value = _olcumOzetleri.TryGetValue(takip.Id, out string ozet) ? ozet : string.Empty;
                e.FormattingApplied = true;
            }
        }

        private Antrenman SeciliAntrenman() => dgvAntrenmanlar.CurrentRow?.DataBoundItem as Antrenman;

        private Takip SeciliTakip() => dgvTakipler.CurrentRow?.DataBoundItem as Takip;

        private async void cmbSporcular_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ListeleDetaylarAsync();
        }

        private async void btnGuncelle_Click(object sender, EventArgs e)
        {
            await ListeleDetaylarAsync();
        }

        // ---- Antrenman düzenleme/silme ----

        private async void dgvAntrenmanlar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // başlık satırı
            await AntrenmanDuzenleAsync();
        }

        private async void dgvAntrenmanlar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                await AntrenmanSilAsync();
            }
        }

        private async void btnAntrenmanDuzenle_Click(object sender, EventArgs e)
        {
            await AntrenmanDuzenleAsync();
        }

        private async void btnAntrenmanSil_Click(object sender, EventArgs e)
        {
            await AntrenmanSilAsync();
        }

        private async Task AntrenmanDuzenleAsync()
        {
            if (IsClosedOrClosing) return;
            Antrenman secili = SeciliAntrenman();
            if (secili == null)
            {
                MessageBox.Show(this, "Düzenlemek için bir antrenman seçin.", "Seçim gerekli",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (DuzenlemePenceresiAc<AntrenmanDuzenleForm>(frm => frm.Yukle(secili)))
                await ListeleDetaylarAsync();
        }

        private async Task AntrenmanSilAsync()
        {
            if (IsClosedOrClosing) return;
            Antrenman secili = SeciliAntrenman();
            if (secili == null)
            {
                MessageBox.Show(this, "Silmek için bir antrenman seçin.", "Seçim gerekli",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!SilmeOnayla(string.Format("\"{0}\" antrenmanı silinecek. Devam edilsin mi?", secili.AntrenmanAdi))) return;
            await SilVeYenileAsync(token => _antrenmanService.SilAsync(secili.Id, token));
        }

        // ---- Takip düzenleme/silme ----

        private async void dgvTakipler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // başlık satırı
            await TakipDuzenleAsync();
        }

        private async void dgvTakipler_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                await TakipSilAsync();
            }
        }

        private async void btnTakipDuzenle_Click(object sender, EventArgs e)
        {
            await TakipDuzenleAsync();
        }

        private async void btnTakipSil_Click(object sender, EventArgs e)
        {
            await TakipSilAsync();
        }

        private async Task TakipDuzenleAsync()
        {
            if (IsClosedOrClosing) return;
            Takip secili = SeciliTakip();
            if (secili == null)
            {
                MessageBox.Show(this, "Düzenlemek için bir takip kaydı seçin.", "Seçim gerekli",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Ölçümler pencerede hemen kaydedilir; takip kaydedilmeden kapatılsa da liste yenilenmeli.
            bool olcumlerDegisti = false;
            bool kaydedildi = DuzenlemePenceresiAc<TakipDuzenleForm>(frm =>
            {
                frm.Yukle(secili);
                frm.FormClosed += (s, e) => olcumlerDegisti = frm.OlcumlerDegisti;
            });
            if ((kaydedildi || olcumlerDegisti) && !IsClosedOrClosing)
                await ListeleDetaylarAsync();
        }

        private async Task TakipSilAsync()
        {
            if (IsClosedOrClosing) return;
            Takip secili = SeciliTakip();
            if (secili == null)
            {
                MessageBox.Show(this, "Silmek için bir takip kaydı seçin.", "Seçim gerekli",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!SilmeOnayla(string.Format("{0:d} tarihli takip kaydı silinecek. Devam edilsin mi?", secili.Tarih))) return;
            await SilVeYenileAsync(token => _takipService.SilAsync(secili.Id, token));
        }

        // ---- Grafik ve CSV ----

        private Sporcu SeciliSporcu() => cmbSporcular.SelectedItem as Sporcu;

        private void btnGrafik_Click(object sender, EventArgs e)
        {
            if (IsClosedOrClosing) return;
            Sporcu sporcu = SeciliSporcu();
            if (sporcu == null)
            {
                MessageBox.Show(this, "Grafik için bir sporcu seçin.", "Seçim gerekli",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DuzenlemePenceresiAc<OlcumGrafikForm>(frm => frm.Yukle(sporcu.Id, sporcu.Ad));
        }

        private async void btnCsvAktar_Click(object sender, EventArgs e)
        {
            if (IsClosedOrClosing) return;
            Sporcu sporcu = SeciliSporcu();
            if (sporcu == null)
            {
                MessageBox.Show(this, "CSV raporu için bir sporcu seçin.", "Seçim gerekli",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string yol;
            using (var dialog = new SaveFileDialog
            {
                Title = "CSV raporunu kaydet",
                Filter = "CSV dosyası (*.csv)|*.csv|Tüm dosyalar (*.*)|*.*",
                DefaultExt = "csv",
                AddExtension = true,
                OverwritePrompt = true,
                FileName = GuvenliDosyaAdi(sporcu.Ad) + "_rapor.csv",
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK || IsClosedOrClosing) return;
                yol = dialog.FileName;
            }
            using (BeginBusy())
            {
                try
                {
                    string csv = await _raporService.CsvOlusturAsync(sporcu.Id, LifetimeToken);
                    if (IsClosedOrClosing) return;
                    if (csv == null)
                    {
                        MessageBox.Show(this, "Sporcu bulunamadı (başka bir yerden silinmiş olabilir).", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    await CsvYazAsync(yol, csv);
                    if (IsClosedOrClosing) return;
                    MessageBox.Show(this, "CSV raporu kaydedildi:" + Environment.NewLine + yol, "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
                {
                    ErrorHandler.Log(ex);
                    if (IsClosedOrClosing) return;
                    MessageBox.Show(this, "Dosya yazılamadı. Dosya başka bir programda (örn. Excel) açık olabilir ya da "
                        + "klasöre yazma izniniz olmayabilir." + Environment.NewLine + yol, "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
            }
        }

        /// <summary>
        /// Önce aynı klasörde geçici dosyaya yazar, başarılıysa hedefin üzerine taşır; böylece yarım kalan
        /// yazma var olan raporu bozmaz. İçerik bellekte hazır olduğundan yazma kapanış token'ıyla kesilmez.
        /// </summary>
        private static async Task CsvYazAsync(string yol, string csv)
        {
            string klasor = Path.GetDirectoryName(Path.GetFullPath(yol));
            string gecici = Path.Combine(klasor, "." + Path.GetFileName(yol) + "." + Guid.NewGuid().ToString("N") + ".tmp");
            try
            {
                // Excel'in Türkçe karakterleri doğru açması için UTF-8 BOM ile yazılır.
                await File.WriteAllTextAsync(gecici, csv, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true), CancellationToken.None);
                File.Move(gecici, yol, overwrite: true);
            }
            finally
            {
                try { if (File.Exists(gecici)) File.Delete(gecici); }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException) { ErrorHandler.Log(ex); }
            }
        }

        private static string GuvenliDosyaAdi(string ad)
        {
            string temiz = string.IsNullOrWhiteSpace(ad) ? "Sporcu" : ad.Trim();
            foreach (char c in Path.GetInvalidFileNameChars()) temiz = temiz.Replace(c, '_');
            return temiz;
        }

        // ---- Ortak ----

        /// <returns>Kayıt düzenlenip kaydedildiyse true.</returns>
        private bool DuzenlemePenceresiAc<TForm>(Action<TForm> hazirla) where TForm : Form
        {
            // Form1.AltFormuAc ile aynı: pencere kendi kapsamında oluşur ve kapanınca dispose edilir.
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                var frm = scope.ServiceProvider.GetRequiredService<TForm>();
                hazirla(frm);
                return frm.ShowDialog(this) == DialogResult.OK && !IsClosedOrClosing;
            }
        }

        private bool SilmeOnayla(string mesaj) =>
            MessageBox.Show(this, mesaj, "Silme onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes;

        private async Task SilVeYenileAsync(Func<CancellationToken, Task<bool>> sil)
        {
            if (IsClosedOrClosing) return;
            using (BeginBusy())
            {
                try
                {
                    bool silindi = await sil(LifetimeToken);
                    if (IsClosedOrClosing) return;
                    if (!silindi)
                        MessageBox.Show(this, "Kayıt bulunamadı (başka bir yerden silinmiş olabilir). Liste yenileniyor.",
                            "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); return; }
                catch (OperationCanceledException) { return; }
            }
            await ListeleDetaylarAsync();
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
