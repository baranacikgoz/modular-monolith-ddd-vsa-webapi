using Common.Domain.Events;

namespace Common.IntegrationEvents;
public abstract record IntegrationEvent : IEvent
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
