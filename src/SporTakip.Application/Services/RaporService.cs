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
    private readonly IOlcumRepository _olcumRepository;

    public RaporService(
        ISporcuRepository sporcuRepository,
        IAntrenmanRepository antrenmanRepository,
        ITakipRepository takipRepository,
        IOlcumRepository olcumRepository)
    {
        _olcumRepository = olcumRepository;
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

    private static readonly CultureInfo TrKultur = CultureInfo.GetCultureInfo("tr-TR");

    public async Task<string?> CsvOlusturAsync(int sporcuId, CancellationToken ct = default)
    {
        var sporcu = await _sporcuRepository.GetByIdAsync(sporcuId, ct);
        if (sporcu is null) return null;

        var takipler = await _takipRepository.ListBySporcuIdAsync(sporcuId, ct);
        var olcumler = await _olcumRepository.ListBySporcuIdAsync(sporcuId, ct);
        var olcumlerByTakip = olcumler.ToLookup(o => o.TakipId);

        var sb = new StringBuilder();
        sb.Append("Tarih;Takip Açıklaması;Ölçüm Türü;Değer;Birim\r\n");
        foreach (var takip in takipler.OrderBy(t => t.Tarih))
        {
            var tarih = takip.Tarih.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            var aciklama = CsvMetin(takip.Aciklama);
            var liste = olcumlerByTakip[takip.Id].ToList();
            if (liste.Count == 0)
            {
                sb.Append($"{tarih};{aciklama};;;\r\n");
                continue;
            }
            foreach (var o in liste)
            {
                var deger = CsvAlan(o.Deger.ToString("0.##", TrKultur));
                sb.Append($"{tarih};{aciklama};{CsvMetin(o.Tur)};{deger};{CsvMetin(o.Birim)}\r\n");
            }
        }
        return sb.ToString();
    }

    // CSV enjeksiyonuna karşı =, +, -, @ ile başlayan metnin başına ' ekler, sonra RFC4180 kaçışı uygular.
    private static string CsvMetin(string? deger)
    {
        if (string.IsNullOrEmpty(deger)) return string.Empty;
        if (deger[0] is '=' or '+' or '-' or '@') deger = "'" + deger;
        return CsvAlan(deger);
    }

    private static string CsvAlan(string deger)
        => deger.IndexOfAny([';', '"', '\r', '\n']) >= 0
            ? "\"" + deger.Replace("\"", "\"\"") + "\""
            : deger;
}
