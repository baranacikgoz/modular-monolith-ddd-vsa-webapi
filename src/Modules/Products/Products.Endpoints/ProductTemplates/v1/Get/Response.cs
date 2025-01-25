using Common.Application.DTOs;
using Products.Domain.ProductTemplates;

namespace Products.Endpoints.ProductTemplates.v1.Get;

public sealed record Response : AuditableEntityDto<ProductTemplateId>
{
    public required string Brand { get; init; }
    public required string Model { get; init; }
    public required string Color { get; init; }
}
