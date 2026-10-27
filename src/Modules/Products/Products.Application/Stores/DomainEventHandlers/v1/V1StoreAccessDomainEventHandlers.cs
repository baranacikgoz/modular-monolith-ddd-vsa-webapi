using Common.Application.EventBus;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Products.Application.Persistence;
using Products.Domain.Stores;
using Products.Domain.Stores.DomainEvents.v1;

namespace Products.Application.Stores.DomainEventHandlers.v1;

/// <summary>
///     Queues <see cref="StoreAccessChangedIntegrationEvent" /> from every store event that opens or closes the store for its
///     owner, so another module can decide store access from its own copy. <c>IsActive</c> follows the event, not a re-read
///     of the status: a store is active from creation, approval and reinstatement, and not from suspension or deactivation.
///     <c>Version</c> is the domain event's own, never the entity's: a save that raises several events must not stamp the
///     first one with a later version. The submission of an application queues nothing, because a pending store is not
///     active and its approval carries the owner.
/// </summary>
public static class V1StoreAccessDomainEventHandlers
{
    public class StoreCreatedAccessPublishingHandler(IIntegrationEventOutbox outbox)
        : DomainEventHandlerBase<V1StoreCreatedDomainEvent>
    {
        public override Task HandleAsync(V1StoreCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new StoreAccessChangedIntegrationEvent(@event.StoreId.Value, @event.OwnerId, true, @event.Version));
            return Task.CompletedTask;
        }
    }

    public class StoreApprovedAccessPublishingHandler(IIntegrationEventOutbox outbox)
        : DomainEventHandlerBase<V1StoreApprovedDomainEvent>
    {
        public override Task HandleAsync(V1StoreApprovedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new StoreAccessChangedIntegrationEvent(@event.StoreId.Value, @event.OwnerId, true, @event.Version));
            return Task.CompletedTask;
        }
    }

    public class StoreSuspendedAccessPublishingHandler(IIntegrationEventOutbox outbox, IProductsDbContext dbContext)
        : DomainEventHandlerBase<V1StoreSuspendedDomainEvent>
    {
        public override Task HandleAsync(V1StoreSuspendedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new StoreAccessChangedIntegrationEvent(
                @event.StoreId.Value, OwnerOf(dbContext, @event.StoreId), false, @event.Version));
            return Task.CompletedTask;
        }
    }

    public class StoreReinstatedAccessPublishingHandler(IIntegrationEventOutbox outbox, IProductsDbContext dbContext)
        : DomainEventHandlerBase<V1StoreReinstatedDomainEvent>
    {
        public override Task HandleAsync(V1StoreReinstatedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new StoreAccessChangedIntegrationEvent(
                @event.StoreId.Value, OwnerOf(dbContext, @event.StoreId), true, @event.Version));
            return Task.CompletedTask;
        }
    }

    public class StoreDeactivatedAccessPublishingHandler(IIntegrationEventOutbox outbox, IProductsDbContext dbContext)
        : DomainEventHandlerBase<V1StoreDeactivatedDomainEvent>
    {
        public override Task HandleAsync(V1StoreDeactivatedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new StoreAccessChangedIntegrationEvent(
                @event.StoreId.Value, OwnerOf(dbContext, @event.StoreId), false, @event.Version));
            return Task.CompletedTask;
        }
    }

    public class StoreDeactivatedWithStockOnHandAccessPublishingHandler(IIntegrationEventOutbox outbox, IProductsDbContext dbContext)
        : DomainEventHandlerBase<V1StoreDeactivatedWithStockOnHandDomainEvent>
    {
        public override Task HandleAsync(V1StoreDeactivatedWithStockOnHandDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new StoreAccessChangedIntegrationEvent(
                @event.StoreId.Value, OwnerOf(dbContext, @event.StoreId), false, @event.Version));
            return Task.CompletedTask;
        }
    }

    /// <summary>
    ///     These events carry only the store id, and a shipped domain event never changes. Domain events are dispatched from
    ///     <c>SaveChanges</c> and only for aggregates the change tracker holds, so the store that raised the event is in it.
    /// </summary>
    private static ApplicationUserId OwnerOf(IProductsDbContext dbContext, StoreId storeId)
    {
        return dbContext.ChangeTracker.Entries<Store>().Single(e => e.Entity.Id == storeId).Entity.OwnerId;
    }
}
