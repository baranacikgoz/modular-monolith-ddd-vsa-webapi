using Common.Application.EventBus;
using Common.IntegrationEvents;
using IAM.Domain.Identity.DomainEvents.v1;
using MassTransit;

namespace IAM.Application.Users.DomainEventHandlers.v1;

public class V1UserRegisteredDomainEventHandler(IEventBus eventbus) : EventHandlerBase<V1UserRegisteredDomainEvent>
{
    protected override async Task HandleAsync(ConsumeContext<V1UserRegisteredDomainEvent> context,
        V1UserRegisteredDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new UserRegisteredIntegrationEvent(@event.UserId, @event.Name, @event.PhoneNumber);

        await eventbus.PublishAsync(integrationEvent, cancellationToken);
    }
}
