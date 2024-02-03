using Common.Core.Contracts;

namespace Common.EventBus;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : DomainEvent;
}
