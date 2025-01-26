using Common.Application.EventBus;
using Common.IntegrationEvents;
using MassTransit;
using Products.Domain.Stores.DomainEvents.v1;

namespace Products.Application.Stores.DomainEventHandlers.v1;

public class V1StoreCreatedDomainEventHandler(IEventBus eventBus) : EventHandlerBase<V1StoreCreatedDomainEvent>
{
    protected override async Task HandleAsync(ConsumeContext<V1StoreCreatedDomainEvent> context, V1StoreCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new StoreCreatedIntegrationEvent(@event.StoreId.Value, @event.OwnerId);

        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
