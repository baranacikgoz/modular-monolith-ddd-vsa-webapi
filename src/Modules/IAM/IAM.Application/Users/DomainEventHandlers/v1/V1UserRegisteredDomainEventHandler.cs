using Common.Application.EventBus;
using Common.Application.Options;
using Common.IntegrationEvents;
using IAM.Domain.Identity.DomainEvents.v1;
using MassTransit;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Application.Users.DomainEventHandlers.v1;

public class V1UserRegisteredDomainEventHandler(IEventBus eventbus, IFusionCache cache, IOptions<CachingOptions> cachingOptions)
    : EventHandlerBase<V1UserRegisteredDomainEvent>(cache, cachingOptions)
{
    protected override async Task HandleAsync(ConsumeContext<V1UserRegisteredDomainEvent> context,
        V1UserRegisteredDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new UserRegisteredIntegrationEvent(@event.UserId, @event.Name, @event.PhoneNumber);

        await eventbus.PublishAsync(integrationEvent, cancellationToken);
    }
}
