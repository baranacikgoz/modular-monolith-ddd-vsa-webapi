using Common.Application.EventBus;
using Common.IntegrationEvents;

namespace Common.Infrastructure.EventBus;

public sealed class IntegrationEventOutbox : IIntegrationEventOutbox
{
    private readonly List<IntegrationEvent> _events = [];

    public void Collect<TEvent>(TEvent @event) where TEvent : IntegrationEvent
        => _events.Add(@event);

    internal IReadOnlyList<IntegrationEvent> Drain()
    {
        var snapshot = _events.ToList();
        _events.Clear();
        return snapshot;
    }
}
