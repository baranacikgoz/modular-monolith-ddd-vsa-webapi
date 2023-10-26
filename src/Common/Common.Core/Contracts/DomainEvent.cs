namespace Common.Core.Contracts;
public abstract record DomainEvent
{
    public static readonly DateTime CreatedOn = DateTime.UtcNow;
}
