using Inventory.Domain.StoreProducts;

namespace Inventory.Application.StoreProducts.v1.AddToStore;

public sealed record Response
{
    public required StoreProductId Id { get; init; }
}
