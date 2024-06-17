using Common.Domain.Entities;
using Common.Domain.Events;

namespace Common.Infrastructure.Persistence.EventSourcing;
public class EventStoreEvent : AuditableEntity
{
    private EventStoreEvent(Guid aggregateId, long version, DomainEvent @event)
    {
        AggregateId = aggregateId;
        Event = @event;
        Version = version;
        EventType = @event.GetType().Name ?? throw new InvalidOperationException("Type Name can't be null.");
    }

    public Guid AggregateId { get; }
    public string EventType { get; }
    public DomainEvent Event { get; }
    public new long Version { get; }

    public static EventStoreEvent Create(Guid aggregateId, long version, DomainEvent @event)
        => new(aggregateId, version, @event);

#pragma warning disable CS8618
    public EventStoreEvent() { } // ORMs need parameterless ctor
#pragma warning restore CS8618
}
