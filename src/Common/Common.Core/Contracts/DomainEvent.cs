namespace Common.Core.Contracts;

public abstract record DomainEvent : IEvent
{
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
}
