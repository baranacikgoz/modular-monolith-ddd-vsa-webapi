using Common.Core.Contracts;

namespace Common.DomainEvents.viaIdentityAndAuth;

public static partial class Events
{
    public static partial class IdentityAndAuth
    {
        public record UserCreatedEvent(Guid UserId) : DomainEvent;
    }
}
