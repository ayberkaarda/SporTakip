using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SporTakip.Application.Validation;

namespace SporTakip
{
    /// <summary>Servis doğrulama hatalarını (ValidationError.PropertyName) formdaki ErrorProvider kontrollerine eşler.</summary>
    internal static class ValidationDisplay
    {
        /// <returns>Sonuç geçerliyse true.</returns>
        internal static bool Goster(ValidationResult result, ErrorProvider errors,
            IReadOnlyDictionary<string, Control> alanlar, IWin32Window owner)
        {
            if (result.IsValid) return true;

            var eslesmeyen = new List<string>();
            foreach (var group in result.Errors.GroupBy(e => e.PropertyName ?? string.Empty))
            {
                string message = string.Join(Environment.NewLine, group.Select(e => e.Message));
                if (alanlar.TryGetValue(group.Key, out Control control))
                    errors.SetError(control, message);
                else
                    eslesmeyen.Add(message);
            }

            if (eslesmeyen.Count > 0)
                MessageBox.Show(owner, string.Join(Environment.NewLine, eslesmeyen), "Eksik bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }
}
