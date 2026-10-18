using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Xunit;

namespace SporTakip.Data.Tests;

public sealed class LegacySchemaMigrationTests
{
    [LocalDbFact]
    public Task TimeComponent_KeepsDatetimeAndAllValues() =>
        VerifyMigrationAsync(new DateTime(2026, 3, 5, 14, 30, 12).AddMilliseconds(120), false, "datetime");

    [LocalDbFact]
    public Task MidnightOnly_ConvertsToDateAndAppliesNotNullBeforeChecks() =>
        VerifyMigrationAsync(new DateTime(2026, 3, 5), false, "date");

    [LocalDbFact]
    public Task NullDate_KeepsNullableDatetimeAndAllValues() =>
        VerifyMigrationAsync(new DateTime(2026, 3, 5), true, "datetime");

    private static async Task VerifyMigrationAsync(DateTime tarih, bool includeNull, string expectedType)
    {
        var builder = new SqlConnectionStringBuilder(
            Environment.GetEnvironmentVariable(DatabaseFixture.ConnectionVariable)!)
        {
            InitialCatalog = "master", AttachDBFilename = string.Empty, Pooling = false
        };
        var name = $"SporTakipLegacyTest_{Guid.NewGuid():N}";
        try
        {
            await using (var master = new SqlConnection(builder.ConnectionString))
            {
                await master.OpenAsync();
                await master.ExecuteAsync($"CREATE DATABASE [{name}];");
            }
            builder.InitialCatalog = name;
            var migrator = new SchemaMigrator(builder.ConnectionString);
            await using var connection = new SqlConnection(builder.ConnectionString);
            await connection.OpenAsync();
            // 001 structure (Ad nullable as in SporTakip.bak), with an older DATETIME Tarih variant.
            // No script reader or GO splitter: only the production migrator executes migrations.
            await connection.ExecuteAsync("""
                CREATE TABLE dbo.Sporcular (
                    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    Ad NVARCHAR(100) NULL,
                    SporDali NVARCHAR(100) NULL
                );
                CREATE TABLE dbo.Antrenmanlar (
                    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    SporcuId INT NULL FOREIGN KEY REFERENCES dbo.Sporcular(Id),
                    AntrenmanAdi NVARCHAR(100) NULL,
                    Detaylar NVARCHAR(MAX) NULL
                );
                CREATE TABLE dbo.Takipler (
                    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    SporcuId INT NULL FOREIGN KEY REFERENCES dbo.Sporcular(Id),
                    Tarih DATETIME NULL,
                    Aciklama NVARCHAR(MAX) NULL
                );
                INSERT INTO dbo.Sporcular (Ad, SporDali) VALUES (N'Eski sporcu', N'Atletizm');
                INSERT INTO dbo.Antrenmanlar (SporcuId, AntrenmanAdi, Detaylar)
                    VALUES (1, N'Kosu', N'Korunacak detay');
                INSERT INTO dbo.Takipler (SporcuId, Tarih, Aciklama)
                    VALUES (1, @tarih, N'Korunacak aciklama');
                IF @includeNull = 1
                    INSERT INTO dbo.Takipler (SporcuId, Tarih, Aciklama) VALUES (1, NULL, N'Eski NULL');
                """, new { tarih, includeNull });

            (await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Sporcular') AND name IN (N'Ad',N'SporDali') AND is_nullable=1;"))
                .Should().Be(2);
            (await connection.ExecuteScalarAsync<string>(
                "SELECT TYPE_NAME(system_type_id) FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Takipler') AND name=N'Tarih';"))
                .Should().Be("datetime");
            (await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.Sporcular');"))
                .Should().Be(0);

            await migrator.MigrateAsync();

            var expectedVersions = typeof(SchemaMigrator).Assembly.GetManifestResourceNames()
                .Where(n => n.StartsWith("Migrations.0", StringComparison.Ordinal) && n.EndsWith(".sql", StringComparison.Ordinal))
                .Select(n => n["Migrations.".Length..]).OrderBy(n => n, StringComparer.Ordinal).ToArray();
            expectedVersions.Should().Contain("005_butunluk_duzeltme.sql");
            var versions = (await connection.QueryAsync<(string Surum, DateTime UygulamaTarihi)>(
                "SELECT Surum, UygulamaTarihi FROM dbo.SchemaVersion ORDER BY Surum;")).ToArray();
            versions.Select(v => v.Surum).Should().Equal(expectedVersions);
            await AssertStateAsync();
            var schema = await ReadSchemaAsync();
            await migrator.MigrateAsync();
            (await connection.QueryAsync<(string Surum, DateTime UygulamaTarihi)>(
                "SELECT Surum, UygulamaTarihi FROM dbo.SchemaVersion ORDER BY Surum;")).Should().Equal(versions);
            (await ReadSchemaAsync()).Should().Equal(schema);
            await AssertStateAsync();

            async Task<string[]> ReadSchemaAsync() => (await connection.QueryAsync<string>("""
                SELECT CONCAT(object_id, ':', column_id, ':', name, ':', system_type_id, ':', max_length, ':', is_nullable)
                FROM sys.columns WHERE object_id IN
                    (OBJECT_ID(N'dbo.Sporcular'), OBJECT_ID(N'dbo.Antrenmanlar'), OBJECT_ID(N'dbo.Takipler'), OBJECT_ID(N'dbo.Olcumler'))
                UNION ALL
                SELECT CONCAT(object_id, ':', name, ':', definition, ':', is_disabled, ':', is_not_trusted)
                FROM sys.check_constraints
                ORDER BY 1;
                """)).ToArray();

            async Task AssertStateAsync()
            {
                (await connection.ExecuteScalarAsync<string>(
                    "SELECT TYPE_NAME(system_type_id) FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Takipler') AND name=N'Tarih';"))
                    .Should().Be(expectedType);
                (await connection.ExecuteScalarAsync<bool>(
                    "SELECT is_nullable FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Takipler') AND name=N'Tarih';"))
                    .Should().Be(expectedType != "date");
                (await connection.ExecuteScalarAsync<int>("""
                    SELECT COUNT(*) FROM sys.columns
                    WHERE object_id=OBJECT_ID(N'dbo.Sporcular') AND name IN (N'Ad',N'SporDali')
                        AND is_nullable=0 AND system_type_id=TYPE_ID(N'nvarchar') AND max_length=200;
                    """)).Should().Be(2);
                (await connection.ExecuteScalarAsync<int>("""
                    SELECT COUNT(*) FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.Sporcular')
                        AND name IN (N'CK_Sporcular_Ad_Dolu',N'CK_Sporcular_SporDali_Dolu') AND is_disabled=0;
                    """)).Should().Be(2);
                (await connection.QueryAsync<(int Id, string Ad, string SporDali)>(
                    "SELECT Id, Ad, SporDali FROM dbo.Sporcular;"))
                    .Should().Equal((1, "Eski sporcu", "Atletizm"));
                (await connection.QueryAsync<(int Id, int SporcuId, string AntrenmanAdi, string Detaylar)>(
                    "SELECT Id, SporcuId, AntrenmanAdi, Detaylar FROM dbo.Antrenmanlar;"))
                    .Should().Equal((1, 1, "Kosu", "Korunacak detay"));
                var rows = (await connection.QueryAsync<(int Id, int SporcuId, DateTime? Tarih, string Aciklama)>(
                    "SELECT Id, SporcuId, Tarih, Aciklama FROM dbo.Takipler ORDER BY Id;")).ToArray();
                rows.Should().HaveCount(includeNull ? 2 : 1);
                rows[0].Should().Be((1, 1, (DateTime?)tarih, "Korunacak aciklama"));
                if (includeNull)
                    rows[1].Should().Be((2, 1, (DateTime?)null, "Eski NULL"));

                // Verify both CHECKs reject whitespace, independently of NOT NULL metadata.
                foreach (var column in new[] { "Ad", "SporDali" })
                {
                    Func<Task> invalidUpdate = async () =>
                        await connection.ExecuteAsync($"UPDATE dbo.Sporcular SET [{column}]=N'   ' WHERE Id=1;");
                    var failure = await invalidUpdate.Should().ThrowAsync<SqlException>();
                    failure.Which.Number.Should().Be(547);
                    failure.Which.Message.Should().Contain($"CK_Sporcular_{column}_Dolu");
                }
            }
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
}
