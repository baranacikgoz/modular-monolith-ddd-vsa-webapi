using Common.Core.Contracts;

namespace Common.Events;

public static partial class EventsOf
{
    public static class Sales
    {
        public sealed record StoreCreatedDomainEvent(Guid StoreId, Guid OwnerId) : DomainEvent;
        public sealed record ProductAddedDomainEvent(Guid StoreId, Guid ProductId) : DomainEvent;
        public sealed record ProductRemovedDomainEvent(Guid StoreId, Guid ProductId) : DomainEvent;
    }
}
