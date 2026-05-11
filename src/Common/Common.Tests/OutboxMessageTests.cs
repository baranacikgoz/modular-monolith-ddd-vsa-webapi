using Common.Application.Persistence.Outbox;
using Common.Domain.Events;
using Common.IntegrationEvents;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1515, CA1707

public sealed record TestableDomainEventForOutbox(string Data) : DomainEvent;
public sealed record TestableIntegrationEventForOutbox(string Data) : IntegrationEvent;

public sealed class OutboxMessageTests
{
    [Fact]
    public void Create_DomainEvent_SetsEventTypeDomain()
    {
        var @event = new TestableDomainEventForOutbox("test");
        var message = OutboxMessage.Create(DateTimeOffset.UtcNow, @event);

        Assert.Equal(OutboxMessage.EventTypeDomain, message.EventType);
    }

    [Fact]
    public void Create_IntegrationEvent_SetsEventTypeIntegration()
    {
        var @event = new TestableIntegrationEventForOutbox("test");
        var message = OutboxMessage.Create(DateTimeOffset.UtcNow, @event);

        Assert.Equal(OutboxMessage.EventTypeIntegration, message.EventType);
    }
}
