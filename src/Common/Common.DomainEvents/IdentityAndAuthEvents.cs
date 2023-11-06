using Common.Core.Contracts;

namespace Common.DomainEvents;

public static partial class Events
{
    public static class IdentityAndAuth
    {
        public record UserCreatedEvent(Guid UserId) : DomainEvent;
    }
}
