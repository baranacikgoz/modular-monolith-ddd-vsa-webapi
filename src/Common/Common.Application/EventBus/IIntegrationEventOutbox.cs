using Common.IntegrationEvents;

namespace Common.Application.EventBus;

public interface IIntegrationEventOutbox
{
    void Collect<TEvent>(TEvent @event) where TEvent : IntegrationEvent;
}
