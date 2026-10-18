using System;
using System.Data.Common;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace SporTakip
{
    internal static class ErrorHandler
    {
        private static ILogger _logger;

        /// <summary>DI kurulduktan sonra çağrılır; öncesinde hatalar doğrudan günlük dosyasına yazılır.</summary>
        internal static void Initialize(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger("SporTakip");
        }

        internal static void Log(Exception exception)
        {
            try
            {
                if (_logger != null) _logger.LogError(exception, "İşlenmeyen hata");
                else FileLoggerProvider.Write("[Error] SporTakip: İşlenmeyen hata", exception);
            }
            catch (Exception)
            {
                // Günlük yazılamaması hata bildirimini engellememeli.
            }
        }

        internal static void Show(Exception exception)
        {
            Log(exception);
            string message = exception is DbException
                ? "Veritabanı işlemi tamamlanamadı. Bağlantı ayarlarını ve veritabanının kullanılabilir olduğunu kontrol edin."
                : "Beklenmeyen bir hata oluştu. Lütfen işlemi yeniden deneyin.";
            MessageBox.Show(message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
