using Common.Core.Contracts;

namespace Common.DomainEvents;

public static partial class Events
{
    public static partial class Published
    {
        public static partial class From
        {
            public static class IdentityAndAuth
            {
                public sealed record UserCreated(Guid UserId, string Name) : DomainEvent;
            }
        }
    }
}
