using System.Text.Json.Serialization;
using Common.Domain.Events;

namespace IAM.Domain.Identity;

public sealed partial class ApplicationUser
{
    [JsonIgnore]
    private readonly List<DomainEvent> _events = [];
    [JsonIgnore]
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
