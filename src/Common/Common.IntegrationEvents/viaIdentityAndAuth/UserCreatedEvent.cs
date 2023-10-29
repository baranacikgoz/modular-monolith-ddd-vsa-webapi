using Common.Core.Contracts;

namespace Common.IntegrationEvents.viaIdentityAndAuth;

public static partial class Events
{
    public static partial class IdentityAndAuth
    {
        public record UserCreatedEvent(Guid UserId) : DomainEvent;
    }
}
