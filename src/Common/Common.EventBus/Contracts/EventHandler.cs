using Common.Core.Contracts;
using MassTransit;

namespace Common.EventBus.Contracts;

public abstract partial class EventHandlerBase<TEvent> : IEventHandler<TEvent> where TEvent : class, IEvent
{
    public Task Consume(ConsumeContext<TEvent> context) => HandleAsync(context.Message, context.CancellationToken);

    protected abstract Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}
