using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace Common.Domain.Aggregates;

public abstract class AggregateRoot<TId>(TId id) : AuditableEntity<TId>(id), IAggregateRoot
    where TId : IStronglyTypedId
{
    [JsonIgnore] private readonly List<DomainEvent> _events = [];

    IStronglyTypedId IAggregateRoot.Id => Id;

    [JsonIgnore] public IReadOnlyCollection<DomainEvent> Events => _events.AsReadOnly();

    [ConcurrencyCheck] public new long Version { get; set; }

    public void ClearEvents()
    {
        _events.Clear();
    }

    protected void AddEvent(DomainEvent @event)
    {
        _events.Add(@event);
    }

#pragma warning disable CA1030
    protected void RaiseEvent(DomainEvent @event)
    {
        Version++;
        @event.Version = Version;
        AddEvent(@event);
    }
#pragma warning restore CA1030
}
