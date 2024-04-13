namespace Common.Core.Contracts;

public abstract record DomainEvent : IEvent
{
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
    public long Version { get; set; }
}
