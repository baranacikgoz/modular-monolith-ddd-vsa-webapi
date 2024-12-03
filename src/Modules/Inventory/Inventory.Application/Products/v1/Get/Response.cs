using Common.Application.DTOs;
using Inventory.Domain.Products;

namespace Inventory.Application.Products.v1.Get;

public sealed record Response : AuditableEntityResponse<ProductId>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}
