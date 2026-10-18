using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SporTakip.Application.Abstractions.Services;

namespace SporTakip
{
    public partial class SporcuDetayForm : BorderlessForm
    {
        private readonly ISporcuService _sporcuService;
        private readonly IAntrenmanService _antrenmanService;
        private readonly ITakipService _takipService;
        private bool _isLoading;
        private int _detayIstegi;

        public SporcuDetayForm(ISporcuService sporcuService, IAntrenmanService antrenmanService, ITakipService takipService)
        {
            _sporcuService = sporcuService;
            _antrenmanService = antrenmanService;
            _takipService = takipService;
            InitializeComponent();
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
            lstAntrenmanlar.Items.Clear();
            lstTakipler.Items.Clear();
            int? sporcuId = SporcuComboHelper.SeciliSporcuId(cmbSporcular);
            if (sporcuId == null) return;
            using (BeginBusy())
            {
                try
                {
                    var antrenmanlar = await _antrenmanService.SporcuyaGoreListeleAsync(sporcuId.Value, token);
                    var takipler = await _takipService.SporcuyaGoreListeleAsync(sporcuId.Value, token);
                    if (istek != _detayIstegi || IsClosedOrClosing) return;
                    foreach (var antrenman in antrenmanlar)
                        lstAntrenmanlar.Items.Add($"{antrenman.AntrenmanAdi}: {antrenman.Detaylar}");
                    foreach (var takip in takipler)
                        lstTakipler.Items.Add($"{takip.Tarih}: {takip.Aciklama}");
                }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
            }
        }

        private async void cmbSporcular_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ListeleDetaylarAsync();
        }

        private async void btnGuncelle_Click(object sender, EventArgs e)
        {
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
