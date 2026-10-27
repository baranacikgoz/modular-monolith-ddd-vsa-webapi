using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.IntegrationEventHandlers;

/// <summary>
///     The event-driven writers of a stock level load it with <c>SELECT ... FOR UPDATE</c> inside the consumer's
///     explicit transaction, so two movements on one stock level queue on the row lock instead of one of them failing
///     the Version check and waiting for a bus retry. The aggregate methods stay exactly as they are; the transaction
///     ends with the consumer's own SaveChanges and commit, which also carries the inbox row
///     (<c>IntegrationEventHandlerBase</c>).
/// </summary>
internal static class StockLevelRowLock
{
    /// <summary>
    ///     Must run inside an open transaction. ToListAsync on the raw SQL keeps EF from wrapping it in a subquery. A
    ///     product with no stock level is a NotFound result: reservations are accepted for any product id, so the caller
    ///     decides what that means.
    /// </summary>
    public static async Task<Result<StockLevel>> LoadForUpdateAsync(
        IInventoryDbContext dbContext, DefaultIdType productId, CancellationToken cancellationToken)
    {
        var stockLevels = await dbContext.StockLevels
            .FromSql($"SELECT * FROM \"Inventory\".\"StockLevels\" WHERE \"ProductId\" = {productId} FOR UPDATE")
            .ToListAsync(cancellationToken);

        return stockLevels.SingleAsResult(_ => true);
    }

    public static Task<bool> HasDeductionAsync(
        IInventoryDbContext dbContext, DefaultIdType reservationId, CancellationToken cancellationToken)
    {
        return dbContext.StockMovements
            .AsNoTracking()
            .TagWith(nameof(HasDeductionAsync), reservationId)
            .AnyAsync(
                m => m.ReservationId == reservationId && m.Type == StockMovementType.ReservationCommitted,
                cancellationToken);
    }
}
