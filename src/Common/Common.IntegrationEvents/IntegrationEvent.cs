using Common.Domain.Events;

namespace Common.IntegrationEvents;

public abstract record IntegrationEvent : IEvent
{
    public DefaultIdType Id { get; init; } = DefaultIdType.CreateVersion7();
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
}
