using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace SporTakip.Data;

public sealed class SchemaMigrator
{
    private readonly string _connectionString;

    public SchemaMigrator(string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        _connectionString = connectionString;
    }

    public async Task MigrateAsync(CancellationToken ct = default)
    {
        var builder = new SqlConnectionStringBuilder(_connectionString);
        var databaseName = builder.InitialCatalog;
        ArgumentException.ThrowIfNullOrWhiteSpace(databaseName);
        var masterBuilder = new SqlConnectionStringBuilder(_connectionString)
        {
            InitialCatalog = "master", AttachDBFilename = string.Empty
        };
        await using (var master = new SqlConnection(masterBuilder.ConnectionString))
        {
            await master.OpenAsync(ct).ConfigureAwait(false);
            await using var create = master.CreateCommand();
            // The identifier cannot be a SQL parameter. Validate only when creation is needed,
            // and quote on the server; existing database names need not follow this restriction.
            create.CommandText = """
                IF DB_ID(@name) IS NULL
                BEGIN
                    IF LEN(@name) > 128 OR @name COLLATE Latin1_General_100_BIN2 LIKE N'%[^A-Za-z0-9_]%'
                        THROW 50000, 'Invalid database name for creation.', 1;
                    DECLARE @sql nvarchar(max) = N'CREATE DATABASE ' + QUOTENAME(@name);
                    EXEC sys.sp_executesql @sql;
                END;
                """;
            create.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 4000).Value = databaseName;
            await create.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
        }

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(ct).ConfigureAwait(false);
        await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync(ct).ConfigureAwait(false);
        // Serialize runners so version checks and DDL cannot race on the same database.
        await ExecuteAsync("""
            DECLARE @result int;
            EXEC @result = sys.sp_getapplock @Resource=N'SporTakip.SchemaMigrator',
                @LockMode=N'Exclusive', @LockOwner=N'Transaction', @LockTimeout=60000;
            IF @result < 0 THROW 50001, 'Migration lock could not be acquired.', 1;
            IF OBJECT_ID(N'dbo.SchemaVersion', N'U') IS NULL
                CREATE TABLE dbo.SchemaVersion (
                    Surum NVARCHAR(100) NOT NULL PRIMARY KEY,
                    UygulamaTarihi DATETIME2 NOT NULL
                );
            """).ConfigureAwait(false);

        // Existing databases without SchemaVersion safely replay the idempotent scripts.
        var assembly = typeof(SchemaMigrator).Assembly;
        foreach (var resource in assembly.GetManifestResourceNames()
                     .Where(n => n.StartsWith("Migrations.0", StringComparison.Ordinal) && n.EndsWith(".sql", StringComparison.Ordinal))
                     .OrderBy(n => n, StringComparer.Ordinal))
        {
            var version = resource["Migrations.".Length..];
            await using var check = connection.CreateCommand();
            check.Transaction = transaction;
            check.CommandText = "SELECT COUNT(*) FROM dbo.SchemaVersion WHERE Surum = @version;";
            check.Parameters.Add("@version", System.Data.SqlDbType.NVarChar, 100).Value = version;
            if (Convert.ToInt32(await check.ExecuteScalarAsync(ct).ConfigureAwait(false)) != 0)
                continue;

            await using var stream = assembly.GetManifestResourceStream(resource)
                ?? throw new InvalidOperationException($"Migration resource missing: {resource}");
            using var reader = new StreamReader(stream);
            var sql = await reader.ReadToEndAsync(ct).ConfigureAwait(false);
            foreach (var batch in Regex.Split(sql, @"^[\t ]*GO[\t ]*(?:--[^\r\n]*)?\r?$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
                if (!string.IsNullOrWhiteSpace(batch))
                    await ExecuteAsync(batch).ConfigureAwait(false);

            await using var record = connection.CreateCommand();
            record.Transaction = transaction;
            record.CommandText = "INSERT INTO dbo.SchemaVersion (Surum, UygulamaTarihi) VALUES (@version, SYSUTCDATETIME());";
            record.Parameters.Add("@version", System.Data.SqlDbType.NVarChar, 100).Value = version;
            await record.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
        }
        await transaction.CommitAsync(ct).ConfigureAwait(false);
        // Disposal rolls back on failure, including the version records.

        async Task ExecuteAsync(string sql)
        {
            await using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = sql;
            command.CommandTimeout = 120;
            await command.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
        }
    }
}
