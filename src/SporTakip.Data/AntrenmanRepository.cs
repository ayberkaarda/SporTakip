using Dapper;
using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Domain;

namespace SporTakip.Data;

public sealed class AntrenmanRepository : IAntrenmanRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public AntrenmanRepository(IDbConnectionFactory connectionFactory)
    {
        ArgumentNullException.ThrowIfNull(connectionFactory);
        _connectionFactory = connectionFactory;
    }

    public async Task<Antrenman?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("SELECT Id, SporcuId, AntrenmanAdi, Detaylar FROM dbo.Antrenmanlar WHERE Id = @Id;", new { Id = id }, cancellationToken: cancellationToken);
        return await connection.QuerySingleOrDefaultAsync<Antrenman>(command).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<Antrenman>> ListBySporcuIdAsync(int sporcuId, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("SELECT Id, SporcuId, AntrenmanAdi, Detaylar FROM dbo.Antrenmanlar WHERE SporcuId = @SporcuId ORDER BY Id;", new { SporcuId = sporcuId }, cancellationToken: cancellationToken);
        var rows = await connection.QueryAsync<Antrenman>(command).ConfigureAwait(false);
        return rows.AsList();
    }

    public async Task<int> AddAsync(Antrenman antrenman, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(antrenman);
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("INSERT INTO dbo.Antrenmanlar (SporcuId, AntrenmanAdi, Detaylar) VALUES (@SporcuId, @AntrenmanAdi, @Detaylar); SELECT CAST(SCOPE_IDENTITY() AS int);", antrenman, cancellationToken: cancellationToken);
        return await connection.ExecuteScalarAsync<int>(command).ConfigureAwait(false);
    }

    public async Task<bool> UpdateAsync(Antrenman antrenman, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(antrenman);
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("UPDATE dbo.Antrenmanlar SET SporcuId = @SporcuId, AntrenmanAdi = @AntrenmanAdi, Detaylar = @Detaylar WHERE Id = @Id;", antrenman, cancellationToken: cancellationToken);
        return await connection.ExecuteAsync(command).ConfigureAwait(false) > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("DELETE FROM dbo.Antrenmanlar WHERE Id = @Id;", new { Id = id }, cancellationToken: cancellationToken);
        return await connection.ExecuteAsync(command).ConfigureAwait(false) > 0;
    }
}
