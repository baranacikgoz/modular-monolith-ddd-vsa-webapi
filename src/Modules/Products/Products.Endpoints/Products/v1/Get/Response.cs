using Common.Application.DTOs;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.Get;

public sealed record Response : AuditableEntityResponse<ProductId>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
}
