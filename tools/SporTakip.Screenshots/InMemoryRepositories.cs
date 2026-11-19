using System.Globalization;
using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Domain;

namespace SporTakip.Screenshots;

/// <summary>Read-mostly in-memory store; the screenshots never save anything.</summary>
internal sealed class SampleStore
{
    public List<Sporcu> Sporcular { get; } = [];
    public List<Antrenman> Antrenmanlar { get; } = [];
    public List<Takip> Takipler { get; } = [];
    public List<Olcum> Olcumler { get; } = [];
}

internal sealed class InMemorySporcuRepository(SampleStore store) : ISporcuRepository
{
    private static readonly StringComparer Tr = StringComparer.Create(CultureInfo.GetCultureInfo("tr-TR"), ignoreCase: true);

    // Same order as the SQL repository (ORDER BY Ad).
    public Task<IReadOnlyList<Sporcu>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Sporcu>>(store.Sporcular.OrderBy(s => s.Ad, Tr).ToList());

    public Task<Sporcu?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => Task.FromResult(store.Sporcular.FirstOrDefault(s => s.Id == id));

    public Task<int> AddAsync(Sporcu sporcu, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();

    public Task<bool> UpdateAsync(Sporcu sporcu, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();

    public Task<bool> SporcuyuTumuyleSilAsync(int id, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();
}

internal sealed class InMemoryAntrenmanRepository(SampleStore store) : IAntrenmanRepository
{
    public Task<Antrenman?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => Task.FromResult(store.Antrenmanlar.FirstOrDefault(a => a.Id == id));

    public Task<IReadOnlyList<Antrenman>> ListBySporcuIdAsync(int sporcuId, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Antrenman>>(store.Antrenmanlar.Where(a => a.SporcuId == sporcuId).OrderBy(a => a.Id).ToList());

    public Task<int> AddAsync(Antrenman antrenman, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();

    public Task<bool> UpdateAsync(Antrenman antrenman, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();
}

internal sealed class InMemoryTakipRepository(SampleStore store) : ITakipRepository
{
    public Task<Takip?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => Task.FromResult(store.Takipler.FirstOrDefault(t => t.Id == id));

    // Same order as the SQL repository (ORDER BY Tarih DESC).
    public Task<IReadOnlyList<Takip>> ListBySporcuIdAsync(int sporcuId, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Takip>>(
            store.Takipler.Where(t => t.SporcuId == sporcuId).OrderByDescending(t => t.Tarih).ThenByDescending(t => t.Id).ToList());

    public Task<int> AddAsync(Takip takip, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();

    public Task<bool> UpdateAsync(Takip takip, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        => throw new NotSupportedException();
}

internal sealed class InMemoryOlcumRepository(SampleStore store) : IOlcumRepository
{
    public Task<Olcum?> GetByIdAsync(int id, CancellationToken ct = default)
        => Task.FromResult(store.Olcumler.FirstOrDefault(o => o.Id == id));

    public Task<IReadOnlyList<Olcum>> ListByTakipIdAsync(int takipId, CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<Olcum>>(store.Olcumler.Where(o => o.TakipId == takipId).OrderBy(o => o.Id).ToList());

    public Task<IReadOnlyList<OlcumKaydi>> ListBySporcuIdAsync(int sporcuId, CancellationToken ct = default)
    {
        var liste = store.Olcumler
            .Join(store.Takipler.Where(t => t.SporcuId == sporcuId), o => o.TakipId, t => t.Id,
                (o, t) => new OlcumKaydi(o.Id, o.TakipId, t.Tarih, o.Tur, o.Deger, o.Birim))
            .OrderBy(k => k.Tarih).ThenBy(k => k.Id)
            .ToList();
        return Task.FromResult<IReadOnlyList<OlcumKaydi>>(liste);
    }

    public Task<int> AddAsync(Olcum olcum, CancellationToken ct = default)
        => throw new NotSupportedException();

    public Task<bool> UpdateAsync(Olcum olcum, CancellationToken ct = default)
        => throw new NotSupportedException();

    public Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        => throw new NotSupportedException();
}
