using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Xunit;

namespace SporTakip.Data.Tests;

public sealed class FaultyMigration004Tests
{
    [LocalDbFact]
    public async Task Faulty004Applied_Migration005RepairsIntegrity_PreservesData_SecondRunIsNoOp()
    {
        var builder = new SqlConnectionStringBuilder(Environment.GetEnvironmentVariable(DatabaseFixture.ConnectionVariable)!)
        {
            InitialCatalog = "master", AttachDBFilename = string.Empty, Pooling = false
        };
        var name = $"SporTakipFaulty004_{Guid.NewGuid():N}";
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
            // State left by the faulty 004: versions recorded, but Ad/SporDali still nullable,
            // no CHECKs, Tarih still DATE NULL.
            await connection.ExecuteAsync("""
                CREATE TABLE dbo.Sporcular (
                    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    Ad NVARCHAR(200) NULL,
                    SporDali NVARCHAR(200) NULL
                );
                CREATE TABLE dbo.Takipler (
                    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    SporcuId INT NULL FOREIGN KEY REFERENCES dbo.Sporcular(Id),
                    Tarih DATE NULL,
                    Aciklama NVARCHAR(MAX) NULL
                );
                CREATE TABLE dbo.SchemaVersion (Surum NVARCHAR(100) NOT NULL PRIMARY KEY, UygulamaTarihi DATETIME2 NOT NULL);
                INSERT INTO dbo.SchemaVersion (Surum, UygulamaTarihi) VALUES
                    (N'001_initial.sql', SYSUTCDATETIME()), (N'002_cascade_fk.sql', SYSUTCDATETIME()),
                    (N'003_olcumler.sql', SYSUTCDATETIME()), (N'004_butunluk.sql', SYSUTCDATETIME());
                INSERT INTO dbo.Sporcular (Ad, SporDali) VALUES (N'Eski sporcu', N'Atletizm');
                INSERT INTO dbo.Takipler (SporcuId, Tarih, Aciklama) VALUES (1, '2026-03-05', N'Korunacak aciklama');
                """);

            await migrator.MigrateAsync();

            var versions = (await connection.QueryAsync<string>("SELECT Surum FROM dbo.SchemaVersion ORDER BY Surum;")).ToArray();
            versions.Should().Contain("005_butunluk_duzeltme.sql");
            await AssertRepairedAsync();
            var schema = await ReadSchemaAsync();

            await migrator.MigrateAsync();
            (await connection.QueryAsync<string>("SELECT Surum FROM dbo.SchemaVersion ORDER BY Surum;")).Should().Equal(versions);
            (await ReadSchemaAsync()).Should().Equal(schema);
            await AssertRepairedAsync();

            async Task<string[]> ReadSchemaAsync() => (await connection.QueryAsync<string>("""
                SELECT CONCAT(object_id, ':', column_id, ':', name, ':', system_type_id, ':', max_length, ':', is_nullable)
                FROM sys.columns WHERE object_id IN (OBJECT_ID(N'dbo.Sporcular'), OBJECT_ID(N'dbo.Takipler'))
                UNION ALL
                SELECT CONCAT(object_id, ':', name, ':', definition, ':', is_disabled, ':', is_not_trusted)
                FROM sys.check_constraints
                ORDER BY 1;
                """)).ToArray();

            async Task AssertRepairedAsync()
            {
                (await connection.ExecuteScalarAsync<int>("""
                    SELECT COUNT(*) FROM sys.columns
                    WHERE object_id=OBJECT_ID(N'dbo.Sporcular') AND name IN (N'Ad',N'SporDali') AND is_nullable=0;
                    """)).Should().Be(2);
                (await connection.ExecuteScalarAsync<bool>(
                    "SELECT is_nullable FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.Takipler') AND name=N'Tarih';"))
                    .Should().BeFalse();
                (await connection.ExecuteScalarAsync<int>("""
                    SELECT COUNT(*) FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.Sporcular')
                        AND name IN (N'CK_Sporcular_Ad_Dolu',N'CK_Sporcular_SporDali_Dolu');
                    """)).Should().Be(2);
                (await connection.QueryAsync<(int Id, string Ad, string SporDali)>("SELECT Id, Ad, SporDali FROM dbo.Sporcular;"))
                    .Should().Equal((1, "Eski sporcu", "Atletizm"));
                (await connection.QueryAsync<(int Id, DateTime Tarih, string Aciklama)>("SELECT Id, Tarih, Aciklama FROM dbo.Takipler;"))
                    .Should().Equal((1, new DateTime(2026, 3, 5), "Korunacak aciklama"));
                Func<Task> invalid = async () => await connection.ExecuteAsync("UPDATE dbo.Sporcular SET Ad=N'   ' WHERE Id=1;");
                (await invalid.Should().ThrowAsync<SqlException>()).Which.Number.Should().Be(547);
            }
        }
        finally
        {
            builder.InitialCatalog = "master";
            await using var master = new SqlConnection(builder.ConnectionString);
            await master.OpenAsync();
            await master.ExecuteAsync(
                $"IF DB_ID(@name) IS NOT NULL BEGIN ALTER DATABASE [{name}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{name}]; END;",
                new { name });
        }
    }
}
