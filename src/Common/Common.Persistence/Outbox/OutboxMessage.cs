using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Core.Contracts;
using Common.Core.Interfaces;

namespace Common.Persistence.Outbox;

public abstract class OutboxMessageBase(IEvent @event)
{
    public int Id { get; set; }
    public IEvent Event { get; } = @event;
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
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
    private OutboxMessage(IEvent @event)
        : base(@event)
    {
    }

    public bool IsProcessed { get; protected set; }
    public DateTime? ProcessedOn { get; protected set; }

    public static OutboxMessage Create(IEvent @event)
        => new(@event);

    public void MarkAsProcessed()
    {
        IsProcessed = true;
        ProcessedOn = DateTime.Now;
    }
}

public class DeadLetterMessage : OutboxMessageBase
{
    private DeadLetterMessage(IEvent @event, int failedCount, DateTime? lastFailedAt)
        : base(@event)
    {
        FailedCount = failedCount;
        LastFailedAt = lastFailedAt;
    }

    public static DeadLetterMessage CreateFrom(OutboxMessage outboxMessage)
        => new(outboxMessage.Event, outboxMessage.FailedCount, outboxMessage.LastFailedAt);
}
