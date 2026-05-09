using Common.Application.EventBus;
using Common.Application.Persistence.Outbox;
using Common.Infrastructure.Persistence.Outbox;
using Common.IntegrationEvents;

namespace Common.Infrastructure.EventBus;

public class IntegrationEventOutbox(IOutboxDbContext dbContext, TimeProvider timeProvider) : IIntegrationEventOutbox
{
    public void Write<TEvent>(TEvent @event) where TEvent : IntegrationEvent
    {
        var message = IntegrationEventOutboxMessage.Create(timeProvider.GetUtcNow(), @event);
        dbContext.IntegrationEventOutboxMessages.Add(message);
    }
}
