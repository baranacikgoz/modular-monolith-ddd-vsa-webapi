using Common.Domain.Events;

namespace Common.Infrastructure.Persistence.Outbox;

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
