using Common.IntegrationEvents;

namespace Common.Application.EventBus;

public interface IIntegrationEventHandlerWrapper
{
    Task HandleAsync(IntegrationEvent @event, CancellationToken cancellationToken);
}
