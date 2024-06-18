using Common.Domain.Entities;
using Common.Domain.Events;

namespace Common.Infrastructure.Persistence.Outbox;

public abstract class OutboxMessageBase(DomainEvent @event) : AuditableEntity
{
    public int Id { get; set; }
    public DomainEvent Event { get; } = @event;
    public int FailedCount { get; protected set; }
    public DateTimeOffset? LastFailedOn { get; protected set; }

    public void MarkAsFailed(DateTimeOffset failedOn)
    {
        FailedCount++;
        LastFailedOn = failedOn;
    }
}
