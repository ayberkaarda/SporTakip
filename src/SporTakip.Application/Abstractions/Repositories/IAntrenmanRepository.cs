using SporTakip.Domain;

namespace SporTakip.Application.Abstractions.Repositories;

public interface IAntrenmanRepository
{
    Task<Antrenman?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Antrenman>> ListBySporcuIdAsync(int sporcuId, CancellationToken cancellationToken = default);

    /// <returns>Eklenen kaydın Id'si.</returns>
    Task<int> AddAsync(Antrenman antrenman, CancellationToken cancellationToken = default);

    /// <returns>Kayıt bulunup güncellendiyse true.</returns>
    Task<bool> UpdateAsync(Antrenman antrenman, CancellationToken cancellationToken = default);

    /// <returns>Kayıt bulunup silindiyse true.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
