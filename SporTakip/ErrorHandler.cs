using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace SporTakip
{
    internal static class ErrorHandler
    {
        private static readonly object LogLock = new object();

        internal static void Log(Exception exception)
        {
            try
            {
                string directory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "SporTakip", "logs");
                lock (LogLock)
                {
                    Directory.CreateDirectory(directory);
                    File.AppendAllText(Path.Combine(directory, DateTime.Now.ToString("yyyy-MM-dd") + ".log"),
                        DateTime.Now.ToString("O") + Environment.NewLine + exception + Environment.NewLine);
                }
            }
            catch (Exception)
            {
                // Günlük yazılamaması hata bildirimini engellememeli.
            }
        }

        internal static void Show(Exception exception)
        {
            Log(exception);
            string message = exception is SqlException
                ? "Veritabanı işlemi tamamlanamadı. Bağlantı ayarlarını ve veritabanının kullanılabilir olduğunu kontrol edin."
                : "Beklenmeyen bir hata oluştu. Lütfen işlemi yeniden deneyin.";
            MessageBox.Show(message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
