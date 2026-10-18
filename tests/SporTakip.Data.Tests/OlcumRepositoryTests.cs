using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using SporTakip.Domain;
using Xunit;

namespace SporTakip.Data.Tests;

[Collection("LocalDB")]
public sealed class OlcumRepositoryTests
{
    private readonly SporcuRepository _sporcular;
    private readonly TakipRepository _takipler;
    private readonly OlcumRepository _olcumler;

    public OlcumRepositoryTests(DatabaseFixture database)
    {
        _sporcular = new(database.ConnectionFactory);
        _takipler = new(database.ConnectionFactory);
        _olcumler = new(database.ConnectionFactory);
    }

    private Task<int> AddSporcuAsync() => _sporcular.AddAsync(new Sporcu
    {
        Ad = $"Test {Guid.NewGuid():N}", SporDali = "Yüzme"
    });

    private Task<int> AddTakipAsync(int sporcuId, DateTime tarih) =>
        _takipler.AddAsync(new Takip { SporcuId = sporcuId, Tarih = tarih });

    private Task<int> AddOlcumAsync(int takipId) => _olcumler.AddAsync(new Olcum
    {
        TakipId = takipId, Tur = "Kilo", Deger = 72.50m, Birim = "kg"
    });

    [LocalDbFact]
    public async Task Olcum_Add_Get_Update_Delete()
    {
        var owner = await AddSporcuAsync();
        var olcum = new Olcum
        {
            TakipId = await AddTakipAsync(owner, new DateTime(2026, 1, 1)),
            Tur = "Kilo", Deger = 72.50m, Birim = "kg"
        };
        olcum.Id = await _olcumler.AddAsync(olcum);
        olcum.Id.Should().BePositive();
        (await _olcumler.GetByIdAsync(olcum.Id)).Should().BeEquivalentTo(olcum);

        olcum.TakipId = await AddTakipAsync(owner, new DateTime(2026, 1, 2));
        olcum.Tur = "Boy";
        olcum.Deger = 180.25m;
        olcum.Birim = null;
        (await _olcumler.UpdateAsync(olcum)).Should().BeTrue();
        (await _olcumler.GetByIdAsync(olcum.Id)).Should().BeEquivalentTo(olcum);
        (await _olcumler.DeleteAsync(olcum.Id)).Should().BeTrue();
        (await _olcumler.GetByIdAsync(olcum.Id)).Should().BeNull();
        (await _olcumler.DeleteAsync(olcum.Id)).Should().BeFalse();
        (await _olcumler.UpdateAsync(olcum)).Should().BeFalse();
    }

    [LocalDbFact]
    public async Task Olcum_ListByTakipId_FiltersAndOrdersById()
    {
        var owner = await AddSporcuAsync();
        var takip = await AddTakipAsync(owner, new DateTime(2026, 1, 1));
        var other = await AddTakipAsync(owner, new DateTime(2026, 1, 2));
        var first = await AddOlcumAsync(takip);
        await AddOlcumAsync(other);
        var second = await AddOlcumAsync(takip);

        var rows = await _olcumler.ListByTakipIdAsync(takip);
        rows.Select(x => x.Id).Should().Equal(first, second);
        rows.Should().OnlyContain(x => x.TakipId == takip);
        (await _olcumler.ListByTakipIdAsync(-1)).Should().BeEmpty();
    }

    [LocalDbFact]
    public async Task Olcum_ListBySporcuId_FiltersByOwnerAndOrdersByDateThenIdAscending()
    {
        var owner = await AddSporcuAsync();
        var otherOwner = await AddSporcuAsync();
        var date = new DateTime(2026, 5, 10);
        var middleTakip = await AddTakipAsync(owner, date);
        var sameDateTakip = await AddTakipAsync(owner, date);
        var newestTakip = await AddTakipAsync(owner, date.AddDays(1));
        var oldestTakip = await AddTakipAsync(owner, date.AddDays(-1));
        var otherTakip = await AddTakipAsync(otherOwner, date);
        var newest = await AddOlcumAsync(newestTakip);
        var middleFirst = await AddOlcumAsync(sameDateTakip);
        var oldest = await AddOlcumAsync(oldestTakip);
        await AddOlcumAsync(otherTakip);
        var middleSecond = await AddOlcumAsync(middleTakip);
        var middleThird = await AddOlcumAsync(sameDateTakip);

        var rows = await _olcumler.ListBySporcuIdAsync(owner);
        rows.Should().Equal(
            new OlcumKaydi(oldest, oldestTakip, date.AddDays(-1), "Kilo", 72.50m, "kg"),
            new OlcumKaydi(middleFirst, sameDateTakip, date, "Kilo", 72.50m, "kg"),
            new OlcumKaydi(middleSecond, middleTakip, date, "Kilo", 72.50m, "kg"),
            new OlcumKaydi(middleThird, sameDateTakip, date, "Kilo", 72.50m, "kg"),
            new OlcumKaydi(newest, newestTakip, date.AddDays(1), "Kilo", 72.50m, "kg"));
        (await _olcumler.ListBySporcuIdAsync(-1)).Should().BeEmpty();
    }

    [LocalDbFact]
    public async Task Olcum_ListBySporcuId_SkipsNullDatesOnLegacyNullableSchema()
    {
        // 004 Tarih'i NOT NULL yapar; NULL tarih yalnizca 004'un donusturemedigi eski semada mumkundur.
        var builder = new SqlConnectionStringBuilder(
            Environment.GetEnvironmentVariable(DatabaseFixture.ConnectionVariable)!)
        {
            InitialCatalog = "master", AttachDBFilename = string.Empty, Pooling = false
        };
        var name = $"SporTakipNullTarihTest_{Guid.NewGuid():N}";
        try
        {
            await using (var master = new SqlConnection(builder.ConnectionString))
            {
                await master.OpenAsync();
                await master.ExecuteAsync($"CREATE DATABASE [{name}];");
            }
            builder.InitialCatalog = name;
            var olcumler = new OlcumRepository(new SqlConnectionFactory(builder.ConnectionString));
            await using var connection = new SqlConnection(builder.ConnectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync("""
                CREATE TABLE dbo.Sporcular (
                    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    Ad NVARCHAR(100) NOT NULL,
                    SporDali NVARCHAR(100) NULL);
                CREATE TABLE dbo.Takipler (
                    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    SporcuId INT NULL FOREIGN KEY REFERENCES dbo.Sporcular(Id),
                    Tarih DATETIME NULL,
                    Aciklama NVARCHAR(MAX) NULL);
                CREATE TABLE dbo.Olcumler (
                    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    TakipId INT NOT NULL FOREIGN KEY REFERENCES dbo.Takipler(Id),
                    Tur NVARCHAR(50) NOT NULL,
                    Deger DECIMAL(9,2) NOT NULL,
                    Birim NVARCHAR(20) NULL);
                INSERT INTO dbo.Sporcular (Ad, SporDali) VALUES (N'Eski sporcu', N'Atletizm');
                INSERT INTO dbo.Takipler (SporcuId, Tarih) VALUES (1, NULL), (1, '20260510');
                INSERT INTO dbo.Olcumler (TakipId, Tur, Deger, Birim)
                    VALUES (1, N'Kilo', 70.00, N'kg'), (2, N'Kilo', 72.50, N'kg');
                """);

            (await olcumler.ListBySporcuIdAsync(1)).Should().Equal(
                new OlcumKaydi(2, 2, new DateTime(2026, 5, 10), "Kilo", 72.50m, "kg"));
            (await olcumler.GetByIdAsync(1)).Should().NotBeNull();
        }
        finally
        {
            builder.InitialCatalog = "master";
            await using var master = new SqlConnection(builder.ConnectionString);
            await master.OpenAsync();
            // Identifier consists only of the constant prefix and a GUID generated above.
            await master.ExecuteAsync(
                $"IF DB_ID(@name) IS NOT NULL BEGIN ALTER DATABASE [{name}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{name}]; END;",
                new { name });
        }
    }

    [LocalDbFact]
    public async Task SporcuyuTumuyleSil_CascadesToOlcumlerAndPreservesOtherAthlete()
    {
        var owner = await AddSporcuAsync();
        var otherOwner = await AddSporcuAsync();
        var date = new DateTime(2026, 1, 1);
        var firstTakip = await AddTakipAsync(owner, date);
        var secondTakip = await AddTakipAsync(owner, date.AddDays(1));
        var otherTakip = await AddTakipAsync(otherOwner, date);
        var deletedIds = new[]
        {
            await AddOlcumAsync(firstTakip),
            await AddOlcumAsync(firstTakip),
            await AddOlcumAsync(secondTakip)
        };
        var preservedId = await AddOlcumAsync(otherTakip);
        foreach (var id in deletedIds)
            (await _olcumler.GetByIdAsync(id)).Should().NotBeNull();

        (await _sporcular.SporcuyuTumuyleSilAsync(owner)).Should().BeTrue();

        (await _sporcular.GetByIdAsync(owner)).Should().BeNull();
        (await _takipler.GetByIdAsync(firstTakip)).Should().BeNull();
        (await _takipler.GetByIdAsync(secondTakip)).Should().BeNull();
        foreach (var id in deletedIds)
            (await _olcumler.GetByIdAsync(id)).Should().BeNull();
        (await _olcumler.ListByTakipIdAsync(firstTakip)).Should().BeEmpty();
        (await _olcumler.ListByTakipIdAsync(secondTakip)).Should().BeEmpty();
        (await _olcumler.ListBySporcuIdAsync(owner)).Should().BeEmpty();
        (await _sporcular.GetByIdAsync(otherOwner)).Should().NotBeNull();
        (await _olcumler.GetByIdAsync(preservedId)).Should().NotBeNull();
        (await _olcumler.ListBySporcuIdAsync(otherOwner)).Select(x => x.Id).Should().Equal(preservedId);
    }
}
