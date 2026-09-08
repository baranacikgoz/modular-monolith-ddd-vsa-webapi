using Common.Domain.Events;

namespace Products.Domain.Stores.DomainEvents.v1;

/// <summary>
///     Raised instead of <see cref="V1StoreDeactivatedDomainEvent"/> when the store still holds products with
///     stock on hand at deactivation time - a downstream consumer (e.g. inventory reconciliation) needs to
///     know this branch happened without re-querying every product's quantity itself.
/// </summary>
public sealed record V1StoreDeactivatedWithStockOnHandDomainEvent(StoreId StoreId, int ProductsWithStockCount) : DomainEvent;
