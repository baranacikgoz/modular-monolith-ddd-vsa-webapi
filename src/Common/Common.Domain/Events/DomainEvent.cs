namespace Common.Domain.Events;

public abstract record DomainEvent : IEvent
{
    public long Version { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
}
