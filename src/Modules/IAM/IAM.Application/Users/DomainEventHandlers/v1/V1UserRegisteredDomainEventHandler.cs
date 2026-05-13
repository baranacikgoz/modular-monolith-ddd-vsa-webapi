using Common.Application.EventBus;
using Common.IntegrationEvents;
using IAM.Domain.Identity.DomainEvents.v1;

namespace IAM.Application.Users.DomainEventHandlers.v1;

public class V1UserRegisteredDomainEventHandler(IIntegrationEventOutbox outbox)
    : DomainEventHandlerBase<V1UserRegisteredDomainEvent>
{
    public override Task HandleAsync(V1UserRegisteredDomainEvent @event, CancellationToken cancellationToken)
    {
        outbox.Collect(new UserRegisteredIntegrationEvent(@event.UserId, @event.FullName, @event.PhoneNumber));
        return Task.CompletedTask;
    }
}
