using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Infrastructure.StockReservations;

/// <summary>
///     The queue consumer behind <see cref="StockReleaseReconcileRequestedIntegrationEvent" />: asks the warehouse what became
///     of a release whose outcome was never recorded (<see cref="StockReleaseReconciler" />). Nothing an expected failure can do
///     is thrown: a warehouse that does not answer, a store without a connection, a reservation that has nothing to reconcile
///     any more are logged and acknowledged, because the sweep that queued the message queues the next one while the marker
///     stands, and a failing message would only be retried against the same warehouse. Only a fault of this system is an exception.
/// </summary>
public sealed partial class ReconcileStockReleaseHandler(
    StockReleaseReconciler reconciler,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox,
    ILogger<ReconcileStockReleaseHandler> logger
) : IntegrationEventHandlerBase<StockReleaseReconcileRequestedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override async Task ProcessAsync(StockReleaseReconcileRequestedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        var outcome = await reconciler.ReconcileAsync(new StockReservationId(@event.ReservationId), cancellationToken);

        if (outcome.IsFailure)
        {
            LogNotReconciled(logger, @event.ReservationId, outcome.Error!.Key);
            return;
        }

        LogReconciled(logger, @event.ReservationId, outcome.Value);
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Release of reservation {ReservationId} reconciled: {Outcome}.")]
    private static partial void LogReconciled(ILogger logger, DefaultIdType reservationId, ReleaseReconcileOutcome outcome);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Release of reservation {ReservationId} was not reconciled ({Reason}); its marker stays for the next sweep.")]
    private static partial void LogNotReconciled(ILogger logger, DefaultIdType reservationId, string reason);
}
