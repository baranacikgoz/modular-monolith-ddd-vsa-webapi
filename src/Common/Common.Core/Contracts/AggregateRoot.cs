namespace Common.Core.Contracts;

public abstract class AggregateRoot<T> : AuditableEntity<T>, IAggregateRoot
{
    protected AggregateRoot(T id)
        : base(id)
    {
    }
    private readonly List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}

public interface IAggregateRoot
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    void AddDomainEvent(DomainEvent domainEvent);
    void ClearDomainEvents();
}
