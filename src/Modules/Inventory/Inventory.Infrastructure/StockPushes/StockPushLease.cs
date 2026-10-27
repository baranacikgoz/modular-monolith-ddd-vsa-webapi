using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.StockPushes;

/// <summary>
///     The right to push one connection's stock, held as a Postgres session advisory lock. It is atomic across the consumers
///     of every pod (a cache entry that is read and then written is not) and the server drops it when the session ends,
///     whatever killed the process. The lock belongs to the session of the DbContext's connection, so the caller keeps that
///     connection open from <see cref="TryAcquireAsync" /> to <see cref="ReleaseAsync" />.
/// </summary>
internal static class StockPushLease
{
    public static async Task<bool> TryAcquireAsync(
        IInventoryDbContext dbContext, WarehouseConnectionId connectionId, CancellationToken cancellationToken)
    {
        var key = KeyOf(connectionId);
        return await dbContext.Database
            .SqlQuery<bool>($"""SELECT pg_try_advisory_lock(hashtextextended({key}, 0)) AS "Value" """)
            .TagWith(nameof(TryAcquireAsync), connectionId.Value)
            .SingleAsync(cancellationToken);
    }

    /// <summary>Never cancelled with the run: a cancelled run would otherwise keep the connection claimed until its session closes.</summary>
    public static async Task ReleaseAsync(IInventoryDbContext dbContext, WarehouseConnectionId connectionId)
    {
        var key = KeyOf(connectionId);
        await dbContext.Database.ExecuteSqlAsync($"SELECT pg_advisory_unlock(hashtextextended({key}, 0))", CancellationToken.None);
    }

    /// <summary>Hashed to the lock's number in SQL; a key space of its own, so it cannot collide with another feature's lock.</summary>
    private static string KeyOf(WarehouseConnectionId connectionId)
    {
        return $"inventory-stock-push:{connectionId.Value:N}";
    }
}
