using Common.IntegrationEvents;

namespace Common.Application.EventBus;

public interface IIntegrationEventOutbox
{
    void Write<TEvent>(TEvent @event) where TEvent : IntegrationEvent;
}
