using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Domain;
using PlotColor = ScottPlot.Color;

namespace SporTakip
{
    /// <summary>Bir sporcunun seçilen ölçüm türündeki değerlerini tarihe göre çizgi grafikte gösterir.</summary>
    public partial class OlcumGrafikForm : BorderlessForm
    {
        // Koyu lacivert tema; form arka planı ve boş durum etiketiyle aynı tonlar.
        private static readonly PlotColor FigurArkaPlan = PlotColor.FromHex("#081640");
        private static readonly PlotColor VeriArkaPlan = PlotColor.FromHex("#0E2258");
        private static readonly PlotColor EksenRengi = PlotColor.FromHex("#AABCE4");
        private static readonly PlotColor CizgiRengi = PlotColor.FromHex("#F07A28");

        private readonly IOlcumService _olcumService;
        private int _sporcuId;
        private int _grafikIstegi;
        private AxisLimits? _cizimSinirlari;

        public OlcumGrafikForm(IOlcumService olcumService)
        {
            _olcumService = olcumService;
            InitializeComponent();
            GrafikTemasiniUygula();
            // ScottPlot'un İngilizce sağ tık menüsü yerine tek Türkçe komut: yakınlaştırma/kaydırmayı geri al.
            formsPlot.Menu?.Clear();
            formsPlot.Menu?.Add("Görünümü sıfırla", plot =>
            {
                if (_cizimSinirlari.HasValue) plot.Axes.SetLimits(_cizimSinirlari.Value);
                formsPlot.Refresh();
            });
        }

        /// <summary>Pencere gösterilmeden önce hangi sporcunun ölçümlerinin çizileceğini verir.</summary>
        public void Yukle(int sporcuId, string sporcuAdi)
        {
            _sporcuId = sporcuId;
            lblSporcu.Text = string.IsNullOrWhiteSpace(sporcuAdi) ? string.Empty : "— " + sporcuAdi;
        }

        private void GrafikTemasiniUygula()
        {
            Plot plot = formsPlot.Plot;
            plot.FigureBackground.Color = FigurArkaPlan;
            plot.DataBackground.Color = VeriArkaPlan;
            plot.Grid.MajorLineColor = PlotColor.FromHex("#FFFFFF").WithOpacity(0.08);
            plot.Font.Set("Segoe UI");
            EksenleriBoya(plot);
        }

        /// <summary>Eksen yazı/çizgi renkleri; alt eksen tarih eksenine çevrilince yeniden uygulanmalı.</summary>
        private static void EksenleriBoya(Plot plot)
        {
            plot.Axes.Color(EksenRengi);
            foreach (var eksen in plot.Axes.GetAxes())
                eksen.FrameLineStyle.Color = PlotColor.FromHex("#FFFFFF").WithOpacity(0.15);
        }

        private async void OlcumGrafikForm_Load(object sender, EventArgs e)
        {
            BosGoster("Yükleniyor…");
            IReadOnlyList<string> turler;
            using (BeginBusy())
            {
                try { turler = await _olcumService.TurleriAsync(_sporcuId, LifetimeToken); }
                catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); return; }
                catch (OperationCanceledException) { return; }
            }
            if (IsClosedOrClosing) return;
            if (turler.Count == 0)
            {
                cmbTur.Enabled = false;
                BosGoster("Bu sporcu için henüz ölçüm yok." + Environment.NewLine
                    + "Takip eklerken veya düzenlerken ölçüm girebilirsiniz.");
                return;
            }
            cmbTur.Items.AddRange(turler.Cast<object>().ToArray());
            // SelectedIndexChanged grafiği çizer.
            cmbTur.SelectedIndex = 0;
        }

        private async void cmbTur_SelectedIndexChanged(object sender, EventArgs e)
        {
            await CizAsync(cmbTur.SelectedItem as string);
        }

        private async Task CizAsync(string tur)
        {
            if (IsClosedOrClosing || string.IsNullOrEmpty(tur)) return;
            // Tür hızlı değiştirilirse yalnızca son isteğin sonucu çizilir.
            int istek = ++_grafikIstegi;
            IReadOnlyList<OlcumKaydi> kayitlar;
            try { kayitlar = await _olcumService.SporcuOlcumleriAsync(_sporcuId, tur, LifetimeToken); }
            catch (Exception ex) when (!IsClosingCancellation(ex)) { ErrorHandler.Show(ex); return; }
            catch (OperationCanceledException) { return; }
            if (istek != _grafikIstegi || IsClosedOrClosing) return;

            var sirali = kayitlar.OrderBy(k => k.Tarih).ThenBy(k => k.Id).ToList();
            if (sirali.Count == 0)
            {
                lblOzet.Text = string.Empty;
                BosGoster("Bu tür için ölçüm yok");
                return;
            }

            // Birim tüm kayıtlarda aynıysa eksende gösterilir.
            var birimler = sirali.Select(k => k.Birim?.Trim() ?? string.Empty).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            string birim = birimler.Count == 1 && birimler[0].Length > 0 ? birimler[0] : null;

            Plot plot = formsPlot.Plot;
            plot.Clear();
            double[] xs = sirali.Select(k => k.Tarih.ToOADate()).ToArray();
            double[] ys = sirali.Select(k => (double)k.Deger).ToArray();
            var cizgi = plot.Add.Scatter(xs, ys);
            cizgi.Color = CizgiRengi;
            cizgi.LineWidth = 2.5f;
            cizgi.MarkerSize = 8;
            cizgi.MarkerShape = MarkerShape.FilledCircle;
            plot.Axes.DateTimeTicksBottom();
            EksenleriBoya(plot);
            plot.YLabel(birim == null ? tur : string.Format("{0} ({1})", tur, birim));
            plot.Axes.AutoScale();
            // Tek nokta veya düz çizgide eksen sıfır genişlikte kalmasın.
            if (xs.Min() == xs.Max()) plot.Axes.SetLimitsX(xs[0] - 3, xs[0] + 3);
            if (ys.Min() == ys.Max()) plot.Axes.SetLimitsY(ys[0] - 1, ys[0] + 1);
            else plot.Axes.Margins(0.08, 0.15);

            _cizimSinirlari = plot.Axes.GetLimits();

            OlcumKaydi son = sirali[sirali.Count - 1];
            lblOzet.Text = string.Format("{0} ölçüm · son: {1} ({2:d})", sirali.Count,
                OlcumUi.DegerBirimMetni(son.Deger, son.Birim), son.Tarih);
            lblBos.Visible = false;
            formsPlot.Visible = true;
            formsPlot.Refresh();
        }

        private void BosGoster(string mesaj)
        {
            lblBos.Text = mesaj;
            lblBos.Visible = true;
            lblBos.BringToFront();
            formsPlot.Visible = false;
        }
    }
}
