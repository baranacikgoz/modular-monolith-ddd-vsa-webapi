using System.ComponentModel.DataAnnotations;
using Common.Domain.Events;

namespace Common.Application.Persistence.Outbox;

public class OutboxMessage
{
    private OutboxMessage(DateTimeOffset createdOn, DomainEvent @event)
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
    public DomainEvent Event { get; }
    public bool IsProcessed { get; protected set; }
    public DateTimeOffset? ProcessedOn { get; protected set; }

    [Timestamp] public uint Version { get; set; }

    public static OutboxMessage Create(DateTimeOffset createdOn, DomainEvent @event)
    {
        return new OutboxMessage(createdOn, @event);
    }

    public void MarkAsProcessed(DateTimeOffset processedOn)
    {
        IsProcessed = true;
        ProcessedOn = processedOn;
    }
}
