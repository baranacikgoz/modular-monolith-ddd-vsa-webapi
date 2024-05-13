using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace Common.Infrastructure.Persistence.Outbox;

public abstract class OutboxMessageBase(DomainEvent @event) : AuditableEntity
{
    public int Id { get; set; }
    public DomainEvent Event { get; } = @event;
    public int FailedCount { get; protected set; }
    public DateTime? LastFailedAt { get; protected set; }

    public void MarkAsFailed()
    {
        FailedCount++;
        LastFailedAt = DateTime.Now;
    }
}

public class OutboxMessage : OutboxMessageBase
{
    private OutboxMessage(DomainEvent @event)
        : base(@event)
    {
    }

    public bool IsProcessed { get; protected set; }
    public DateTime? ProcessedOn { get; protected set; }

    public static OutboxMessage Create(DomainEvent @event)
        => new(@event);

    public void MarkAsProcessed()
    {
        IsProcessed = true;
        ProcessedOn = DateTime.Now;
    }
}

public class DeadLetterMessage : OutboxMessageBase
{
    private DeadLetterMessage(DomainEvent @event, int failedCount, DateTime? lastFailedAt)
        : base(@event)
    {
        FailedCount = failedCount;
        LastFailedAt = lastFailedAt;
    }

    public static DeadLetterMessage CreateFrom(OutboxMessage outboxMessage)
        => new(outboxMessage.Event, outboxMessage.FailedCount, outboxMessage.LastFailedAt);
}
