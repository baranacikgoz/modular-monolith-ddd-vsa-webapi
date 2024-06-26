using System.ComponentModel.DataAnnotations;
using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace Common.Domain.Aggregates;

public abstract class AggregateRoot<TId>(TId id) : AuditableEntity<TId>(id), IAggregateRoot
    where TId : IStronglyTypedId
{
    IStronglyTypedId IAggregateRoot.Id => Id;

    private readonly List<DomainEvent> _events = [];
    public IReadOnlyCollection<DomainEvent> Events => _events.AsReadOnly();
    public void LoadFromHistory(IEnumerable<DomainEvent> events)
    {
        foreach (var @event in events)
        {
            RaiseEvent(@event);
        }
    }

    [ConcurrencyCheck]
    public new long Version { get; set; }
    protected void AddEvent(DomainEvent @event) => _events.Add(@event);
    public void ClearEvents() => _events.Clear();
    protected abstract void ApplyEvent(DomainEvent @event);

#pragma warning disable CA1030
    protected void RaiseEvent(DomainEvent @event)
    {
        Version++;
        @event.Version = Version;
        ApplyEvent(@event);
        AddEvent(@event);
    }
#pragma warning restore CA1030
}
