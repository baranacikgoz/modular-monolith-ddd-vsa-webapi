using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Domain.StockPushes;
using Inventory.Domain.WarehouseConnections;
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
    ///     Creates the stock level's row or gives the existing one a new version and a fresh start (no refused pushes, no error
    ///     key). <c>MarkedOn</c> stays the time of the first mark since the row was last cleared. Runs in the transaction of the save that changed the quantity.
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
                 "PushAttempts" = 0,
                 "LastErrorKey" = NULL,
                 "LastModifiedOn" = {now}
             """,
            cancellationToken);
    }

    /// <summary>
    ///     Marks the stock level of <paramref name="productId" /> for the active connection of the product's store, in one
    ///     statement: the connection is found (through the local product copy) and the mark is upserted as in
    ///     <see cref="MarkAsync" />, and the connection's id comes back for the push message. No row comes back, and nothing is
    ///     marked, for a product whose store has no active connection or is not copied yet. Runs in the transaction of the save
    ///     that changed the quantity.
    /// </summary>
    public static async Task<IReadOnlyList<DefaultIdType>> MarkForActiveConnectionAsync(
        IInventoryDbContext dbContext, StockLevelId stockLevelId, DefaultIdType productId, DateTimeOffset now,
        CancellationToken cancellationToken)
    {
        var active = nameof(WarehouseConnectionStatus.Active);

        return await dbContext.Database
            .SqlQuery<DefaultIdType>(
                $"""
                 WITH "ActiveConnection" AS (
                     SELECT c."Id", c."StoreId"
                     FROM "Inventory"."WarehouseConnections" AS c
                     WHERE c."Status" = {active}
                       AND EXISTS (
                           SELECT 1 FROM "Inventory"."ProductSummaries" AS p
                           WHERE p."ProductId" = {productId} AND p."StoreId" = c."StoreId")
                 ), "Marked" AS (
                     INSERT INTO "Inventory"."StockPushDirtyItems"
                         ("StockLevelId", "StoreId", "ProductId", "MarkVersion", "MarkedOn", "CreatedOn")
                     SELECT {stockLevelId.Value}, a."StoreId", {productId}, nextval('"Inventory"."StockPushMarkVersions"'), {now}, {now}
                     FROM "ActiveConnection" AS a
                     ON CONFLICT ("StockLevelId") DO UPDATE SET
                         "MarkVersion" = nextval('"Inventory"."StockPushMarkVersions"'),
                         "PushAttempts" = 0,
                         "LastErrorKey" = NULL,
                         "LastModifiedOn" = {now}
                     RETURNING "StockLevelId"
                 )
                 SELECT a."Id" AS "Value" FROM "ActiveConnection" AS a
                 """)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    ///     Counts one refused push on the row, if it still carries the version that was refused: a mark made meanwhile starts
    ///     the count over and is not to be overwritten.
    /// </summary>
    public static Task<int> RecordRefusalAsync(
        IInventoryDbContext dbContext, PushedMark refused, string errorKey, DateTimeOffset now, CancellationToken cancellationToken)
    {
        var key = errorKey.Length > StockPushDirtyItem.LastErrorKeyMaxLength
            ? errorKey[..StockPushDirtyItem.LastErrorKeyMaxLength]
            : errorKey;

        return dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"""
             UPDATE "Inventory"."StockPushDirtyItems"
             SET "PushAttempts" = "PushAttempts" + 1, "LastErrorKey" = {key}, "LastModifiedOn" = {now}
             WHERE "StockLevelId" = {refused.StockLevelId.Value} AND "MarkVersion" = {refused.MarkVersion}
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
