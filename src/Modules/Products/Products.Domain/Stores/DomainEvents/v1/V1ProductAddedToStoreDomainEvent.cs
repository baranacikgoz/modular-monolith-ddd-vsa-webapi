using Common.Domain.Events;
using Products.Domain.ProductTemplates;
using Products.Domain.Products;

namespace Products.Domain.Stores.DomainEvents.v1;

public sealed record V1ProductAddedToStoreDomainEvent(
    StoreId StoreId,
    V1ProductAddedToStoreDomainEvent.ProductSnapshot Product
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

internal static class V1ProductAddedToStoreDomainEventExtensions
{
    public static V1ProductAddedToStoreDomainEvent.ProductSnapshot ToAddedSnapshot(this Product product)
    {
        return new V1ProductAddedToStoreDomainEvent.ProductSnapshot(
            product.Id, product.ProductTemplateId, product.Name, product.Description, product.Quantity, product.Price);
    }
}
