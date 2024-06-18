namespace Common.Domain.Events;

public abstract record DomainEvent : IEvent
{
    // It is better to re-assign CreatedOn it right before persisting to db, to set the exact same time with other events those are going to be persisted together.
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
    public long Version { get; set; }
}
