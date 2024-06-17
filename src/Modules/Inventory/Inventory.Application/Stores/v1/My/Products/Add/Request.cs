using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Inventory.Domain.Products;

namespace Inventory.Application.Stores.v1.My.Products.Add;

public sealed record Request(
    int Quantity,
    decimal Price)
{
    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<ProductId>))]
    public ProductId ProductId { get; init; }
}
