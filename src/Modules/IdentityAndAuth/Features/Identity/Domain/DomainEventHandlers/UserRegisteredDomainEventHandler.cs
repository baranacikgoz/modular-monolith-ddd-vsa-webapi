using Common.EventBus.Contracts;
using Common.IntegrationEvents;
using IdentityAndAuth.Features.Identity.Domain.DomainEvents;

namespace IdentityAndAuth.Features.Identity.Domain.DomainEventHandlers;
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
