using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            // Composition root: ana pencere ve servisleri tek bir kapsamda; alt pencereler kendi kapsamlarını açar.
            using (IServiceScope scope = host.Services.CreateScope())
            {
                Application.Run(scope.ServiceProvider.GetRequiredService<Form1>());
            }
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

        builder.Services.AddTransient<Form1>();
        builder.Services.AddTransient<AntrenmanEkleForm>();
        builder.Services.AddTransient<TakipEkleForm>();
        builder.Services.AddTransient<SporcuDetayForm>();
        builder.Services.AddTransient<SporcuDuzenleForm>();
        builder.Services.AddTransient<AntrenmanDuzenleForm>();
        builder.Services.AddTransient<TakipDuzenleForm>();

        return builder.Build();
    }
}
