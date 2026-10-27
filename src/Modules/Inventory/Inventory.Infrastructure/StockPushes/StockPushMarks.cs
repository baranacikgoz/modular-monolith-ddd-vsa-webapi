using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.StockPushes;

/// <summary>A dirty row as a push read it: clearing it later deletes the row only if it still carries this version.</summary>
internal readonly record struct PushedMark(StockLevelId StockLevelId, long MarkVersion);

/// <summary>
///     The only writer of <c>Inventory.StockPushDirtyItems</c>. A mark is one atomic upsert that draws a new version from
///     the sequence, so it never races with another mark or with a clear; a clear deletes a row only if its version is the
///     one the push read, so a mark made while the push ran keeps its row.
/// </summary>
internal static class StockPushMarks
{
    /// <summary>
    ///     Creates the stock level's row or gives the existing one a new version. <c>MarkedOn</c> stays the time of the first
    ///     mark since the row was last cleared. Runs in the transaction of the save that changed the quantity.
    /// </summary>
    public static Task MarkAsync(
        IInventoryDbContext dbContext, StockLevelId stockLevelId, DefaultIdType storeId, DefaultIdType productId, DateTimeOffset now,
        CancellationToken cancellationToken)
    {
        return dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"""
             INSERT INTO "Inventory"."StockPushDirtyItems"
                 ("StockLevelId", "StoreId", "ProductId", "MarkVersion", "MarkedOn", "CreatedOn")
             VALUES ({stockLevelId.Value}, {storeId}, {productId}, nextval('"Inventory"."StockPushMarkVersions"'), {now}, {now})
             ON CONFLICT ("StockLevelId") DO UPDATE SET
                 "MarkVersion" = nextval('"Inventory"."StockPushMarkVersions"'),
                 "LastModifiedOn" = {now}
             """,
            cancellationToken);
    }

    /// <summary>Deletes the rows that still carry the version that was pushed; returns how many were cleared.</summary>
    public static Task<int> ClearAsync(
        IInventoryDbContext dbContext, IReadOnlyCollection<PushedMark> pushed, CancellationToken cancellationToken)
    {
        var ids = pushed.Select(p => p.StockLevelId.Value).ToArray();
        var versions = pushed.Select(p => p.MarkVersion).ToArray();

        return dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"""
             DELETE FROM "Inventory"."StockPushDirtyItems" AS d
             USING unnest({ids}, {versions}) AS p("StockLevelId", "MarkVersion")
             WHERE d."StockLevelId" = p."StockLevelId" AND d."MarkVersion" = p."MarkVersion"
             """,
            cancellationToken);
    }
}
