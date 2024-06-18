using Common.Domain.Events;

namespace Common.IntegrationEvents;
public abstract record IntegrationEvent : IEvent
{
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
}
