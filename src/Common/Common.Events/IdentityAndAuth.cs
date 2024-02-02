using Common.Core.Contracts;

namespace Common.Events;

public static partial class EventsOf
{
    public static class IdentityAndAuth
    {
        public sealed record UserCreatedDomainEvent(Guid UserId, string Name) : DomainEvent;
    }

}
