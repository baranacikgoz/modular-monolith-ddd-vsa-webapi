using Common.Application.Persistence.Outbox;
using Common.IntegrationEvents;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1515, CA1707

public sealed record TestableIntegrationEvent(string Data) : IntegrationEvent;

public sealed class OutboxMessageTests
{
    [Fact]
    public void Create_IntegrationEvent_CreatesMessage()
    {
        var @event = new TestableIntegrationEvent("test");
        var now = DateTimeOffset.UtcNow;

        var message = OutboxMessage.Create(now, @event);

        Assert.Equal(@event, message.Event);
        Assert.Equal(now, message.CreatedOn);
        Assert.False(message.IsProcessed);
        Assert.Equal(0, message.RetryCount);
        Assert.Null(message.FailedOn);
    }

    [Fact]
    public void IncrementRetryCount_IncrementsCounter()
    {
        var message = OutboxMessage.Create(DateTimeOffset.UtcNow, new TestableIntegrationEvent("x"));

        message.IncrementRetryCount();
        message.IncrementRetryCount();
        message.IncrementRetryCount();

        Assert.Equal(3, message.RetryCount);
    }

    [Fact]
    public void MarkAsFailed_SetsFailedOn()
    {
        var message = OutboxMessage.Create(DateTimeOffset.UtcNow, new TestableIntegrationEvent("x"));
        var failedAt = DateTimeOffset.UtcNow;

        message.MarkAsFailed(failedAt);

        Assert.Equal(failedAt, message.FailedOn);
        Assert.False(message.IsProcessed);
    }

    [Fact]
    public void MarkAsProcessed_SetsIsProcessedAndProcessedOn()
    {
        var message = OutboxMessage.Create(DateTimeOffset.UtcNow, new TestableIntegrationEvent("x"));
        var processedAt = DateTimeOffset.UtcNow;

        message.MarkAsProcessed(processedAt);

        Assert.True(message.IsProcessed);
        Assert.Equal(processedAt, message.ProcessedOn);
    }
}
