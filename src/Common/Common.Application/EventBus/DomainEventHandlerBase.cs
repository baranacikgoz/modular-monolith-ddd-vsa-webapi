using Common.Application.Options;
using Common.Domain.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Common.Application.EventBus;

public abstract class DomainEventHandlerBase<TEvent>(
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    ILogger logger
) : EventHandlerBase<TEvent>(cache, cachingOptions, logger), IDomainEventHandler<TEvent>, IDomainEventHandlerWrapper
    where TEvent : DomainEvent
{
    // Safe cast: DI lookup uses MakeGenericType(@event.GetType()), so @event is always TEvent here.
    Task IDomainEventHandlerWrapper.HandleAsync(DomainEvent @event, CancellationToken cancellationToken)
        => HandleAsync((TEvent)@event, cancellationToken);
}
