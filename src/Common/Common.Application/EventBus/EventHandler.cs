using Common.Domain.Events;
using MassTransit;

namespace Common.Application.EventBus;

public abstract partial class EventHandlerBase<TEvent> : IEventHandler<TEvent> where TEvent : class, IEvent
{
    public Task Consume(ConsumeContext<TEvent> context) => HandleAsync(context, context.Message, context.CancellationToken);

    protected abstract Task HandleAsync(ConsumeContext<TEvent> context, TEvent @event, CancellationToken cancellationToken);
}
