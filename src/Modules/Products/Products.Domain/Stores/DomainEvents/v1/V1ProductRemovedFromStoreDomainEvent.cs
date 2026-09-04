using Common.Domain.Events;
using Products.Domain.ProductTemplates;
using Products.Domain.Products;

namespace Products.Domain.Stores.DomainEvents.v1;

public sealed record V1ProductRemovedFromStoreDomainEvent(
    StoreId StoreId,
    V1ProductRemovedFromStoreDomainEvent.ProductSnapshot Product
) : DomainEvent
{
    public sealed record ProductSnapshot(
        ProductId ProductId,
        ProductTemplateId ProductTemplateId,
        string Name,
        string Description,
        int Quantity,
        decimal Price);
}

internal static class V1ProductRemovedFromStoreDomainEventExtensions
{
    public static V1ProductRemovedFromStoreDomainEvent.ProductSnapshot ToRemovedSnapshot(this Product product)
    {
        return new V1ProductRemovedFromStoreDomainEvent.ProductSnapshot(
            product.Id, product.ProductTemplateId, product.Name, product.Description, product.Quantity, product.Price);
    }
}
