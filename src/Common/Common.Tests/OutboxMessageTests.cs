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
        var now = DateTimeOffset.UtcNow;
        var message = OutboxMessage.Create(now, new TestableIntegrationEvent("x"));

        message.IncrementRetryCount(now, TimeSpan.FromSeconds(5));
        message.IncrementRetryCount(now, TimeSpan.FromSeconds(10));
        message.IncrementRetryCount(now, TimeSpan.FromSeconds(20));

        Assert.Equal(3, message.RetryCount);
    }

    [Fact]
    public void IncrementRetryCount_SetsNextRetryAt()
    {
        var now = DateTimeOffset.UtcNow;
        var backoff = TimeSpan.FromSeconds(30);
        var message = OutboxMessage.Create(now, new TestableIntegrationEvent("x"));

        message.IncrementRetryCount(now, backoff);

        Assert.NotNull(message.NextRetryAt);
        Assert.Equal(now + backoff, message.NextRetryAt);
    }

    [Fact]
    public void IncrementRetryCount_UpdatesNextRetryAtOnEachCall()
    {
        var now = DateTimeOffset.UtcNow;
        var message = OutboxMessage.Create(now, new TestableIntegrationEvent("x"));

        message.IncrementRetryCount(now, TimeSpan.FromSeconds(10));
        var firstNextRetryAt = message.NextRetryAt;

        var later = now.AddSeconds(15);
        message.IncrementRetryCount(later, TimeSpan.FromSeconds(20));

        Assert.NotEqual(firstNextRetryAt, message.NextRetryAt);
        Assert.Equal(later + TimeSpan.FromSeconds(20), message.NextRetryAt);
    }

    [Fact]
    public void Create_NewMessage_HasNullNextRetryAt()
    {
        var message = OutboxMessage.Create(DateTimeOffset.UtcNow, new TestableIntegrationEvent("x"));

        Assert.Null(message.NextRetryAt);
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

    [Fact]
    public void ReleaseClaim_Always_ClearsNextRetryAt()
    {
        var now = DateTimeOffset.UtcNow;
        var message = OutboxMessage.Create(now, new TestableIntegrationEvent("x"));
        message.IncrementRetryCount(now, TimeSpan.FromSeconds(30)); // sets NextRetryAt

        message.ReleaseClaim();

        Assert.Null(message.NextRetryAt);
    }
}
