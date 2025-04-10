namespace Common.Domain.Events;

public abstract record DomainEvent : IEvent
{
    public DateTimeOffset CreatedOn { get; set; }
    public long Version { get; set; }
}
