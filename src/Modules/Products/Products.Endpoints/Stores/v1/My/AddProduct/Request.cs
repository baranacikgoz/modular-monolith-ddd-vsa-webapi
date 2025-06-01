using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Products.Domain.ProductTemplates;

namespace Products.Endpoints.Stores.v1.My.AddProduct;

public sealed record Request
{
    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<ProductTemplateId>))]
    public ProductTemplateId ProductTemplateId { get; init; }

    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
}
