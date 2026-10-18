using Dapper;
using FluentAssertions;
using Xunit;

namespace SporTakip.Data.Tests;

[Collection("LocalDB")]
public sealed class ExistingDatabaseMigrationTests
{
    private readonly DatabaseFixture _database;
    public ExistingDatabaseMigrationTests(DatabaseFixture database) => _database = database;

    [LocalDbFact]
    public async Task ExistingTablesWithoutVersionHistory_CanBeMigratedWithoutLosingRows()
    {
        await using var connection = await _database.ConnectionFactory.OpenConnectionAsync();
        await connection.ExecuteAsync("INSERT INTO dbo.Sporcular (Ad, SporDali) VALUES (N'Migration kontrol', N'Atletizm');");
        var count = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.Sporcular;");
        await new SchemaMigrator(_database.ConnectionString).MigrateAsync();
        (await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.Sporcular;")).Should().Be(count);
        var expected = typeof(SchemaMigrator).Assembly.GetManifestResourceNames()
            .Count(n => n.StartsWith("Migrations.0", StringComparison.Ordinal) && n.EndsWith(".sql", StringComparison.Ordinal));
        (await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.SchemaVersion;")).Should().Be(expected);
    }
}
