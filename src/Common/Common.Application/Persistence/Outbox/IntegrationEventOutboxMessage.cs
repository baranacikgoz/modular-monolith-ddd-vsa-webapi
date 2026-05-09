using System.ComponentModel.DataAnnotations;
using Common.IntegrationEvents;

namespace Common.Application.Persistence.Outbox;

public class IntegrationEventOutboxMessage
{
    private IntegrationEventOutboxMessage(DateTimeOffset createdOn, IntegrationEvent @event)
    {
        CreatedOn = createdOn;
        Event = @event;
    }

#pragma warning disable
    public IntegrationEventOutboxMessage() // Required for deserialization
    {
    }
#pragma warning restore

    public int Id { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public IntegrationEvent Event { get; private set; }
    public bool IsProcessed { get; protected set; }
    public DateTimeOffset? ProcessedOn { get; protected set; }

    [Timestamp] public uint Version { get; set; }

    public static IntegrationEventOutboxMessage Create(DateTimeOffset createdOn, IntegrationEvent @event)
        => new(createdOn, @event);

    public void MarkAsProcessed(DateTimeOffset processedOn)
    {
        IsProcessed = true;
        ProcessedOn = processedOn;
    }
}
