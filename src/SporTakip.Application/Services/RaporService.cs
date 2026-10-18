using System.Globalization;
using System.Text;
using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Application.Abstractions.Services;
using SporTakip.Application.Reporting;

namespace SporTakip.Application.Services;

public sealed class RaporService : IRaporService
{
    private readonly ISporcuRepository _sporcuRepository;
    private readonly IAntrenmanRepository _antrenmanRepository;
    private readonly ITakipRepository _takipRepository;

    public RaporService(
        ISporcuRepository sporcuRepository,
        IAntrenmanRepository antrenmanRepository,
        ITakipRepository takipRepository)
    {
        _sporcuRepository = sporcuRepository;
        _antrenmanRepository = antrenmanRepository;
        _takipRepository = takipRepository;
    }

    public async Task<SporcuRaporu?> SporcuRaporuOlusturAsync(int sporcuId, CancellationToken cancellationToken = default)
    {
        var sporcu = await _sporcuRepository.GetByIdAsync(sporcuId, cancellationToken);
        if (sporcu is null) return null;

        var antrenmanlar = await _antrenmanRepository.ListBySporcuIdAsync(sporcuId, cancellationToken);
        var takipler = await _takipRepository.ListBySporcuIdAsync(sporcuId, cancellationToken);
        return new SporcuRaporu(sporcu, antrenmanlar, takipler);
    }

    public string MetneDonustur(SporcuRaporu rapor)
    {
        ArgumentNullException.ThrowIfNull(rapor);
        var sb = new StringBuilder();
        sb.Append($"=== {rapor.Sporcu.Ad} - {rapor.Sporcu.SporDali} Raporu ===\n");
        foreach (var takip in rapor.Takipler)
            sb.Append($"{takip.Tarih.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)} - {takip.Aciklama}\n");
        return sb.ToString();
    }
}
