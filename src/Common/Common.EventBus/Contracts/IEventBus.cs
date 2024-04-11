using Common.Core.Contracts;
using Common.Core.Interfaces;

namespace Common.EventBus.Contracts;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IEvent;
}
