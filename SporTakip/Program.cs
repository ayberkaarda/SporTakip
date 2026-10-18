using System;
using System.Windows.Forms;
using SporTakip;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (sender, e) => ErrorHandler.Show(e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            ErrorHandler.Show(e.ExceptionObject as Exception ?? new Exception("Bilinmeyen uygulama hatası."));
        // csproj'taki ApplicationHighDpiMode ile aynı; ilk pencere oluşturulmadan önce çağrılmalı.
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Application.Run(new Form1());
    }
}

