using Common.Domain.Events;

namespace Common.Application.EventBus;

public interface IDomainEventHandler<in TEvent> where TEvent : DomainEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}
