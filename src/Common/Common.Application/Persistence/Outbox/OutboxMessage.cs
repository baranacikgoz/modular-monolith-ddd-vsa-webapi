using Common.Domain.Events;
using Common.IntegrationEvents;

namespace Common.Application.Persistence.Outbox;

public class OutboxMessage : IOutboxMessage
{
    private OutboxMessage(DateTimeOffset createdOn, IntegrationEvent @event)
    {
        CreatedOn = createdOn;
        Event = @event;
    }

#pragma warning disable
    public OutboxMessage() // Required for deserialization
    {
    }
#pragma warning restore

    public int Id { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public IntegrationEvent Event { get; private set; }
    public bool IsProcessed { get; protected set; }
    public DateTimeOffset? ProcessedOn { get; protected set; }
    public int RetryCount { get; private set; }
    public DateTimeOffset? FailedOn { get; private set; }

    IEvent? IOutboxMessage.Event => Event;

    // TraceId/ParentSpanId carry W3C context captured at write time for span correlation.
    public string? TraceId { get; set; }
    public string? ParentSpanId { get; set; }

    public static OutboxMessage Create(DateTimeOffset createdOn, IntegrationEvent @event)
        => new(createdOn, @event);

    public void MarkAsProcessed(DateTimeOffset processedOn)
    {
        IsProcessed = true;
        ProcessedOn = processedOn;
    }

    public void IncrementRetryCount() => RetryCount++;

    public void MarkAsFailed(DateTimeOffset failedOn) => FailedOn = failedOn;
}
