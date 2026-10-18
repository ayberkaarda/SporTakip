using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Domain;

namespace SporTakip
{
    /// <summary>Sporcu seçim kutularını dolduran ortak yardımcı (B5: önce Display/ValueMember, sonra DataSource).</summary>
    internal static class SporcuComboHelper
    {
        internal static async Task DoldurAsync(ComboBox combo, ISporcuService sporcuService,
            CancellationToken cancellationToken)
        {
            var sporcular = await sporcuService.ListeleAsync(cancellationToken);
            if (combo.IsDisposed || cancellationToken.IsCancellationRequested) return;
            combo.DisplayMember = nameof(Sporcu.Ad);
            combo.ValueMember = nameof(Sporcu.Id);
            combo.DataSource = sporcular;
        }

        /// <summary>Seçili sporcunun Id'si; seçim yoksa null.</summary>
        internal static int? SeciliSporcuId(ComboBox combo) =>
            combo.SelectedValue is int id ? id : (int?)null;
    }
}
