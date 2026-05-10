using Common.Application.EventBus;
using Common.Application.Options;
using Common.IntegrationEvents;
using IAM.Domain.Identity.DomainEvents.v1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Application.Users.DomainEventHandlers.v1;

public class V1UserRegisteredDomainEventHandler(
    IIntegrationEventOutbox outbox,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    ILogger<V1UserRegisteredDomainEventHandler> logger)
    : EventHandlerBase<V1UserRegisteredDomainEvent>(cache, cachingOptions, logger)
{
    protected override Task ProcessAsync(V1UserRegisteredDomainEvent @event, CancellationToken cancellationToken)
    {
        outbox.Write(new UserRegisteredIntegrationEvent(@event.UserId, @event.Name, @event.PhoneNumber));
        return Task.CompletedTask;
    }
}
