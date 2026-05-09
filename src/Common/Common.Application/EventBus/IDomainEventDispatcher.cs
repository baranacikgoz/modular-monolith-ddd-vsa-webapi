using Common.Domain.Events;

namespace Common.Application.EventBus;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(DomainEvent @event, CancellationToken cancellationToken);
}
