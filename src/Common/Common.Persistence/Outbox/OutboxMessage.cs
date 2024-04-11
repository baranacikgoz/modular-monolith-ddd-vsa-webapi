using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Core.Contracts;
using Common.Core.Interfaces;

namespace Common.Persistence.Outbox;

public abstract class OutboxMessageBase(string type, string payload)
{
    public int Id { get; set; }
    public string Type { get; } = type;
    public string Payload { get; } = payload;
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
    private OutboxMessage(string type, string payload)
        : base(type, payload)
    {
    }

    public bool IsProcessed { get; protected set; }
    public DateTime? ProcessedOn { get; protected set; }

    public static OutboxMessage Create(string type, string payload)
        => new(type, payload);

    public void MarkAsProcessed()
    {
        IsProcessed = true;
        ProcessedOn = DateTime.Now;
    }
}

public class DeadLetterMessage : OutboxMessageBase
{
    private DeadLetterMessage(string type, string payload, int failedCount, DateTime? lastFailedAt)
        : base(type, payload)
    {
        FailedCount = failedCount;
        LastFailedAt = lastFailedAt;
    }

    public static DeadLetterMessage CreateFrom(OutboxMessage outboxMessage)
        => new(outboxMessage.Type, outboxMessage.Payload, outboxMessage.FailedCount, outboxMessage.LastFailedAt);
}
