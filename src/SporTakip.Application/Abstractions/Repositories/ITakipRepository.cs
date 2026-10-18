using SporTakip.Domain;

namespace SporTakip.Application.Abstractions.Repositories;

public interface ITakipRepository
{
    Task<Takip?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>Tarihe göre azalan sırada (ORDER BY Tarih DESC).</summary>
    Task<IReadOnlyList<Takip>> ListBySporcuIdAsync(int sporcuId, CancellationToken cancellationToken = default);

    /// <returns>Eklenen kaydın Id'si.</returns>
    Task<int> AddAsync(Takip takip, CancellationToken cancellationToken = default);

    /// <returns>Kayıt bulunup güncellendiyse true.</returns>
    Task<bool> UpdateAsync(Takip takip, CancellationToken cancellationToken = default);

    /// <returns>Kayıt bulunup silindiyse true.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
