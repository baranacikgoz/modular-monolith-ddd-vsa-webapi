using Common.Application.EventBus;
using Common.Application.Options;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.StockPushes;

/// <summary>
///     The recurring job body under the event-driven push: a mark normally has its connection's push queued by the change
///     that made it, so a mark older than <see cref="InventoryOptions.StockPushDirtyMinAgeSeconds" /> means that push was lost
///     or stopped on a failure. For each connection with such a mark one <see cref="StockPushDueIntegrationEvent" /> is
///     published, <see cref="InventoryOptions.SweepPageSize" /> connections at a time. It never calls a warehouse and never
///     touches a mark: the queue consumers push, and a run that finds the lease held simply leaves it to the holder.
/// </summary>
public sealed partial class StockPushSweepService(
    IInventoryDbContext dbContext,
    IIntegrationEventOutbox outbox,
    TimeProvider timeProvider,
    IOptions<InventoryOptions> options,
    ILogger<StockPushSweepService> logger)
{
    public async Task SweepAsync(CancellationToken cancellationToken = default)
    {
        var cutoff = timeProvider.GetUtcNow().AddSeconds(-options.Value.StockPushDirtyMinAgeSeconds);
        var pageSize = options.Value.SweepPageSize;
        var published = 0;

        // Publishing changes no mark, so the set is stable while it is paged through.
        while (true)
        {
            var page = await dbContext.StockPushDirtyItems
                .AsNoTracking()
                .TagWith(nameof(SweepAsync), published)
                .Where(d => d.MarkedOn <= cutoff)
                .Join(
                    dbContext.WarehouseConnections.Where(c => c.Status == WarehouseConnectionStatus.Active),
                    d => d.StoreId,
                    c => c.StoreId,
                    (_, connection) => connection.Id)
                .Distinct()
                .OrderBy(id => id)
                .Skip(published)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            foreach (var connectionId in page)
            {
                outbox.Collect(new StockPushDueIntegrationEvent(connectionId.Value));
            }

            if (page.Count > 0)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            published += page.Count;
            if (page.Count < pageSize)
            {
                break;
            }
        }

        LogSweepComplete(logger, published);
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Stock push sweep queued a push for {ConnectionCount} connections with old stock marks.")]
    private static partial void LogSweepComplete(ILogger logger, int connectionCount);
}
