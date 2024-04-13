using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Contracts;

namespace Common.Persistence.EventSourcing;
public class EventStoreEvent : IAuditableEntity
{
    private EventStoreEvent(Guid aggregateId, long version, DomainEvent @event)
    {
        AggregateId = aggregateId;
        Event = @event;
        Version = version;
    }

    public Guid AggregateId { get; }
    public DomainEvent Event { get; }
    public long Version { get; }

    public static EventStoreEvent Create(Guid aggregateId, long version, DomainEvent @event)
        => new(aggregateId, version, @event);

    // Auditing Related Section
    public DateTime CreatedOn { get; set; }
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime? LastModifiedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public string LastModifiedIp { get; set; } = string.Empty;

#pragma warning disable CS8618
    private EventStoreEvent() { } // ORMs need parameterless ctor
#pragma warning restore CS8618
}
