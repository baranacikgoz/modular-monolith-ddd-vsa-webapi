namespace Common.Domain.Events;

public abstract record DomainEvent : IEvent
{
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
    public long Version { get; set; }

    /// <summary>
    /// Indicates that if this event is created to undo another.
    /// </summary>
    public bool IsUndoerEvent { get; private set; }
    public void MarkAsUndoerEvent() => IsUndoerEvent = true;

    /// <summary>
    /// Indicates that if this event is undone by another.
    /// </summary>
    public bool IsUndone { get; private set; }
    public void MarkAsUndone() => IsUndone = true;
}
