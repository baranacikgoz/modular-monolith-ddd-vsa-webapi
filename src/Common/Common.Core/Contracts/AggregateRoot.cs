using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Core.Contracts;

public abstract class AggregateRoot<T> : AuditableEntity<T>, IAggregateRoot
{
    protected AggregateRoot(T id)
        : base(id)
    {
    }

    private readonly Queue<IEvent> _events = [];

    [NotMapped]
    public bool HasAnyEvent => _events.Count > 0;

    public bool TryDequeueEvent(out IEvent? @event) => _events.TryDequeue(out @event);
    protected void EnqueueEvent(IEvent @event) => _events.Enqueue(@event);
    protected abstract void ApplyEvent(IEvent @event);

#pragma warning disable CA1030
    protected void RaiseEvent(IEvent @event)
    {
        ApplyEvent(@event);
        EnqueueEvent(@event);
    }
#pragma warning restore CA1030
}

public interface IAggregateRoot
{
    bool TryDequeueEvent(out IEvent? @event);
    bool HasAnyEvent { get; }
}
