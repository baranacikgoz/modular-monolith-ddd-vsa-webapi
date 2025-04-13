using System.ComponentModel.DataAnnotations;
using Common.Domain.Events;

namespace Common.Infrastructure.Persistence.Outbox;

public class OutboxMessage
{
    private OutboxMessage(DateTimeOffset createdOn, DomainEvent @event)
    {
        CreatedOn = createdOn;
        Event = @event;
    }

    public int Id { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DomainEvent Event { get; }
    public bool IsProcessed { get; protected set; }
    public DateTimeOffset? ProcessedOn { get; protected set; }

    [Timestamp]
    public uint Version { get; set; }

    public static OutboxMessage Create(DateTimeOffset createdOn, DomainEvent @event)
        => new(createdOn, @event);

    public void MarkAsProcessed(DateTimeOffset processedOn)
    {
        IsProcessed = true;
        ProcessedOn = processedOn;
    }

#pragma warning disable
    public OutboxMessage() // Required for deserialization
    {}
    #pragma warning restore
}
