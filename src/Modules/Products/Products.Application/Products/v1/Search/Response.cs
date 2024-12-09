using Common.Application.DTOs;
using Products.Domain.Products;

namespace Products.Application.Products.v1.Search;

public sealed record Response : AuditableEntityResponse<ProductId>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}
