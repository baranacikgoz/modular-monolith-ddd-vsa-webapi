using Common.Application.DTOs;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Application.Products.DTOs;

public sealed record ProductDto : AuditableEntityDto<ProductId>
{
    public required StoreId StoreId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
}
