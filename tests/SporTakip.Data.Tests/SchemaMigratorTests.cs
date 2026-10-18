using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using SporTakip.Domain;
using Xunit;

namespace SporTakip.Data.Tests;

public sealed class SchemaMigratorTests
{
    [LocalDbFact]
    public async Task EmptyDatabase_AppliesAllScripts_RecordsVersions_SecondRunIsNoOp_AndPersistsGun()
    {
        var builder = new SqlConnectionStringBuilder(Environment.GetEnvironmentVariable(DatabaseFixture.ConnectionVariable)!)
        {
            InitialCatalog = $"SporTakipMigrationTest_{Guid.NewGuid():N}",
            AttachDBFilename = string.Empty, Pooling = false
        };
        var connectionString = builder.ConnectionString;
        var name = builder.InitialCatalog;
        try
        {
            var migrator = new SchemaMigrator(connectionString);
            await migrator.MigrateAsync();
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            var expected = typeof(SchemaMigrator).Assembly.GetManifestResourceNames()
                .Where(n => n.StartsWith("Migrations.0", StringComparison.Ordinal) && n.EndsWith(".sql", StringComparison.Ordinal))
                .Select(n => n["Migrations.".Length..]).OrderBy(n => n).ToArray();
            expected.Should().Contain("005_butunluk_duzeltme.sql");
            var before = (await connection.QueryAsync<(string Surum, DateTime UygulamaTarihi)>(
                "SELECT Surum, UygulamaTarihi FROM dbo.SchemaVersion ORDER BY Surum;")).ToArray();
            before.Select(v => v.Surum).Should().Equal(expected);
            (await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM sys.tables WHERE schema_id=SCHEMA_ID(N'dbo') AND name IN (N'Sporcular',N'Antrenmanlar',N'Takipler',N'Olcumler');"))
                .Should().Be(4);

            var factory = new SqlConnectionFactory(connectionString);
            var sporcuId = await new SporcuRepository(factory).AddAsync(new Sporcu { Ad = "Program", SporDali = "Atletizm" });
            var repository = new AntrenmanRepository(factory);
            var antrenman = new Antrenman { SporcuId = sporcuId, AntrenmanAdi = "Kosu", Gun = 1 };
            antrenman.Id = await repository.AddAsync(antrenman);
            (await repository.GetByIdAsync(antrenman.Id)).Should().BeEquivalentTo(antrenman);
            antrenman.Gun = 7;
            (await repository.UpdateAsync(antrenman)).Should().BeTrue();
            (await repository.ListBySporcuIdAsync(sporcuId)).Should().ContainEquivalentOf(antrenman);
            antrenman.Gun = null;
            (await repository.UpdateAsync(antrenman)).Should().BeTrue();
            (await repository.GetByIdAsync(antrenman.Id))!.Gun.Should().BeNull();

            await migrator.MigrateAsync();
            var after = (await connection.QueryAsync<(string Surum, DateTime UygulamaTarihi)>(
                "SELECT Surum, UygulamaTarihi FROM dbo.SchemaVersion ORDER BY Surum;")).ToArray();
            after.Should().Equal(before);
            (await repository.GetByIdAsync(antrenman.Id)).Should().BeEquivalentTo(antrenman);
        }
        finally
        {
            builder.InitialCatalog = "master";
            await using var master = new SqlConnection(builder.ConnectionString);
            await master.OpenAsync();
            // The name is generated here exclusively from a constant prefix and a GUID.
            await master.ExecuteAsync($"IF DB_ID(@name) IS NOT NULL BEGIN ALTER DATABASE [{name}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{name}]; END;", new { name });
        }
    }
}
