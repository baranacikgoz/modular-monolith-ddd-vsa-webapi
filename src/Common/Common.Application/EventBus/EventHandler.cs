using Common.Domain.Events;
using MassTransit;

namespace Common.Application.EventBus;

public abstract class EventHandlerBase<TEvent> : IEventHandler<TEvent> where TEvent : class, IEvent
{
    public Task Consume(ConsumeContext<TEvent> context)
    {
        return HandleAsync(context, context.Message, context.CancellationToken);
    }

    protected abstract Task HandleAsync(ConsumeContext<TEvent> context, TEvent @event,
        CancellationToken cancellationToken);
}
