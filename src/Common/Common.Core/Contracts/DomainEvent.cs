using Common.Core.Interfaces;

namespace Common.Core.Contracts;

public abstract record DomainEvent : IEvent
{
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
}
