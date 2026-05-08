using Common.Application.Options;
using Common.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Common.Application.EventBus;

public abstract class EventHandlerBase<TEvent>(IFusionCache cache, IOptions<CachingOptions> cachingOptions)
    : IEventHandler<TEvent> where TEvent : class, IEvent
{
    public async Task Consume(ConsumeContext<TEvent> context)
    {
        var messageId = context.MessageId?.ToString();
        if (messageId is null)
        {
            await HandleAsync(context, context.Message, context.CancellationToken);
            return;
        }

        var key = $"processed_msg:{messageId}";

        // FusionCache with Redis backplane: factory executes at most once across all replicas for a given key.
        // If HandleAsync throws the entry is not persisted, so the message remains retry-eligible.
        await cache.GetOrSetAsync<bool>(
            key,
            async (_, ct) =>
            {
                await HandleAsync(context, context.Message, ct);
                return true;
            },
            options: new FusionCacheEntryOptions { Duration = cachingOptions.Value.IdempotencyKeyDuration },
            token: context.CancellationToken);
    }

    protected abstract Task HandleAsync(ConsumeContext<TEvent> context, TEvent @event,
        CancellationToken cancellationToken);
}
