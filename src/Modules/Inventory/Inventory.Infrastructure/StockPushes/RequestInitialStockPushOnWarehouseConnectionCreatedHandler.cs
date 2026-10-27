using Common.Application.EventBus;
using Common.IntegrationEvents;
using Inventory.Domain.WarehouseConnections.DomainEvents.v1;

namespace Inventory.Infrastructure.StockPushes;

/// <summary>
///     A new warehouse connection knows none of its store's stock. A store that connects again after a deactivation gets a new
///     connection (a deactivated one is final), so this one event covers both a first connection and a reactivation. The
///     request is queued with the connection's own save, so it exists exactly when the connection does; marking the store's
///     stock levels is the consumer's work (<see cref="MarkStoreStockForPushHandler" />), in pages, outside this transaction.
/// </summary>
public sealed class RequestInitialStockPushOnWarehouseConnectionCreatedHandler(IIntegrationEventOutbox outbox)
    : DomainEventHandlerBase<V1WarehouseConnectionCreatedDomainEvent>
{
    public override Task HandleAsync(V1WarehouseConnectionCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        outbox.Collect(new StockPushInitialMarkRequestedIntegrationEvent(@event.WarehouseConnectionId.Value));
        return Task.CompletedTask;
    }
}
