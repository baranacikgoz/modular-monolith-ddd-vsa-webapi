using Inventory.Domain.StoreProducts;

namespace Inventory.Application.StoreProducts.v1.My.AddToMyStore;

public sealed record Response
{
    public required StoreProductId Id { get; init; }
}
