using Common.Application.EventBus;
using Common.Application.Options;
using Common.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.Domain.Stores.DomainEvents.v1;
using ZiggyCreatures.Caching.Fusion;

namespace Products.Application.Stores.DomainEventHandlers.v1;

// This is a separation to ensure single responsibility between event handlers, even they are for the same event.
public static class V1StoreCreatedDomainEventHandlers
{
    public class StoreCreatedIntegrationEventPublishingHandler(
        IIntegrationEventOutbox outbox,
        IFusionCache cache,
        IOptions<CachingOptions> cachingOptions,
        ILogger<StoreCreatedIntegrationEventPublishingHandler> logger)
        : EventHandlerBase<V1StoreCreatedDomainEvent>(cache, cachingOptions, logger)
    {
        protected override Task ProcessAsync(V1StoreCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            outbox.Write(new StoreCreatedIntegrationEvent(@event.StoreId.Value, @event.OwnerId));
            return Task.CompletedTask;
        }
    }

    // Simulate some business (e.g. update some count variable of an entity)
    public class SimulateSomeBusinessHandler(
        IFusionCache cache,
        IOptions<CachingOptions> cachingOptions,
        ILogger<SimulateSomeBusinessHandler> logger)
        : EventHandlerBase<V1StoreCreatedDomainEvent>(cache, cachingOptions, logger)
    {
        protected override Task ProcessAsync(V1StoreCreatedDomainEvent @event, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
