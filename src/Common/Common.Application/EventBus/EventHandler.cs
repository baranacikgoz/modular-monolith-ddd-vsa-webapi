using Common.Application.Caching;
using Common.Domain.Events;
using MassTransit;

namespace Common.Application.EventBus;

public abstract class EventHandlerBase<TEvent>(ICacheService cache) : IEventHandler<TEvent> where TEvent : class, IEvent
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

        // GetOrCreateAsync is atomic within this process (HybridCache stampede protection):
        // only one concurrent consumer executes the factory for a given key.
        // If HandleAsync throws the entry is not persisted, so the message remains retry-eligible.
        await cache.GetOrCreateAsync(
            key,
            async ct =>
            {
                await HandleAsync(context, context.Message, ct);
                return true;
            },
            absoluteExpirationRelativeToNow: TimeSpan.FromDays(1),
            cancellationToken: context.CancellationToken);
    }

    protected abstract Task HandleAsync(ConsumeContext<TEvent> context, TEvent @event,
        CancellationToken cancellationToken);
}
