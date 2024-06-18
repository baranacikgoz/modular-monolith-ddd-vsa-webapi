using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Inventory.Domain.Products;

namespace Inventory.Application.StoreProducts.v1.My.AddToStore;

public sealed record Request(
    int Quantity,
    decimal Price)
{
    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<ProductId>))]
    public ProductId ProductId { get; init; }
}
