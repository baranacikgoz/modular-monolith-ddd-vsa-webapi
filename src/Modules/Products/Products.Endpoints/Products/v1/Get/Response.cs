using Common.Application.DTOs;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.Get;

public sealed record Response : AuditableEntityResponse<ProductId>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
    public string? Sku { get; init; }
    public IReadOnlyList<ProductAttribute> Attributes { get; init; } = [];
    public PackageDimensionsResponse? PackageDimensions { get; init; }

    // Populated via a cross-module InterModuleRequest to Inventory after the DB projection below -
    // 0 until Inventory's ProductCreatedIntegrationEvent consumer has processed this product (async).
    public int AvailableQuantity { get; init; }
}

public sealed record PackageDimensionsResponse(decimal LengthCm, decimal WidthCm, decimal HeightCm, decimal WeightKg);
