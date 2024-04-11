using Common.Core.Interfaces;

namespace Common.Core.Contracts;

public abstract record DomainEvent : IEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
}
