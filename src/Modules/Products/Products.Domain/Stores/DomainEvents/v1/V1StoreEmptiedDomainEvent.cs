using Common.Domain.Events;

namespace Products.Domain.Stores.DomainEvents.v1;

/// <summary>
///     Cascading event: raised in addition to <see cref="V1ProductRemovedFromStoreDomainEvent"/> when removing
///     a product leaves the store with none left - a post-mutation collection-state check deciding whether a
///     second, higher-level event fires alongside the primary one.
/// </summary>
public sealed record V1StoreEmptiedDomainEvent(StoreId StoreId) : DomainEvent;
