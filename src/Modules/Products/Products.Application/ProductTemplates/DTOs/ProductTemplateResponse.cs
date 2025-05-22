using Common.Application.DTOs;
using Products.Domain.ProductTemplates;

namespace Products.Application.ProductTemplates.DTOs;

public sealed record ProductTemplateResponse : AuditableEntityResponse<ProductTemplateId>
{
    public required string Brand { get; init; }
    public required string Model { get; init; }
    public required string Color { get; init; }
}
