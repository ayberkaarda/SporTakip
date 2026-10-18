using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Domain;

namespace SporTakip
{
    public partial class Form1 : BorderlessForm
    {
        private readonly ISporcuService _sporcuService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ErrorProvider _errors;

        public Form1(ISporcuService sporcuService, IServiceScopeFactory scopeFactory)
        {
            _sporcuService = sporcuService;
            _scopeFactory = scopeFactory;
            InitializeComponent();
            if (components == null) components = new System.ComponentModel.Container();
            _errors = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
        }

        /// <summary>Listede "Id - Ad - SporDali" biçiminde gösterilen satır.</summary>
        private sealed class SporcuSatiri
        {
            public SporcuSatiri(Sporcu sporcu) => Sporcu = sporcu;

            public Sporcu Sporcu { get; }

            public int Id => Sporcu.Id;

            public string Gorunum => string.Format("{0} - {1} - {2}", Sporcu.Id, Sporcu.Ad, Sporcu.SporDali);
        }

        private async Task ListeleSporcularAsync()
        {
            IReadOnlyList<Sporcu> sporcular = await _sporcuService.ListeleAsync(LifetimeToken);
            if (IsClosedOrClosing) return;
            lstSporcular.DisplayMember = nameof(SporcuSatiri.Gorunum);
            lstSporcular.ValueMember = nameof(SporcuSatiri.Id);
            lstSporcular.DataSource = sporcular.Select(s => new SporcuSatiri(s)).ToList();
        }

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            _errors.Clear();
            string ad = txtAd.Text.Trim();
            string sporDali = txtSporDali.Text.Trim();
            if (string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(sporDali))
            {
                MessageBox.Show("Lütfen sporcu adını ve spor dalını girin.", "Eksik bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (BeginBusy())
            {
                try
                {
                    var result = await _sporcuService.EkleAsync(new Sporcu { Ad = ad, SporDali = sporDali }, LifetimeToken);
                    // Form bu arada kapatıldıysa kontrollere dokunma, kapanmış form için mesaj gösterme.
                    if (IsClosedOrClosing) return;
                    if (!ValidationDisplay.Goster(result, _errors, new Dictionary<string, Control>
                        {
                            [nameof(Sporcu.Ad)] = txtAd,
                            [nameof(Sporcu.SporDali)] = txtSporDali,
                        }, this)) return;
                    MessageBox.Show("Sporcu kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAd.Clear();
                    txtSporDali.Clear();
                    txtAd.Focus();
                    await ListeleSporcularAsync();
                }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
            }
        }

        private void AltFormuAc<TForm>() where TForm : Form
        {
            // Her pencere kendi kapsamında oluşur; kapsam kapanınca form ve scoped servisler dispose edilir.
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                var frm = scope.ServiceProvider.GetRequiredService<TForm>();
                frm.ShowDialog(this);
            }
        }

        private void btnAntrenmanEkle_Click(object sender, EventArgs e)
        {
            AltFormuAc<AntrenmanEkleForm>();
        }

        private void btnTakipEkle_Click(object sender, EventArgs e)
        {
            AltFormuAc<TakipEkleForm>();
        }

        private void btnSporcuDetay_Click(object sender, EventArgs e)
        {
            AltFormuAc<SporcuDetayForm>();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            using (BeginBusy())
            {
                try { await ListeleSporcularAsync(); }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); }
                catch (OperationCanceledException) { }
            }
        }

        private void txtAd_Enter(object sender, EventArgs e)
        {

        }

        private void txtAd_Leave(object sender, EventArgs e)
        {

        }

        private async void btnSil_Click(object sender, EventArgs e)
        {
            var selected = lstSporcular.SelectedItem as SporcuSatiri;
            if (selected == null)
            {
                MessageBox.Show("Silmek için bir sporcu seçin.", "Seçim gerekli",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = selected.Id;
            string ad = selected.Sporcu.Ad;
            if (MessageBox.Show(string.Format("\"{0}\" ve ona ait tüm antrenman ve takip kayıtları silinecek. Devam edilsin mi?", ad),
                "Silme onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            using (BeginBusy())
            {
                try
                {
                    bool silindi = await _sporcuService.SilAsync(id, LifetimeToken);
                    if (IsClosedOrClosing) return;
                    if (silindi)
                        MessageBox.Show(this, "Sporcu ve tüm ilişkili kayıtlar silindi.", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(this, "Kayıt bulunamadı (başka bir yerden silinmiş olabilir). Liste yenileniyor.",
                            "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await ListeleSporcularAsync();
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
