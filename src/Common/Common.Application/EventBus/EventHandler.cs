using Common.Application.Caching;
using Common.Domain.Events;
using MassTransit;

namespace Common.Application.EventBus;

public abstract class EventHandlerBase<TEvent>(ICacheService cache) : IEventHandler<TEvent> where TEvent : class, IEvent
{
    public async Task Consume(ConsumeContext<TEvent> context)
    {
        var messageId = context.MessageId?.ToString();
        if (messageId is not null)
        {
            var key = $"processed_msg:{messageId}";
            var alreadyProcessed = await cache.GetAsync<bool?>(key, context.CancellationToken);
            if (alreadyProcessed == true) return;

            await HandleAsync(context, context.Message, context.CancellationToken);

            await cache.SetAsync(key, true,
                absoluteExpirationRelativeToNow: TimeSpan.FromDays(1),
                cancellationToken: context.CancellationToken);
        }
        else
        {
            await HandleAsync(context, context.Message, context.CancellationToken);
        }
    }

    protected abstract Task HandleAsync(ConsumeContext<TEvent> context, TEvent @event,
        CancellationToken cancellationToken);
}
