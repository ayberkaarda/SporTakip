using SporTakip.Application.Reporting;

namespace SporTakip.Application.Abstractions.Services;

public interface IRaporService
{
    /// <returns>Sporcu bulunamazsa null.</returns>
    Task<SporcuRaporu?> SporcuRaporuOlusturAsync(int sporcuId, CancellationToken cancellationToken = default);

    /// <summary>Raporu "=== Ad - SporDali Raporu ===" başlıklı, her takip için "tarih - açıklama" satırlı metne çevirir.</summary>
    string MetneDonustur(SporcuRaporu rapor);
}
