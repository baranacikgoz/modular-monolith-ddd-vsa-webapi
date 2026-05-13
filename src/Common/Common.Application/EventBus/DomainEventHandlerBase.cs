using Common.Domain.Events;

namespace Common.Application.EventBus;

public abstract class DomainEventHandlerBase<TEvent> : IEventHandler<TEvent>, IEventHandlerWrapper
    where TEvent : class, IEvent
{
    Task IEventHandlerWrapper.HandleAsync(IEvent @event, CancellationToken cancellationToken)
        => HandleAsync((TEvent)@event, cancellationToken);

    public abstract Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}
