using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SporTakip.Application;
using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Domain;
using SporTakip.Theme;

namespace SporTakip.Screenshots;

/// <summary>
/// Writes the README screenshots (light theme, 96 DPI) without showing any window.
/// Usage: SporTakip.Screenshots [output folder]; the default is the repository's screenshots folder.
/// </summary>
internal static class Program
{
    [STAThread]
    private static int Main(string[] args)
    {
        string cikti = args.Length > 0 ? Path.GetFullPath(args[0]) : Path.Combine(DepoKoku(), "screenshots");
        Directory.CreateDirectory(cikti);

        // The UI formats dates and numbers with the current culture; the screenshots are Turkish.
        CultureInfo tr = CultureInfo.GetCultureInfo("tr-TR");
        CultureInfo.DefaultThreadCurrentCulture = tr;
        CultureInfo.DefaultThreadCurrentUICulture = tr;
        Thread.CurrentThread.CurrentCulture = tr;
        Thread.CurrentThread.CurrentUICulture = tr;

        // DPI-unaware: the process sees 96 DPI on any monitor scale, so the images are 1x.
        System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
        System.Windows.Forms.Application.EnableVisualStyles();
        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        // A handler keeps WinForms from opening its exception dialog; the run fails instead.
        Exception? hata = null;
        System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        System.Windows.Forms.Application.ThreadException += (_, e) => hata ??= e.Exception;

        AcikTemayiSec();

        SampleStore store = SampleData.Olustur();
        var services = new ServiceCollection();
        services.AddSingleton(store);
        services.AddSingleton<ISporcuRepository, InMemorySporcuRepository>();
        services.AddSingleton<IAntrenmanRepository, InMemoryAntrenmanRepository>();
        services.AddSingleton<ITakipRepository, InMemoryTakipRepository>();
        services.AddSingleton<IOlcumRepository, InMemoryOlcumRepository>();
        services.AddSporTakipApplication();
        services.AddTransient<Form1>();
        services.AddTransient<AntrenmanEkleForm>();
        services.AddTransient<TakipEkleForm>();
        services.AddTransient<SporcuDetayForm>();
        services.AddTransient<HaftalikProgramForm>();
        services.AddTransient<OlcumGrafikForm>();
        using ServiceProvider provider = services.BuildServiceProvider();

        int vitrin = SampleData.VitrinSporcuId;
        string vitrinAdi = store.Sporcular.Single(s => s.Id == vitrin).Ad;

        var cekimler = new (string Dosya, Func<IServiceProvider, Form> Olustur, Action<Form>? Sonra)[]
        {
            ("anasayfa.png", sp => sp.GetRequiredService<Form1>(), null),
            ("antreman_ekle.png", sp => sp.GetRequiredService<AntrenmanEkleForm>(), f =>
            {
                Kontrol<ComboBox>(f, "comboSporcular").SelectedValue = vitrin;
                Kontrol<TextBox>(f, "txtAntrenmanName").Text = "Bacak ve kalça";
                Kontrol<TextBox>(f, "txtDetaylar").Text = "Romanian deadlift 4x8, hip thrust 3x10";
                Kontrol<ComboBox>(f, "cmbGun").SelectedIndex = 4; // Perşembe
            }),
            ("takip_ekle.png", sp => sp.GetRequiredService<TakipEkleForm>(), f =>
            {
                Kontrol<ComboBox>(f, "cmbSporcular").SelectedValue = vitrin;
                Kontrol<DateTimePicker>(f, "dtTarih").Value = new DateTime(2026, 11, 20);
                Kontrol<TextBox>(f, "txtAciklama").Text = "Setler tamamlandı";
                // Measurements waiting to be saved with the follow-up (what "Ölçüm Ekle" fills).
                var bekleyen = Alan<System.ComponentModel.BindingList<Olcum>>(f, "_bekleyenOlcumler");
                bekleyen.Add(new Olcum { Tur = "Kilo", Deger = 82.1m, Birim = "kg" });
                bekleyen.Add(new Olcum { Tur = "Bel", Deger = 88m, Birim = "cm" });
                bekleyen.Add(new Olcum { Tur = "Nabız", Deger = 64m, Birim = "bpm" });
            }),
            ("sporcu_detayi.png", sp => sp.GetRequiredService<SporcuDetayForm>(), f =>
                Kontrol<ComboBox>(f, "cmbSporcular").SelectedValue = vitrin),
            ("haftalik_program.png", sp =>
            {
                var f = sp.GetRequiredService<HaftalikProgramForm>();
                f.Yukle(vitrin, vitrinAdi);
                return f;
            }, null),
            ("olcum_grafik.png", sp =>
            {
                var f = sp.GetRequiredService<OlcumGrafikForm>();
                f.Yukle(vitrin, vitrinAdi);
                return f;
            }, f => Kontrol<ComboBox>(f, "cmbTur").SelectedItem = "Kilo"),
        };

        foreach (var (dosya, olustur, sonra) in cekimler)
        {
            using IServiceScope scope = provider.CreateScope();
            using Form form = olustur(scope.ServiceProvider);
            using Bitmap goruntu = OffscreenRenderer.Ciz(form, sonra);
            if (hata != null) break;
            string yol = Path.Combine(cikti, dosya);
            goruntu.Save(yol, ImageFormat.Png);
            Console.WriteLine($"{dosya} {goruntu.Width}x{goruntu.Height}");
        }

        if (hata != null)
        {
            Console.Error.WriteLine(hata);
            return 1;
        }
        return 0;
    }

    /// <summary>Light palette for this process only; the user's saved theme choice is not touched.</summary>
    private static void AcikTemayiSec()
    {
        MethodInfo ayarla = typeof(ThemeManager).GetMethod("Ayarla", BindingFlags.NonPublic | BindingFlags.Static)
            ?? throw new InvalidOperationException("ThemeManager.Ayarla not found.");
        ayarla.Invoke(null, [TemaModu.Acik, false]);
    }

    private static T Kontrol<T>(Form form, string ad) where T : Control =>
        form.Controls.Find(ad, searchAllChildren: true).OfType<T>().SingleOrDefault()
        ?? throw new InvalidOperationException($"{form.Name}: control '{ad}' not found.");

    private static T Alan<T>(Form form, string ad) where T : class =>
        form.GetType().GetField(ad, BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(form) as T
        ?? throw new InvalidOperationException($"{form.Name}: field '{ad}' not found.");

    private static string DepoKoku()
    {
        for (var dizin = new DirectoryInfo(Environment.CurrentDirectory); dizin != null; dizin = dizin.Parent)
            if (File.Exists(Path.Combine(dizin.FullName, "SporTakip.sln"))) return dizin.FullName;
        throw new InvalidOperationException("Run from inside the repository or pass an output folder.");
    }
}
