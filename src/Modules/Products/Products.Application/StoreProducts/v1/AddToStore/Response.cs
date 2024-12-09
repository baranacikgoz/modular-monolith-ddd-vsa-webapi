using Products.Domain.StoreProducts;

namespace Products.Application.StoreProducts.v1.AddToStore;

public sealed record Response
{
    public required StoreProductId Id { get; init; }
}
