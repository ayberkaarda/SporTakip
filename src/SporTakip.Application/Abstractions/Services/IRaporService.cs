using SporTakip.Application.Reporting;

namespace SporTakip.Application.Abstractions.Services;

public interface IRaporService
{
    /// <returns>Sporcu bulunamazsa null.</returns>
    Task<SporcuRaporu?> SporcuRaporuOlusturAsync(int sporcuId, CancellationToken cancellationToken = default);

    /// <summary>Raporu "=== Ad - SporDali Raporu ===" başlıklı, her takip için "tarih - açıklama" satırlı metne çevirir.</summary>
    string MetneDonustur(SporcuRaporu rapor);

    /// <summary>Excel-TR uyumlu (';' ayırıcılı, CRLF) CSV üretir. Takipler eskiden yeniye; ölçümlü takip ölçüm başına bir satır.</summary>
    /// <returns>Sporcu bulunamazsa null.</returns>
    Task<string?> CsvOlusturAsync(int sporcuId, CancellationToken ct = default);
}
