using Common.Application.DTOs;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.Get;

public sealed record Response : AuditableEntityResponse<ProductId>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }

    // Populated via a cross-module InterModuleRequest to Inventory after the DB projection below -
    // 0 until Inventory's ProductCreatedIntegrationEvent consumer has processed this product (async).
    public int AvailableQuantity { get; init; }
}
