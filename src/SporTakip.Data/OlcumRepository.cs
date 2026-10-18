using Dapper;
using SporTakip.Application.Abstractions.Repositories;
using SporTakip.Domain;

namespace SporTakip.Data;

public sealed class OlcumRepository : IOlcumRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public OlcumRepository(IDbConnectionFactory connectionFactory)
    {
        ArgumentNullException.ThrowIfNull(connectionFactory);
        _connectionFactory = connectionFactory;
    }

    public async Task<Olcum?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(ct).ConfigureAwait(false);
        var command = new CommandDefinition("SELECT Id, TakipId, Tur, Deger, Birim FROM dbo.Olcumler WHERE Id = @Id;", new { Id = id }, cancellationToken: ct);
        return await connection.QuerySingleOrDefaultAsync<Olcum>(command).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<Olcum>> ListByTakipIdAsync(int takipId, CancellationToken ct = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(ct).ConfigureAwait(false);
        var command = new CommandDefinition("SELECT Id, TakipId, Tur, Deger, Birim FROM dbo.Olcumler WHERE TakipId = @TakipId ORDER BY Id;", new { TakipId = takipId }, cancellationToken: ct);
        var rows = await connection.QueryAsync<Olcum>(command).ConfigureAwait(false);
        return rows.AsList();
    }

    public async Task<IReadOnlyList<OlcumKaydi>> ListBySporcuIdAsync(int sporcuId, CancellationToken ct = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(ct).ConfigureAwait(false);
        // OlcumKaydi.Tarih null olamaz; NULL tarihli takipleri eşleme öncesinde dışla.
        var command = new CommandDefinition("SELECT o.Id, o.TakipId, t.Tarih, o.Tur, o.Deger, o.Birim FROM dbo.Olcumler AS o INNER JOIN dbo.Takipler AS t ON t.Id = o.TakipId WHERE t.SporcuId = @SporcuId AND t.Tarih IS NOT NULL ORDER BY t.Tarih ASC, o.Id ASC;", new { SporcuId = sporcuId }, cancellationToken: ct);
        var rows = await connection.QueryAsync<OlcumKaydi>(command).ConfigureAwait(false);
        return rows.AsList();
    }

    public async Task<int> AddAsync(Olcum o, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(o);
        await using var connection = await _connectionFactory.OpenConnectionAsync(ct).ConfigureAwait(false);
        var command = new CommandDefinition("INSERT INTO dbo.Olcumler (TakipId, Tur, Deger, Birim) VALUES (@TakipId, @Tur, @Deger, @Birim); SELECT CAST(SCOPE_IDENTITY() AS int);", o, cancellationToken: ct);
        return await connection.ExecuteScalarAsync<int>(command).ConfigureAwait(false);
    }

    public async Task<bool> UpdateAsync(Olcum o, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(o);
        await using var connection = await _connectionFactory.OpenConnectionAsync(ct).ConfigureAwait(false);
        var command = new CommandDefinition("UPDATE dbo.Olcumler SET TakipId = @TakipId, Tur = @Tur, Deger = @Deger, Birim = @Birim WHERE Id = @Id;", o, cancellationToken: ct);
        return await connection.ExecuteAsync(command).ConfigureAwait(false) > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        await using var connection = await _connectionFactory.OpenConnectionAsync(ct).ConfigureAwait(false);
        var command = new CommandDefinition("DELETE FROM dbo.Olcumler WHERE Id = @Id;", new { Id = id }, cancellationToken: ct);
        return await connection.ExecuteAsync(command).ConfigureAwait(false) > 0;
    }
}
