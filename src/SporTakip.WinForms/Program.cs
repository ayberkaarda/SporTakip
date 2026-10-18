using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SporTakip;
using SporTakip.Application;
using SporTakip.Data;

static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (sender, e) => ErrorHandler.Show(e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            ErrorHandler.Show(e.ExceptionObject as Exception ?? new Exception("Bilinmeyen uygulama hatası."));
        // csproj'taki ApplicationHighDpiMode ile aynı; ilk pencere oluşturulmadan önce çağrılmalı.
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        IHost host;
        try
        {
            host = BuildHost(args);
        }
        catch (Exception ex)
        {
            ErrorHandler.Show(ex);
            return;
        }

        using (host)
        {
            ErrorHandler.Initialize(host.Services.GetRequiredService<ILoggerFactory>());
            VeritabaniniHazirla(host.Services);
            // Composition root: ana pencere ve servisleri tek bir kapsamda; alt pencereler kendi kapsamlarını açar.
            using (IServiceScope scope = host.Services.CreateScope())
            {
                Application.Run(scope.ServiceProvider.GetRequiredService<Form1>());
            }
        }
    }

    // Kısa süren güncellemelerde bekleme penceresi hiç görünmesin (yanıp sönmesin).
    private static readonly TimeSpan BeklemePenceresiGecikmesi = TimeSpan.FromMilliseconds(300);
    private static readonly TimeSpan MigrationZamanAsimi = TimeSpan.FromMinutes(2);

    /// <summary>
    /// Ana pencereden önce veritabanı şemasını günceller. Hata olursa günlüğe yazar, kullanıcıyı bilgilendirir
    /// ve uygulama yine açılır; veritabanı yoksa ana pencere eskisi gibi kendi hata mesajını gösterir.
    /// </summary>
    private static void VeritabaniniHazirla(IServiceProvider services)
    {
        Task migration;
        using var zamanAsimi = new CancellationTokenSource(MigrationZamanAsimi);
        try
        {
            var migrator = services.GetRequiredService<SchemaMigrator>();
            // İş parçacığı havuzunda başlatılır; senkron bağlantı açılışı bile UI iş parçacığını tutmaz.
            migration = Task.Run(() => migrator.MigrateAsync(zamanAsimi.Token));
            // Task.Wait hata durumunda AggregateException fırlatır; burada yalnızca bitişi bekliyoruz.
            if (!((IAsyncResult)migration).AsyncWaitHandle.WaitOne(BeklemePenceresiGecikmesi))
            {
                // Ayrı bir mesaj döngüsü: pencere çizilmeye ve animasyon dönmeye devam eder.
                using var bekleme = new VeritabaniHazirlikForm(migration);
                Application.Run(bekleme);
            }
            migration.GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            ErrorHandler.Log(ex);
            string neden = ex is OperationCanceledException
                ? "Veritabanı belirlenen sürede yanıt vermedi."
                : "Veritabanına bağlanılamadı ya da şema güncellemesi tamamlanamadı.";
            MessageBox.Show(neden + Environment.NewLine + Environment.NewLine
                + "Bağlantı ayarlarını (appsettings.json > ConnectionStrings:SporTakip) ve SQL Server'ın çalıştığını kontrol edin. "
                + "Uygulama açılacak, ancak veritabanı işlemleri çalışmayabilir.",
                "Veritabanı hazırlanamadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private static IHost BuildHost(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
        {
            Args = args,
            // appsettings*.json dosyaları exe'nin yanında; uygulama başka bir dizinden başlatılsa da bulunur.
            ContentRootPath = AppContext.BaseDirectory,
        });
        // Makineye özel ayarlar (git'e girmez); varsa appsettings.json'ı ezer.
        builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: false);

        builder.Logging.ClearProviders();
        builder.Logging.AddDebug();
        builder.Logging.AddProvider(new FileLoggerProvider());

        string connectionString = builder.Configuration.GetConnectionString("SporTakip");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                "SporTakip bağlantı ayarı bulunamadı (appsettings.json > ConnectionStrings:SporTakip).");

        builder.Services.AddSporTakipApplication();
        builder.Services.AddSporTakipData(connectionString);
        // Açılıştaki şema güncellemesi; Data katmanı zaten kaydettiyse o kayıt kullanılır.
        builder.Services.TryAddSingleton(_ => new SchemaMigrator(connectionString));

        builder.Services.AddTransient<Form1>();
        builder.Services.AddTransient<AntrenmanEkleForm>();
        builder.Services.AddTransient<TakipEkleForm>();
        builder.Services.AddTransient<SporcuDetayForm>();
        builder.Services.AddTransient<SporcuDuzenleForm>();
        builder.Services.AddTransient<AntrenmanDuzenleForm>();
        builder.Services.AddTransient<TakipDuzenleForm>();
        builder.Services.AddTransient<OlcumGrafikForm>();
        builder.Services.AddTransient<HaftalikProgramForm>();

        return builder.Build();
    }
}
