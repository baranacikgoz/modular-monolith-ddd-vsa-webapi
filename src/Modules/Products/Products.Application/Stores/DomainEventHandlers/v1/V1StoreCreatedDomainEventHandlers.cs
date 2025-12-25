using Common.Application.EventBus;
using Common.IntegrationEvents;
using MassTransit;
using Products.Domain.Stores.DomainEvents.v1;

namespace Products.Application.Stores.DomainEventHandlers.v1;

// This is a separation to ensure single responsibility between event handlers, even they are for the same event.
public static class V1StoreCreatedDomainEventHandlers
{
    public class StoreCreatedIntegrationEventPublishingHandler(IEventBus eventBus)
        : EventHandlerBase<V1StoreCreatedDomainEvent>
    {
        protected override async Task HandleAsync(ConsumeContext<V1StoreCreatedDomainEvent> context,
            V1StoreCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            var integrationEvent = new StoreCreatedIntegrationEvent(@event.StoreId.Value, @event.OwnerId);

            await eventBus.PublishAsync(integrationEvent, cancellationToken);
        }
    }

    // Simulate some business (e.g. update some count variable of an entity)
    public class SimulateSomeBusinessHandler : EventHandlerBase<V1StoreCreatedDomainEvent>
    {
        protected override Task HandleAsync(ConsumeContext<V1StoreCreatedDomainEvent> context,
            V1StoreCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
