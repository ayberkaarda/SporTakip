using System.Data.Common;

namespace SporTakip.Data;

public interface IDbConnectionFactory
{
    /// <summary>Açılmış yeni bir bağlantı döner; dispose etmek çağıranın sorumluluğundadır.</summary>
    Task<DbConnection> OpenConnectionAsync(CancellationToken cancellationToken = default);
}
