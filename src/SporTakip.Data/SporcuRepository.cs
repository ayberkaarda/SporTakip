using Dapper;
using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Domain;

namespace SporTakip.Data;

public sealed class SporcuRepository : ISporcuRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public SporcuRepository(IDbConnectionFactory connectionFactory)
    {
        ArgumentNullException.ThrowIfNull(connectionFactory);
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<Sporcu>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("SELECT Id, Ad, SporDali FROM dbo.Sporcular ORDER BY Id;", null, cancellationToken: cancellationToken);
        var rows = await connection.QueryAsync<Sporcu>(command).ConfigureAwait(false);
        return rows.AsList();
    }

    public async Task<Sporcu?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("SELECT Id, Ad, SporDali FROM dbo.Sporcular WHERE Id = @Id;", new { Id = id }, cancellationToken: cancellationToken);
        return await connection.QuerySingleOrDefaultAsync<Sporcu>(command).ConfigureAwait(false);
    }

    public async Task<int> AddAsync(Sporcu sporcu, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(sporcu);
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("INSERT INTO dbo.Sporcular (Ad, SporDali) VALUES (@Ad, @SporDali); SELECT CAST(SCOPE_IDENTITY() AS int);", sporcu, cancellationToken: cancellationToken);
        return await connection.ExecuteScalarAsync<int>(command).ConfigureAwait(false);
    }

    public async Task<bool> UpdateAsync(Sporcu sporcu, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(sporcu);
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("UPDATE dbo.Sporcular SET Ad = @Ad, SporDali = @SporDali WHERE Id = @Id;", sporcu, cancellationToken: cancellationToken);
        return await connection.ExecuteAsync(command).ConfigureAwait(false) > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("DELETE FROM dbo.Sporcular WHERE Id = @Id;", new { Id = id }, cancellationToken: cancellationToken);
        return await connection.ExecuteAsync(command).ConfigureAwait(false) > 0;
    }

    public async Task<bool> SporcuyuTumuyleSilAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var parameters = new { Id = id };
            await connection.ExecuteAsync(new CommandDefinition(
                "DELETE FROM dbo.Takipler WHERE SporcuId = @Id;", parameters,
                transaction: transaction, cancellationToken: cancellationToken)).ConfigureAwait(false);
            await connection.ExecuteAsync(new CommandDefinition(
                "DELETE FROM dbo.Antrenmanlar WHERE SporcuId = @Id;", parameters,
                transaction: transaction, cancellationToken: cancellationToken)).ConfigureAwait(false);
            var affectedRows = await connection.ExecuteAsync(new CommandDefinition(
                "DELETE FROM dbo.Sporcular WHERE Id = @Id;", parameters,
                transaction: transaction, cancellationToken: cancellationToken)).ConfigureAwait(false);
            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            return affectedRows > 0;
        }
        catch
        {
            try
            {
                await transaction.RollbackAsync(CancellationToken.None).ConfigureAwait(false);
            }
            catch
            {
                // Preserve the original exception if rollback also fails.
            }
            throw;
        }
    }
}
