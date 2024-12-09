using Common.Application.DTOs;
using Products.Domain.StoreProducts;

namespace Products.Application.StoreProducts.v1.My.Get;

public sealed record Response : AuditableEntityResponse<StoreProductId>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
}
