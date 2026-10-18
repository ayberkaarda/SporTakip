using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Domain;

namespace SporTakip.Application.Tests;

internal sealed class FakeSporcuRepository : ISporcuRepository
{
    public List<Sporcu> Items { get; } = [];
    public List<int> TumuyleSilinenler { get; } = [];
    public List<int> DuzSilinenler { get; } = [];
    public Sporcu? Eklenen { get; private set; }
    public Sporcu? Guncellenen { get; private set; }
    public bool UpdateSonucu { get; set; } = true;
    public bool TumuyleSilSonucu { get; set; } = true;

    public Task<IReadOnlyList<Sporcu>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Sporcu>>(Items.ToList());

    public Task<Sporcu?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => Task.FromResult(Items.FirstOrDefault(s => s.Id == id));

    public Task<int> AddAsync(Sporcu sporcu, CancellationToken cancellationToken = default)
    {
        Eklenen = sporcu;
        return Task.FromResult(42);
    }

    public Task<bool> UpdateAsync(Sporcu sporcu, CancellationToken cancellationToken = default)
    {
        Guncellenen = sporcu;
        return Task.FromResult(UpdateSonucu);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        DuzSilinenler.Add(id);
        return Task.FromResult(true);
    }

    public Task<bool> SporcuyuTumuyleSilAsync(int id, CancellationToken cancellationToken = default)
    {
        TumuyleSilinenler.Add(id);
        return Task.FromResult(TumuyleSilSonucu);
    }
}

internal sealed class FakeAntrenmanRepository : IAntrenmanRepository
{
    public List<Antrenman> Items { get; } = [];
    public Antrenman? Eklenen { get; private set; }
    public Antrenman? Guncellenen { get; private set; }
    public bool UpdateSonucu { get; set; } = true;
    public List<int> Silinenler { get; } = [];

    public Task<Antrenman?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => Task.FromResult(Items.FirstOrDefault(a => a.Id == id));

    public Task<IReadOnlyList<Antrenman>> ListBySporcuIdAsync(int sporcuId, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Antrenman>>(Items.Where(a => a.SporcuId == sporcuId).ToList());

    public Task<int> AddAsync(Antrenman antrenman, CancellationToken cancellationToken = default)
    {
        Eklenen = antrenman;
        return Task.FromResult(7);
    }

    public Task<bool> UpdateAsync(Antrenman antrenman, CancellationToken cancellationToken = default)
    {
        Guncellenen = antrenman;
        return Task.FromResult(UpdateSonucu);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        Silinenler.Add(id);
        return Task.FromResult(true);
    }
}

internal sealed class FakeTakipRepository : ITakipRepository
{
    public List<Takip> Items { get; } = [];
    public Takip? Eklenen { get; private set; }
    public Takip? Guncellenen { get; private set; }
    public bool UpdateSonucu { get; set; } = true;
    public List<int> Silinenler { get; } = [];

    public Task<Takip?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => Task.FromResult(Items.FirstOrDefault(t => t.Id == id));

    public Task<IReadOnlyList<Takip>> ListBySporcuIdAsync(int sporcuId, CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Takip>>(
            Items.Where(t => t.SporcuId == sporcuId).OrderByDescending(t => t.Tarih).ToList());

    public Task<int> AddAsync(Takip takip, CancellationToken cancellationToken = default)
    {
        Eklenen = takip;
        return Task.FromResult(9);
    }

    public Task<bool> UpdateAsync(Takip takip, CancellationToken cancellationToken = default)
    {
        Guncellenen = takip;
        return Task.FromResult(UpdateSonucu);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        Silinenler.Add(id);
        return Task.FromResult(true);
    }
}

internal sealed class FakeOlcumRepository(FakeTakipRepository takipler) : IOlcumRepository
{
    public List<Olcum> Items { get; } = [];
    public Olcum? Eklenen { get; private set; }
    public Olcum? Guncellenen { get; private set; }
    public List<int> Silinenler { get; } = [];

    public Task<Olcum?> GetByIdAsync(int id, CancellationToken ct = default)
        => Task.FromResult(Items.FirstOrDefault(o => o.Id == id));

    public Task<IReadOnlyList<Olcum>> ListByTakipIdAsync(int takipId, CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<Olcum>>(Items.Where(o => o.TakipId == takipId).OrderBy(o => o.Id).ToList());

    public Task<IReadOnlyList<OlcumKaydi>> ListBySporcuIdAsync(int sporcuId, CancellationToken ct = default)
    {
        var liste = Items
            .Join(takipler.Items.Where(t => t.SporcuId == sporcuId), o => o.TakipId, t => t.Id,
                (o, t) => new OlcumKaydi(o.Id, o.TakipId, t.Tarih, o.Tur, o.Deger, o.Birim))
            .OrderBy(k => k.Tarih).ThenBy(k => k.Id)
            .ToList();
        return Task.FromResult<IReadOnlyList<OlcumKaydi>>(liste);
    }

    public Task<int> AddAsync(Olcum olcum, CancellationToken ct = default)
    {
        Eklenen = olcum;
        return Task.FromResult(5);
    }

    public Task<bool> UpdateAsync(Olcum olcum, CancellationToken ct = default)
    {
        Guncellenen = olcum;
        var idx = Items.FindIndex(o => o.Id == olcum.Id);
        if (idx < 0) return Task.FromResult(false);
        Items[idx] = olcum;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        Silinenler.Add(id);
        return Task.FromResult(Items.RemoveAll(o => o.Id == id) > 0);
    }
}
