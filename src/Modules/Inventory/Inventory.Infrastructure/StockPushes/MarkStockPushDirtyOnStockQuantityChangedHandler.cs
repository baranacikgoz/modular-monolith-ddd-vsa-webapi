using Common.Application.EventBus;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Application.WarehouseConnections;
using Inventory.Domain.StockLevels.DomainEvents.v1;
using Inventory.Domain.WarehouseConnections;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.StockPushes;

/// <summary>
///     A changed quantity has to reach the warehouse of the product's store: the stock level is marked dirty and one
///     <see cref="StockPushDueIntegrationEvent" /> per connection is collected (the events of one save coalesce into a
///     single message). A store without an active connection has nobody to push to, so nothing is marked. Runs inside the
///     transaction of the save that changed the quantity, so a push can never read the old quantity under a mark that is
///     already gone.
/// </summary>
public sealed class MarkStockPushDirtyOnStockQuantityChangedHandler(
    IInventoryDbContext dbContext,
    IIntegrationEventOutbox outbox,
    TimeProvider timeProvider)
    : DomainEventHandlerBase<V1StockQuantityChangedDomainEvent>
{
    public override async Task HandleAsync(V1StockQuantityChangedDomainEvent @event, CancellationToken cancellationToken)
    {
        // A product whose store has no active connection is a NotFound result, which here just means there is nothing to mark.
        await dbContext
            .ActiveConnectionOfProduct(@event.ProductId)
            .AsNoTracking()
            .TagWith(nameof(HandleAsync), @event.ProductId)
            .Select(c => new { c.Id, c.StoreId })
            .SingleAsResultAsync(nameof(WarehouseConnection), cancellationToken)
            .TapAsync(async connection =>
            {
                await StockPushMarks.MarkAsync(
                    dbContext, @event.StockLevelId, connection.StoreId, @event.ProductId, timeProvider.GetUtcNow(), cancellationToken);
                outbox.Collect(new StockPushDueIntegrationEvent(connection.Id.Value));
            });
    }
}
