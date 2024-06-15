using Common.Domain.EventBus;
using Common.IntegrationEvents;
using IAM.Domain.Identity.DomainEvents;

namespace IAM.Application.Identity.DomainEventHandlers;
public class UserRegisteredDomainEventHandler(
    IEventBus eventbus
    ) : EventHandlerBase<UserRegisteredDomainEvent>
{
    protected override async Task HandleAsync(UserRegisteredDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new UserRegisteredIntegrationEvent(@event.UserId, @event.Name, @event.PhoneNumber);

        await eventbus.PublishAsync(integrationEvent, cancellationToken);
    }
}
