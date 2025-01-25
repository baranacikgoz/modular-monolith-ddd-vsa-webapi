using Products.Domain.ProductTemplates;

namespace Products.Endpoints.ProductTemplates.v1.Create;

public sealed record Response
{
    public ProductTemplateId Id { get; init; }
}
