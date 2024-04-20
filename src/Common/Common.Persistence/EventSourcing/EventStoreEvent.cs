using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Contracts;
using Common.Core.Contracts.Identity;

namespace Common.Persistence.EventSourcing;
public class EventStoreEvent : IAuditableEntity
{
    private EventStoreEvent(Guid aggregateId, long version, DomainEvent @event)
    {
        AggregateId = aggregateId;
        Event = @event;
        Version = version;
    }

    public Guid AggregateId { get; init; }
    public DomainEvent Event { get; }
    public long Version { get; init; }

    public static EventStoreEvent Create(Guid aggregateId, long version, DomainEvent @event)
        => new(aggregateId, version, @event);

    // Auditing Related Section
    public DateTime CreatedOn { get; set; }
    public ApplicationUserId CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public ApplicationUserId? LastModifiedBy { get; set; }
    public string LastModifiedIp { get; set; } = string.Empty;

#pragma warning disable CS8618
    public EventStoreEvent() { } // ORMs need parameterless ctor
#pragma warning restore CS8618
}
