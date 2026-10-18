using System;
using System.Linq;
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
        private readonly IServiceScopeFactory _scopeFactory;
        private bool _isLoading;
        private int _detayIstegi;

        public SporcuDetayForm(ISporcuService sporcuService, IAntrenmanService antrenmanService, ITakipService takipService,
            IServiceScopeFactory scopeFactory)
        {
            _sporcuService = sporcuService;
            _antrenmanService = antrenmanService;
            _takipService = takipService;
            _scopeFactory = scopeFactory;
            InitializeComponent();
            // Sütunlar Designer'da tanımlı; DataSource'un özelliklerinden yeni sütun üretilmesin.
            dgvAntrenmanlar.AutoGenerateColumns = false;
            dgvTakipler.AutoGenerateColumns = false;
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
                    if (istek != _detayIstegi || IsClosedOrClosing) return;
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
            if (DuzenlemePenceresiAc<TakipDuzenleForm>(frm => frm.Yukle(secili)))
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
