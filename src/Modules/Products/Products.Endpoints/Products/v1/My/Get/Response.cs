using Common.Application.DTOs;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.My.Get;

public sealed record Response : AuditableEntityResponse<ProductId>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
    public string? Sku { get; init; }
    public IReadOnlyList<ProductAttribute> Attributes { get; init; } = [];
    public PackageDimensionsResponse? PackageDimensions { get; init; }
}

public sealed record PackageDimensionsResponse(decimal LengthCm, decimal WidthCm, decimal HeightCm, decimal WeightKg);
