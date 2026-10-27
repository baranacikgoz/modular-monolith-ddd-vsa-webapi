using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Domain.ResultMonad;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Infrastructure.Telemetry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Infrastructure.IntegrationEventHandlers;

/// <summary>
///     A committed reservation takes its quantity out of the on-hand stock by choreography: the reservation is committed
///     locally and publishes <see cref="StockReservationCommittedIntegrationEvent" />; this consumer loads the stock level
///     under a row lock and deducts in one transaction with the inbox row. Idempotent three ways: the inbox row (same
///     message), the ledger check under the row lock (the same reservation under a new message id) and the unique index
///     on the ledger's reservation id as the last backstop. A reservation that cannot be deducted (no stock level for its
///     product, more than the stock on hand) is a business outcome, not a fault: it is logged and acknowledged, because
///     retrying changes nothing and a failing message would block the queue.
/// </summary>
public sealed partial class DeductStockOnReservationCommittedHandler(
    IInventoryDbContext dbContext,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox,
    ILogger<DeductStockOnReservationCommittedHandler> logger
) : IntegrationEventHandlerBase<StockReservationCommittedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override async Task ProcessAsync(StockReservationCommittedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        var outcome = await StockLevelRowLock.LoadForUpdateAsync(dbContext, @event.ProductId, cancellationToken)
            .BindAsync(async stockLevel =>
            {
                // Under the row lock this check is race-free: a concurrent consumer for the same reservation waits at the
                // lock until the first commits, and its query then sees the committed deduction.
                var alreadyDeducted = await StockLevelRowLock.HasDeductionAsync(dbContext, @event.ReservationId, cancellationToken);
                return stockLevel
                    .DeductForReservation(@event.ReservationId, @event.Quantity, alreadyDeducted)
                    .Map(() => stockLevel);
            });

        var deducted = outcome.Match(
            onSuccess: _ => true,
            onFailure: error =>
            {
                LogNotDeducted(logger, @event.ReservationId, @event.ProductId, error.Key);
                return false;
            });

        // Commits the deduction (or nothing) together with the inbox row the base class tracked before ProcessAsync.
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        if (deducted)
        {
            InventoryTelemetry.StockMovementsRecorded.Add(
                1, new KeyValuePair<string, object?>("type", nameof(StockMovementType.ReservationCommitted)));
        }
    }

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Committed reservation {ReservationId} of product {ProductId} was not deducted from the stock on hand: {Reason}.")]
    private static partial void LogNotDeducted(ILogger logger, DefaultIdType reservationId, DefaultIdType productId, string reason);
}
