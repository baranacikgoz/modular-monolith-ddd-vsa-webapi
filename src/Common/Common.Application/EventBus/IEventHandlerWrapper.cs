using Common.Domain.Events;

namespace Common.Application.EventBus;

public interface IEventHandlerWrapper
{
    Task HandleAsync(IEvent @event, CancellationToken cancellationToken);
}
