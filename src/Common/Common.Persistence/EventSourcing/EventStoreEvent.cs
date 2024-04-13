using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Contracts;

namespace Common.Persistence.EventSourcing;
public class EventStoreEvent
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
    public DateTime CreatedOn { get; } = DateTime.UtcNow;

    public static EventStoreEvent Create(Guid aggregateId, long version, DomainEvent @event)
        => new(aggregateId, version, @event);

#pragma warning disable CS8618
    private EventStoreEvent() { } // ORMs need parameterless ctor
#pragma warning restore CS8618
}
