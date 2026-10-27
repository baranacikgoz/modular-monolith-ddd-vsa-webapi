using Common.Domain.Events;
using Products.Domain.Products;

namespace Products.Domain.Stores.DomainEvents.v1;

/// <summary>
///     Raised next to <see cref="V1ProductRemovedFromStoreDomainEvent" /> when the removed product still had images: their
///     rows go with the product, but the files stay in the public object store under <paramref name="ObjectKeys" /> until
///     something deletes them.
/// </summary>
public sealed record V1ProductImagesRemovedWithProductDomainEvent(
    StoreId StoreId,
    ProductId ProductId,
    IReadOnlyList<string> ObjectKeys
) : DomainEvent;
