using Common.Application.EventBus;
using Common.IntegrationEvents;
using Products.Domain.Stores.DomainEvents.v1;

namespace Products.Application.Stores.DomainEventHandlers.v1;

public static class V1StoreCreatedDomainEventHandlers
{
    public class StoreCreatedIntegrationEventPublishingHandler(IIntegrationEventOutbox outbox)
        : DomainEventHandlerBase<V1StoreCreatedDomainEvent>
    {
        public override Task HandleAsync(V1StoreCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Collect(new StoreCreatedIntegrationEvent(@event.StoreId.Value, @event.OwnerId));
            return Task.CompletedTask;
        }
    }

    public class SimulateSomeBusinessHandler : DomainEventHandlerBase<V1StoreCreatedDomainEvent>
    {
        public override Task HandleAsync(V1StoreCreatedDomainEvent @event, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
