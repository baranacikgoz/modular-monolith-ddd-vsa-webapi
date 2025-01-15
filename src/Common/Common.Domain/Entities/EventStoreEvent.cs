using Common.Domain.Events;

namespace Common.Domain.Entities;

public class EventStoreEvent : AuditableEntity
{
    private EventStoreEvent(string aggregateType, DefaultIdType aggregateId, long version, DomainEvent @event)
    {
        AggregateType = aggregateType;
        AggregateId = aggregateId;
        Event = @event;
        Version = version;
        EventType = @event.GetType().Name ?? throw new InvalidOperationException("Type Name can't be null.");

        @event.CreatedOn = CreatedOn;
    }

    public string AggregateType { get; }
    public DefaultIdType AggregateId { get; }
    public string EventType { get; }
    public DomainEvent Event { get; }
    public new long Version { get; }

    public static EventStoreEvent Create(string aggregateType, DefaultIdType aggregateId, long version, DomainEvent @event)
        => new(aggregateType, aggregateId, version, @event);

#pragma warning disable CS8618
    public EventStoreEvent() { } // ORMs need parameterless ctor
#pragma warning restore CS8618
}
