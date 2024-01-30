using Common.Core.Contracts;

namespace Common.DomainEvents;

public static partial class Events
{
    public static partial class Published
    {
        public static partial class From
        {
            public static class Sales
            {
                public sealed record StoreCreated(Guid StoreId, Guid OwnerId) : DomainEvent;
                public sealed record ProductAdded(Guid StoreId, Guid ProductId) : DomainEvent;
                public sealed record ProductRemoved(Guid StoreId, Guid ProductId) : DomainEvent;
            }
        }
    }
}
