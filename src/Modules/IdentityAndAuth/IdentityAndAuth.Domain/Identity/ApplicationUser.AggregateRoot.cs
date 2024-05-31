using Common.Domain.Events;

namespace IdentityAndAuth.Domain.Identity;

public sealed partial class ApplicationUser
{
    private readonly List<DomainEvent> _events = [];
    public IReadOnlyCollection<DomainEvent> Events => _events.AsReadOnly();
    public void LoadFromHistory(IEnumerable<DomainEvent> events)
    {
        foreach (var @event in events)
        {
            RaiseEvent(@event);
        }
    }
    public void AddEvent(DomainEvent @event) => _events.Add(@event);
    public void ClearEvents() => _events.Clear();
    private void RaiseEvent(DomainEvent @event)
    {
        Version++;
        @event.Version = Version;
        ApplyEvent(@event);
        AddEvent(@event);
    }
}
