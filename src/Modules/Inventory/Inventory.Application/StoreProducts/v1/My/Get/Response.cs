using Common.Application.DTOs;
using Inventory.Domain.StoreProducts;

namespace Inventory.Application.StoreProducts.v1.My.Get;

public sealed record Response : AuditableEntityResponse<StoreProductId>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
}
