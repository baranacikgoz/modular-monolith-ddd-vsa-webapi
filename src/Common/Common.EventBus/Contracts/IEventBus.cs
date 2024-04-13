using Common.Core.Contracts;

namespace Common.EventBus.Contracts;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IEvent;
}
