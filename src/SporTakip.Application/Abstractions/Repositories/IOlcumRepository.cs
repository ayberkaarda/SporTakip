using SporTakip.Domain;

namespace SporTakip.Application.Abstractions.Repositories;

public interface IOlcumRepository
{
    Task<Olcum?> GetByIdAsync(int id, CancellationToken ct = default);

    Task<IReadOnlyList<Olcum>> ListByTakipIdAsync(int takipId, CancellationToken ct = default);

    /// <summary>Sporcunun tüm ölçümleri, takip tarihiyle birlikte.</summary>
    Task<IReadOnlyList<OlcumKaydi>> ListBySporcuIdAsync(int sporcuId, CancellationToken ct = default);

    /// <returns>Eklenen kaydın Id'si.</returns>
    Task<int> AddAsync(Olcum olcum, CancellationToken ct = default);

    Task<bool> UpdateAsync(Olcum olcum, CancellationToken ct = default);

    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
