namespace Common.Domain.Events;

public abstract record DomainEvent : IEvent
{
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
    public long Version { get; set; }
}
