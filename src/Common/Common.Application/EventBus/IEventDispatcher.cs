using Common.Domain.Events;

namespace Common.Application.EventBus;

public interface IEventDispatcher
{
    Task DispatchAsync(IEvent @event, CancellationToken cancellationToken);
}
