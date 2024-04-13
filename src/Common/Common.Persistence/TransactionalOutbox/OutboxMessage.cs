using System.ComponentModel.DataAnnotations;
using Common.Core.Contracts;

namespace Common.Persistence.TransactionalOutbox;

public abstract class OutboxMessageBase(DomainEvent @event) : IAuditableEntity
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

    // Auditing Related Section
    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime? LastModifiedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public string LastModifiedIp { get; set; } = string.Empty;
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
