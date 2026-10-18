using FluentAssertions;
using SporTakip.Domain;
using Xunit;

namespace SporTakip.Data.Tests;

[Collection("LocalDB")]
public sealed class RepositoryTests
{
    private readonly SporcuRepository _sporcular;
    private readonly AntrenmanRepository _antrenmanlar;
    private readonly TakipRepository _takipler;

    public RepositoryTests(DatabaseFixture database)
    {
        _sporcular = new(database.ConnectionFactory);
        _antrenmanlar = new(database.ConnectionFactory);
        _takipler = new(database.ConnectionFactory);
    }

    private async Task<int> AddSporcuAsync() => await _sporcular.AddAsync(new Sporcu
    {
        Ad = $"Test {Guid.NewGuid():N}", SporDali = "Yüzme"
    });

    [LocalDbFact]
    public async Task Sporcu_Add_Get_Update_Delete()
    {
        var sporcu = new Sporcu { Ad = "Çağrı", SporDali = "Yüzme" };
        sporcu.Id = await _sporcular.AddAsync(sporcu);
        sporcu.Id.Should().BePositive();
        (await _sporcular.GetByIdAsync(sporcu.Id)).Should().BeEquivalentTo(sporcu);
        (await _sporcular.GetAllAsync()).Should().ContainEquivalentOf(sporcu);

        sporcu.Ad = "Özgür";
        sporcu.SporDali = "Atletizm";
        (await _sporcular.UpdateAsync(sporcu)).Should().BeTrue();
        (await _sporcular.GetByIdAsync(sporcu.Id)).Should().BeEquivalentTo(sporcu);
        (await _sporcular.DeleteAsync(sporcu.Id)).Should().BeTrue();
        (await _sporcular.GetByIdAsync(sporcu.Id)).Should().BeNull();
    }

    [LocalDbFact]
    public async Task Antrenman_Add_Get_Update_Delete()
    {
        var antrenman = new Antrenman
        {
            SporcuId = await AddSporcuAsync(), AntrenmanAdi = "Isınma", Detaylar = "Üç tur"
        };
        antrenman.Id = await _antrenmanlar.AddAsync(antrenman);
        antrenman.Id.Should().BePositive();
        (await _antrenmanlar.GetByIdAsync(antrenman.Id)).Should().BeEquivalentTo(antrenman);

        antrenman.SporcuId = await AddSporcuAsync();
        antrenman.AntrenmanAdi = "Koşu";
        antrenman.Detaylar = null;
        (await _antrenmanlar.UpdateAsync(antrenman)).Should().BeTrue();
        (await _antrenmanlar.GetByIdAsync(antrenman.Id)).Should().BeEquivalentTo(antrenman);
        (await _antrenmanlar.DeleteAsync(antrenman.Id)).Should().BeTrue();
        (await _antrenmanlar.GetByIdAsync(antrenman.Id)).Should().BeNull();
    }

    [LocalDbFact]
    public async Task Takip_Add_Get_Update_Delete_PersistsDateWithoutTime()
    {
        var takip = new Takip
        {
            SporcuId = await AddSporcuAsync(), Tarih = new DateTime(2026, 3, 5, 14, 30, 0), Aciklama = "İlk ölçüm"
        };
        takip.Id = await _takipler.AddAsync(takip);
        takip.Id.Should().BePositive();
        takip.Tarih = takip.Tarih.Date;
        (await _takipler.GetByIdAsync(takip.Id)).Should().BeEquivalentTo(takip);

        takip.SporcuId = await AddSporcuAsync();
        takip.Tarih = new DateTime(2026, 4, 6, 18, 45, 0);
        takip.Aciklama = null;
        (await _takipler.UpdateAsync(takip)).Should().BeTrue();
        takip.Tarih = takip.Tarih.Date;
        (await _takipler.GetByIdAsync(takip.Id)).Should().BeEquivalentTo(takip);
        (await _takipler.DeleteAsync(takip.Id)).Should().BeTrue();
        (await _takipler.GetByIdAsync(takip.Id)).Should().BeNull();
    }

    [LocalDbFact]
    public async Task Antrenman_ListBySporcuId_FiltersAndOrdersById()
    {
        var sporcuId = await AddSporcuAsync();
        var otherId = await AddSporcuAsync();
        var first = await _antrenmanlar.AddAsync(new Antrenman { SporcuId = sporcuId, AntrenmanAdi = "Bir" });
        await _antrenmanlar.AddAsync(new Antrenman { SporcuId = otherId, AntrenmanAdi = "Diğer" });
        var second = await _antrenmanlar.AddAsync(new Antrenman { SporcuId = sporcuId, AntrenmanAdi = "İki" });

        var rows = await _antrenmanlar.ListBySporcuIdAsync(sporcuId);
        rows.Select(x => x.Id).Should().Equal(first, second);
        rows.Should().OnlyContain(x => x.SporcuId == sporcuId);
    }

    [LocalDbFact]
    public async Task Takip_ListBySporcuId_FiltersAndOrdersByDateThenIdDescending()
    {
        var sporcuId = await AddSporcuAsync();
        var otherId = await AddSporcuAsync();
        var date = new DateTime(2026, 5, 10);
        var middle = await _takipler.AddAsync(new Takip { SporcuId = sporcuId, Tarih = date });
        var oldest = await _takipler.AddAsync(new Takip { SporcuId = sporcuId, Tarih = date.AddDays(-1) });
        await _takipler.AddAsync(new Takip { SporcuId = otherId, Tarih = date.AddDays(10) });
        var newest = await _takipler.AddAsync(new Takip { SporcuId = sporcuId, Tarih = date.AddDays(1) });
        var sameDate = await _takipler.AddAsync(new Takip { SporcuId = sporcuId, Tarih = date });

        var rows = await _takipler.ListBySporcuIdAsync(sporcuId);
        rows.Select(x => x.Id).Should().Equal(newest, sameDate, middle, oldest);
        rows.Should().OnlyContain(x => x.SporcuId == sporcuId);
        rows.Select(x => x.Tarih).Should().BeInDescendingOrder();
    }

    [LocalDbFact]
    public async Task SporcuyuTumuyleSil_RemovesChildrenAndPreservesOtherAthlete()
    {
        var id = await AddSporcuAsync();
        var otherId = await AddSporcuAsync();
        var childIds = new List<(int Antrenman, int Takip)>();
        foreach (var owner in new[] { id, id, otherId })
        {
            childIds.Add((
                await _antrenmanlar.AddAsync(new Antrenman { SporcuId = owner, AntrenmanAdi = "Koşu" }),
                await _takipler.AddAsync(new Takip { SporcuId = owner, Tarih = new DateTime(2026, 1, 1) })));
        }

        (await _sporcular.SporcuyuTumuyleSilAsync(id)).Should().BeTrue();
        (await _sporcular.GetByIdAsync(id)).Should().BeNull();
        (await _antrenmanlar.ListBySporcuIdAsync(id)).Should().BeEmpty();
        (await _takipler.ListBySporcuIdAsync(id)).Should().BeEmpty();
        foreach (var child in childIds.Take(2))
        {
            (await _antrenmanlar.GetByIdAsync(child.Antrenman)).Should().BeNull();
            (await _takipler.GetByIdAsync(child.Takip)).Should().BeNull();
        }
        (await _sporcular.GetByIdAsync(otherId)).Should().NotBeNull();
        (await _antrenmanlar.GetByIdAsync(childIds[2].Antrenman)).Should().NotBeNull();
        (await _takipler.GetByIdAsync(childIds[2].Takip)).Should().NotBeNull();
    }

    [LocalDbFact]
    public async Task Sporcu_Delete_CascadesToChildren()
    {
        var id = await AddSporcuAsync();
        var antrenmanId = await _antrenmanlar.AddAsync(new Antrenman { SporcuId = id });
        var takipId = await _takipler.AddAsync(new Takip { SporcuId = id, Tarih = new DateTime(2026, 1, 1) });

        (await _sporcular.DeleteAsync(id)).Should().BeTrue();
        (await _sporcular.GetByIdAsync(id)).Should().BeNull();
        (await _antrenmanlar.GetByIdAsync(antrenmanId)).Should().BeNull();
        (await _takipler.GetByIdAsync(takipId)).Should().BeNull();
    }

    [LocalDbFact]
    public async Task MissingIds_ReturnNullFalseOrEmpty()
    {
        var owner = await AddSporcuAsync();
        const int missing = -1;
        (await _sporcular.GetByIdAsync(missing)).Should().BeNull();
        (await _sporcular.UpdateAsync(new Sporcu { Id = missing, Ad = "Yok" })).Should().BeFalse();
        (await _sporcular.DeleteAsync(missing)).Should().BeFalse();
        (await _sporcular.SporcuyuTumuyleSilAsync(missing)).Should().BeFalse();
        (await _antrenmanlar.GetByIdAsync(missing)).Should().BeNull();
        (await _antrenmanlar.UpdateAsync(new Antrenman { Id = missing, SporcuId = owner })).Should().BeFalse();
        (await _antrenmanlar.DeleteAsync(missing)).Should().BeFalse();
        (await _antrenmanlar.ListBySporcuIdAsync(missing)).Should().BeEmpty();
        (await _takipler.GetByIdAsync(missing)).Should().BeNull();
        (await _takipler.UpdateAsync(new Takip { Id = missing, SporcuId = owner, Tarih = new DateTime(2026, 1, 1) })).Should().BeFalse();
        (await _takipler.DeleteAsync(missing)).Should().BeFalse();
        (await _takipler.ListBySporcuIdAsync(missing)).Should().BeEmpty();
        (await _sporcular.GetByIdAsync(owner)).Should().NotBeNull();
    }
}
