using Common.Application.Options;
using Common.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Common.Application.EventBus;

public abstract class IntegrationEventHandlerBase<TEvent>(
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    ILogger logger
) : EventHandlerBase<TEvent>(cache, cachingOptions, logger), IIntegrationEventHandler<TEvent>, IIntegrationEventHandlerWrapper
    where TEvent : IntegrationEvent
{
    // Safe cast: DI lookup uses MakeGenericType(@event.GetType()), so @event is always TEvent here.
    Task IIntegrationEventHandlerWrapper.HandleAsync(IntegrationEvent @event, CancellationToken cancellationToken)
        => HandleAsync((TEvent)@event, cancellationToken);
}
