namespace Common.Core.Contracts;
public abstract record DomainEvent : IEvent
{
    public static readonly DateTime CreatedOn = DateTime.UtcNow;
}
