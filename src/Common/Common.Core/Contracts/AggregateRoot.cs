using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Common.Core.Contracts;

namespace Common.Core.Contracts;

public abstract class AggregateRoot<TId>(TId id) : IAggregateRoot where TId : IStronglyTypedId
{
    public TId Id { get; protected set; } = id;
    IStronglyTypedId IAggregateRoot.Id => Id;

    private readonly List<DomainEvent> _events = [];
    public IReadOnlyCollection<DomainEvent> Events => _events.AsReadOnly();

    [ConcurrencyCheck]
    public long Version { get; set; }
    public DateTime CreatedOn { get; private set; }
    public Guid CreatedBy { get; private set; } = Guid.Empty;
    public DateTime? LastModifiedOn { get; private set; }
    public Guid? LastModifiedBy { get; private set; }
    public string LastModifiedIp { get; private set; } = string.Empty;
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

    public void ApplyCreatedAudit(Guid userId, string ipAddress, DateTime createdOn)
    {
        CreatedBy = userId;
        CreatedOn = createdOn;
        ApplyUpdatedAudit(userId, ipAddress, createdOn);
    }
    public void ApplyUpdatedAudit(Guid userId, string ipAddress, DateTime updatedOn)
    {
        LastModifiedBy = userId;
        LastModifiedOn = updatedOn;
        LastModifiedIp = ipAddress;
    }
#pragma warning restore CA1030
}

public interface IAggregateRoot : IAuditableEntity
{
    IStronglyTypedId Id { get; }
    long Version { get; set; }
    IReadOnlyCollection<DomainEvent> Events { get; }
    void ClearEvents();
}
