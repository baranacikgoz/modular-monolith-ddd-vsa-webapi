using Common.Core.Interfaces;

namespace IdentityAndAuth.Features.Identity.Domain;

public sealed partial class ApplicationUser
{
    private readonly Queue<IEvent> _events = [];
    public bool HasAnyEvent => _events.Count > 0;

    public bool TryDequeueEvent(out IEvent? @event) => _events.TryDequeue(out @event);
    private void EnqueueEvent(IEvent @event) => _events.Enqueue(@event);
    private void RaiseEvent(IEvent @event)
    {
        Apply(@event);
        EnqueueEvent(@event);
    }
}
