using System.ComponentModel.DataAnnotations;
using Common.Domain.Events;
using Common.IntegrationEvents;

namespace Common.Application.Persistence.Outbox;

public class OutboxMessage : IOutboxMessage
{
    private OutboxMessage(DateTimeOffset createdOn, IEvent @event)
    {
        CreatedOn = createdOn;
        Event = @event;
        EventType = @event switch
        {
            DomainEvent => EventTypeDomain,
            IntegrationEvent => EventTypeIntegration,
            _ => throw new ArgumentException($"Unsupported event type: {@event.GetType()}")
        };
    }

#pragma warning disable
    public OutboxMessage() // Required for deserialization
    {
    }
#pragma warning restore

    public int Id { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public IEvent Event { get; private set; }
    public bool IsProcessed { get; protected set; }
    public DateTimeOffset? ProcessedOn { get; protected set; }
    public string EventType { get; private set; } = string.Empty;

    IEvent? IOutboxMessage.Event => Event;

    [Timestamp] public uint Version { get; set; }

    /// <summary>W3C TraceId captured from Activity.Current at write time.</summary>
    public string? TraceId { get; set; }
    /// <summary>W3C ParentSpanId captured from Activity.Current at write time.</summary>
    public string? ParentSpanId { get; set; }

    public const string EventTypeDomain = "DomainEvent";
    public const string EventTypeIntegration = "IntegrationEvent";

    public static OutboxMessage Create(DateTimeOffset createdOn, IEvent @event)
    {
        return new OutboxMessage(createdOn, @event);
    }

    public void MarkAsProcessed(DateTimeOffset processedOn)
    {
        IsProcessed = true;
        ProcessedOn = processedOn;
    }
}
