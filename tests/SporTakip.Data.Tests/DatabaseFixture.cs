using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Xunit;

namespace SporTakip.Data.Tests;

public sealed class LocalDbFactAttribute : FactAttribute
{
    public LocalDbFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(DatabaseFixture.ConnectionVariable)))
            Skip = $"{DatabaseFixture.ConnectionVariable} is not set.";
    }
}

[CollectionDefinition("LocalDB")]
public sealed class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
}

public sealed class DatabaseFixture : IAsyncLifetime
{
    public const string ConnectionVariable = "SPORTAKIP_TEST_CONNECTION";
    private readonly string _databaseName = $"SporTakipTest_{Guid.NewGuid():N}";
    private string? _masterConnectionString;
    private bool _created;

    public string ConnectionString { get; private set; } = string.Empty;
    public SqlConnectionFactory ConnectionFactory => new(ConnectionString);

    public async Task InitializeAsync()
    {
        var configured = Environment.GetEnvironmentVariable(ConnectionVariable);
        if (string.IsNullOrWhiteSpace(configured))
            return;

        var builder = new SqlConnectionStringBuilder(configured)
        {
            InitialCatalog = "master",
            AttachDBFilename = string.Empty,
            Pooling = false
        };
        _masterConnectionString = builder.ConnectionString;
        await using var master = new SqlConnection(_masterConnectionString);
        await master.OpenAsync();
        await using (var create = master.CreateCommand())
        {
            create.CommandText = $"CREATE DATABASE [{_databaseName}];";
            await create.ExecuteNonQueryAsync();
            _created = true;
        }

        builder.InitialCatalog = _databaseName;
        ConnectionString = builder.ConnectionString;
        try
        {
            await using var database = new SqlConnection(ConnectionString);
            await database.OpenAsync();
            foreach (var filename in new[] { "001_initial.sql", "002_cascade_fk.sql", "003_olcumler.sql" })
            {
                var sql = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "database", filename));
                foreach (var batch in Regex.Split(sql, @"^\s*GO\s*(?:--[^\r\n]*)?\r?$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(batch))
                        continue;
                    await using var command = database.CreateCommand();
                    command.CommandText = batch;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        catch
        {
            await DisposeAsync();
            throw;
        }
    }

    public async Task DisposeAsync()
    {
        if (!_created)
            return;

        await using var master = new SqlConnection(_masterConnectionString);
        await master.OpenAsync();
        await using var command = master.CreateCommand();
        command.CommandText = $"ALTER DATABASE [{_databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{_databaseName}];";
        await command.ExecuteNonQueryAsync();
        _created = false;
    }
}
