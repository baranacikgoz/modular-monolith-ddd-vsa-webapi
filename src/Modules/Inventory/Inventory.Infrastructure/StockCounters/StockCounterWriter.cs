using Inventory.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.StockCounters;

/// <summary>
///     The only writer of <c>Inventory.StoreDailyStockTotals</c>: one atomic upsert per event, so two consumers on the
///     same store never lose an increment (the hot counter exception in CLAUDE.md §4). Each call runs in an explicit
///     transaction that the consumer's SaveChanges then uses for its inbox row, so a redelivered event is rejected by the
///     inbox key and its counter change rolls back with it.
/// </summary>
internal static class StockCounterWriter
{
    /// <summary>Runs <paramref name="write" /> and the pending inbox row in one transaction.</summary>
    public static async Task InTransactionAsync(
        IInventoryDbContext dbContext, Func<Task> write, CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        await write();
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    /// <summary>
    ///     A reservation of <paramref name="quantity" /> units was committed for the store on <paramref name="day" />:
    ///     creates the day's row or adds to it in the same statement, whatever other consumer is doing the same.
    /// </summary>
    public static Task AddCommittedAsync(
        IInventoryDbContext dbContext, DefaultIdType storeId, int quantity, DateOnly day, DateTimeOffset now,
        CancellationToken cancellationToken)
    {
        return dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"""
             INSERT INTO "Inventory"."StoreDailyStockTotals" ("StoreId", "Day", "CommittedReservations", "CommittedUnits", "CreatedOn")
             VALUES ({storeId}, {day}, 1, {quantity}, {now})
             ON CONFLICT ("StoreId", "Day") DO UPDATE SET
                 "CommittedReservations" = "StoreDailyStockTotals"."CommittedReservations" + EXCLUDED."CommittedReservations",
                 "CommittedUnits" = "StoreDailyStockTotals"."CommittedUnits" + EXCLUDED."CommittedUnits",
                 "LastModifiedOn" = {now}
             """,
            cancellationToken);
    }
}
