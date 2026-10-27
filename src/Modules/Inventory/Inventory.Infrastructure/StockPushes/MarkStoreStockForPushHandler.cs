using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Infrastructure.StockPushes;

/// <summary>
///     The queue consumer behind <see cref="StockPushInitialMarkRequestedIntegrationEvent" />: marks every stock level of the
///     connection's store dirty, so the first push sends the warehouse the whole store. The store is walked in id order in
///     pages of <see cref="InventoryOptions.StockPushInitialMarkPageSize" />, each page one set-based upsert of its own (see
///     <see cref="StockPushMarks.MarkStorePageAsync" />), so a large store never holds one long transaction or one huge
///     statement. A stock level that already carries a mark gets a new version and keeps its first mark time. When the walk is
///     done one push is queued for the connection. A connection that is gone or deactivated by the time the request runs is
///     acknowledged without marking: the marks of a deactivated connection would have nobody to be pushed to. The work is
///     idempotent, so a redelivery after a crash simply walks the store again.
/// </summary>
public sealed partial class MarkStoreStockForPushHandler(
    IInventoryDbContext dbContext,
    IIntegrationEventOutbox outbox,
    TimeProvider timeProvider,
    IOptions<InventoryOptions> options,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox,
    ILogger<MarkStoreStockForPushHandler> logger
) : IntegrationEventHandlerBase<StockPushInitialMarkRequestedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override async Task ProcessAsync(StockPushInitialMarkRequestedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        var connectionId = new WarehouseConnectionId(@event.ConnectionId);

        var connection = await dbContext.WarehouseConnections
            .AsNoTracking()
            .TagWith(nameof(ProcessAsync), connectionId)
            .Where(c => c.Id == connectionId && c.Status == WarehouseConnectionStatus.Active)
            .Select(c => new { c.StoreId })
            .SingleAsResultAsync(nameof(WarehouseConnection), cancellationToken);

        if (connection.IsFailure)
        {
            LogNothingToMark(logger, @event.ConnectionId);
            return;
        }

        var marked = await MarkStoreAsync(connection.Value!.StoreId, cancellationToken);
        if (marked > 0)
        {
            outbox.Collect(new StockPushDueIntegrationEvent(@event.ConnectionId));
        }

        // Commits the queued push together with the inbox row the base class tracked before this method ran.
        await dbContext.SaveChangesAsync(cancellationToken);
        LogMarked(logger, @event.ConnectionId, marked);
    }

    /// <summary>Walks the store in id order until a page comes back short. Returns how many stock levels were marked.</summary>
    private async Task<int> MarkStoreAsync(DefaultIdType storeId, CancellationToken cancellationToken)
    {
        var pageSize = options.Value.StockPushInitialMarkPageSize;
        var now = timeProvider.GetUtcNow();
        var after = DefaultIdType.Empty;
        var marked = 0;

        while (true)
        {
            var page = await StockPushMarks.MarkStorePageAsync(dbContext, storeId, after, pageSize, now, cancellationToken);
            marked += page.Count;

            if (page.Count < pageSize)
            {
                return marked;
            }

            after = page[^1];
        }
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Initial stock push of connection {ConnectionId} skipped: the connection is gone or not active.")]
    private static partial void LogNothingToMark(ILogger logger, Guid connectionId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Initial stock push of connection {ConnectionId} marked {MarkedCount} stock levels.")]
    private static partial void LogMarked(ILogger logger, Guid connectionId, int markedCount);
}
