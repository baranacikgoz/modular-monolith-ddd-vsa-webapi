using Common.Domain.Events;

namespace Common.Application.EventBus;

public interface IDomainEventHandlerWrapper
{
    Task HandleAsync(DomainEvent @event, CancellationToken cancellationToken);
}
