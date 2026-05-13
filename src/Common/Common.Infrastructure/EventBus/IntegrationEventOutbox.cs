using Common.Application.EventBus;
using Common.IntegrationEvents;

namespace Common.Infrastructure.EventBus;

public sealed class IntegrationEventOutbox : IIntegrationEventOutbox
{
    private readonly List<IntegrationEvent> _events = [];
    private readonly Lock _lock = new();

    public void Collect<TEvent>(TEvent @event) where TEvent : IntegrationEvent
    {
        lock (_lock)
        {
            _events.Add(@event);
        }
    }

    internal IReadOnlyList<IntegrationEvent> Drain()
    {
        lock (_lock)
        {
            var snapshot = _events.ToList();
            _events.Clear();
            return snapshot;
        }
    }
}
