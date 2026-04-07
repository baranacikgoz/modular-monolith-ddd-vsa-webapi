using Common.Domain.Events;

namespace Common.Domain.Entities;

public class AuditLogEntry : AuditableEntity
{
    private AuditLogEntry(string aggregateType, DefaultIdType aggregateId, long version, DomainEvent @event)
    {
        AggregateType = aggregateType;
        AggregateId = aggregateId;
        Event = @event;
        Version = version;
        EventType = @event.GetType().Name ?? throw new InvalidOperationException("Type Name can't be null.");
    }

#pragma warning disable CS8618
    public AuditLogEntry()
    {
    } // ORMs need parameterless ctor
#pragma warning restore CS8618

    public string AggregateType { get; }
    public DefaultIdType AggregateId { get; }
    public string EventType { get; }
    public DomainEvent Event { get; }
    public new long Version { get; }

    public static AuditLogEntry Create(string aggregateType, DefaultIdType aggregateId, long version,
        DomainEvent @event)
    {
        return new AuditLogEntry(aggregateType, aggregateId, version, @event);
    }
}
