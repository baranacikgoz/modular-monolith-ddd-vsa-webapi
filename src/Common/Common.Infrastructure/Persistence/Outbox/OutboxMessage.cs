using Common.Domain.Entities;
using Common.Domain.Events;

namespace Common.Infrastructure.Persistence.Outbox;

public abstract class OutboxMessageBase(DomainEvent @event) : AuditableEntity
{
    public int Id { get; set; }
    public DomainEvent Event { get; } = @event;
    public int FailedCount { get; protected set; }
    public DateTimeOffset? LastFailedOn { get; protected set; }

    public void MarkAsFailed(DateTimeOffset failedOn)
    {
        FailedCount++;
        LastFailedOn = failedOn;
    }
}

public class OutboxMessage : OutboxMessageBase
{
    private OutboxMessage(DomainEvent @event)
        : base(@event)
    {
    }

    public bool IsProcessed { get; protected set; }
    public DateTimeOffset? ProcessedOn { get; protected set; }

    public static OutboxMessage Create(DomainEvent @event)
        => new(@event);

    public void MarkAsProcessed(DateTimeOffset processedOn)
    {
        IsProcessed = true;
        ProcessedOn = processedOn;
    }
}

public class DeadLetterMessage : OutboxMessageBase
{
    private DeadLetterMessage(DomainEvent @event, int failedCount, DateTimeOffset? lastFailedOn)
        : base(@event)
    {
        FailedCount = failedCount;
        LastFailedOn = lastFailedOn;
    }

    public static DeadLetterMessage CreateFrom(OutboxMessage outboxMessage)
        => new(outboxMessage.Event, outboxMessage.FailedCount, outboxMessage.LastFailedOn);
}
