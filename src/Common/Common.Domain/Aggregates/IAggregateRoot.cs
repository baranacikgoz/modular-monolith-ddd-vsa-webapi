using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace Common.Domain.Aggregates;

public interface IAggregateRoot : IAuditableEntity
{
    IStronglyTypedId Id { get; }
    long Version { get; set; }
    IReadOnlyCollection<DomainEvent> Events { get; }
    void LoadFromHistory(IEnumerable<DomainEvent> events);
    void ClearEvents();
}
