using System.Data;
using Dapper;
using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Domain;

namespace SporTakip.Data;

public sealed class TakipRepository : ITakipRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public TakipRepository(IDbConnectionFactory connectionFactory)
    {
        ArgumentNullException.ThrowIfNull(connectionFactory);
        _connectionFactory = connectionFactory;
    }

    public async Task<Takip?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("SELECT Id, SporcuId, Tarih, Aciklama FROM dbo.Takipler WHERE Id = @Id;", new { Id = id }, cancellationToken: cancellationToken);
        return await connection.QuerySingleOrDefaultAsync<Takip>(command).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<Takip>> ListBySporcuIdAsync(int sporcuId, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("SELECT Id, SporcuId, Tarih, Aciklama FROM dbo.Takipler WHERE SporcuId = @SporcuId ORDER BY Tarih DESC, Id DESC;", new { SporcuId = sporcuId }, cancellationToken: cancellationToken);
        var rows = await connection.QueryAsync<Takip>(command).ConfigureAwait(false);
        return rows.AsList();
    }

    public async Task<int> AddAsync(Takip takip, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(takip);
        var parameters = new DynamicParameters();
        parameters.Add("Id", takip.Id);
        parameters.Add("SporcuId", takip.SporcuId);
        parameters.Add("Tarih", takip.Tarih.Date, DbType.Date);
        parameters.Add("Aciklama", takip.Aciklama);
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("INSERT INTO dbo.Takipler (SporcuId, Tarih, Aciklama) VALUES (@SporcuId, @Tarih, @Aciklama); SELECT CAST(SCOPE_IDENTITY() AS int);", parameters, cancellationToken: cancellationToken);
        return await connection.ExecuteScalarAsync<int>(command).ConfigureAwait(false);
    }

    public async Task<bool> UpdateAsync(Takip takip, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(takip);
        var parameters = new DynamicParameters();
        parameters.Add("Id", takip.Id);
        parameters.Add("SporcuId", takip.SporcuId);
        parameters.Add("Tarih", takip.Tarih.Date, DbType.Date);
        parameters.Add("Aciklama", takip.Aciklama);
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("UPDATE dbo.Takipler SET SporcuId = @SporcuId, Tarih = @Tarih, Aciklama = @Aciklama WHERE Id = @Id;", parameters, cancellationToken: cancellationToken);
        return await connection.ExecuteAsync(command).ConfigureAwait(false) > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        var command = new CommandDefinition("DELETE FROM dbo.Takipler WHERE Id = @Id;", new { Id = id }, cancellationToken: cancellationToken);
        return await connection.ExecuteAsync(command).ConfigureAwait(false) > 0;
    }
}
