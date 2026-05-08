namespace Common.Domain.Events;

public abstract record DomainEvent : IEvent
{
    public DefaultIdType Id { get; init; } = DefaultIdType.CreateVersion7();
    public long Version { get; set; }
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
}
