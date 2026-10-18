#nullable enable
using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace SporTakip
{
    /// <summary>
    /// Günlükleri %LOCALAPPDATA%\SporTakip\logs\yyyy-MM-dd.log dosyasına yazan, ek paket gerektirmeyen sağlayıcı.
    /// Seviye filtresi "Logging" yapılandırmasından gelir.
    /// </summary>
    [ProviderAlias("File")]
    internal sealed class FileLoggerProvider : ILoggerProvider
    {
        private static readonly object WriteLock = new object();

        internal static string LogDirectory { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SporTakip", "logs");

        public ILogger CreateLogger(string categoryName) => new FileLogger(categoryName);

        public void Dispose()
        {
        }

        /// <summary>Tek bir kaydı dosyaya ekler; yazılamazsa sessizce geçer (hata bildirimi engellenmemeli).</summary>
        internal static void Write(string header, Exception? exception)
        {
            try
            {
                string text = DateTime.Now.ToString("O") + " " + header + Environment.NewLine
                    + (exception != null ? exception + Environment.NewLine : string.Empty);
                lock (WriteLock)
                {
                    Directory.CreateDirectory(LogDirectory);
                    File.AppendAllText(Path.Combine(LogDirectory, DateTime.Now.ToString("yyyy-MM-dd") + ".log"), text);
                }
            }
            catch (Exception)
            {
                // Günlük yazılamaması uygulamayı durdurmamalı.
            }
        }

        private sealed class FileLogger : ILogger
        {
            private readonly string _category;

            public FileLogger(string category) => _category = category;

            public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

            public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
                Func<TState, Exception?, string> formatter)
            {
                if (!IsEnabled(logLevel)) return;
                Write("[" + logLevel + "] " + _category + ": " + formatter(state, exception), exception);
            }
        }
    }
}
