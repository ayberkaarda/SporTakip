using SporTakip.Domain;

namespace SporTakip.Application.Abstractions.Repositories;

public interface ISporcuRepository
{
    Task<IReadOnlyList<Sporcu>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Sporcu?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <returns>Eklenen kaydın Id'si.</returns>
    Task<int> AddAsync(Sporcu sporcu, CancellationToken cancellationToken = default);

    /// <returns>Kayıt bulunup güncellendiyse true.</returns>
    Task<bool> UpdateAsync(Sporcu sporcu, CancellationToken cancellationToken = default);

    /// <summary>Yalnızca sporcu satırını siler; ilişkili kayıt varsa FK hatası verebilir.</summary>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>Sporcunun takip ve antrenman kayıtlarını ve kendisini tek transaction içinde siler.</summary>
    /// <returns>Sporcu bulunup silindiyse true.</returns>
    Task<bool> SporcuyuTumuyleSilAsync(int id, CancellationToken cancellationToken = default);
}
