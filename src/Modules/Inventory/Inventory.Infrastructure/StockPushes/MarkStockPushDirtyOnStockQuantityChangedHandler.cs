using Common.Application.EventBus;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels.DomainEvents.v1;

namespace Inventory.Infrastructure.StockPushes;

/// <summary>
///     A changed quantity has to reach the warehouse of the product's store: the stock level is marked dirty and one
///     <see cref="StockPushDueIntegrationEvent" /> per connection is collected (the events of one save coalesce into a
///     single message). A store without an active connection has nobody to push to, so nothing is marked. Runs inside the
///     transaction of the save that changed the quantity, so a push can never read the old quantity under a mark that is
///     already gone. The connection lookup and the mark are one statement (<see cref="StockPushMarks.MarkForActiveConnectionAsync" />),
///     so a save that changes hundreds of quantities costs one statement per quantity.
/// </summary>
public sealed class MarkStockPushDirtyOnStockQuantityChangedHandler(
    IInventoryDbContext dbContext,
    IIntegrationEventOutbox outbox,
    TimeProvider timeProvider)
    : DomainEventHandlerBase<V1StockQuantityChangedDomainEvent>
{
    public override async Task HandleAsync(V1StockQuantityChangedDomainEvent @event, CancellationToken cancellationToken)
    {
        // One statement finds the product's active connection and marks the stock level for it; a product whose store has no
        // active connection comes back empty, which here just means there is nothing to mark.
        var connections = await StockPushMarks.MarkForActiveConnectionAsync(
            dbContext, @event.StockLevelId, @event.ProductId, timeProvider.GetUtcNow(), cancellationToken);

        foreach (var connectionId in connections)
        {
            outbox.Collect(new StockPushDueIntegrationEvent(connectionId));
        }
    }
}
