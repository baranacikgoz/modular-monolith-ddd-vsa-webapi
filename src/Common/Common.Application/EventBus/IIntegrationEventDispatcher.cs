using Common.IntegrationEvents;

namespace Common.Application.EventBus;

public interface IIntegrationEventDispatcher
{
    Task DispatchAsync(IntegrationEvent @event, CancellationToken cancellationToken);
}
