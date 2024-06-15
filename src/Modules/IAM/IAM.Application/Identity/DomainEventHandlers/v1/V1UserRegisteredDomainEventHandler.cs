using Common.Domain.EventBus;
using Common.IntegrationEvents;
using IAM.Domain.Identity.DomainEvents.v1;

namespace IAM.Application.Identity.DomainEventHandlers.v1;
public class V1UserRegisteredDomainEventHandler(
    IEventBus eventbus
    ) : EventHandlerBase<V1UserRegisteredDomainEvent>
{
    protected override async Task HandleAsync(V1UserRegisteredDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new UserRegisteredIntegrationEvent(@event.UserId, @event.Name, @event.PhoneNumber);

        await eventbus.PublishAsync(integrationEvent, cancellationToken);
    }
}
