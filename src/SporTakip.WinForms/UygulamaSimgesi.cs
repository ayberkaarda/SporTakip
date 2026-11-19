using System;
using System.Drawing;
using System.IO;

namespace SporTakip
{
    /// <summary>
    /// The application icon (app.ico, drawn from assets/logo.svg). Embedded in the assembly so every
    /// window can use it without per-form .resx copies.
    /// </summary>
    internal static class UygulamaSimgesi
    {
        private const string KaynakAdi = "SporTakip.app.ico";

        private static readonly Lazy<Icon> _simge = new Lazy<Icon>(() =>
        {
            using Stream stream = typeof(UygulamaSimgesi).Assembly.GetManifestResourceStream(KaynakAdi);
            return stream == null ? null : new Icon(stream);
        });

        /// <summary>Shared instance for Form.Icon; forms do not dispose it.</summary>
        internal static Icon Simge => _simge.Value;

        /// <summary>The icon image closest to <paramref name="boyut"/> pixels, as a new bitmap owned by the caller.</summary>
        internal static Bitmap Gorsel(int boyut)
        {
            Icon simge = Simge;
            if (simge == null) return null;
            using var secilen = new Icon(simge, boyut, boyut);
            return secilen.ToBitmap();
        }
    }
}
