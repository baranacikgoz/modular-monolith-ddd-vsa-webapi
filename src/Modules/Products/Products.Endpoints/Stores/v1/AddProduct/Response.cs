using Products.Domain.Products;

namespace Products.Endpoints.Stores.v1.AddProduct;

public sealed record Response
{
    public ProductId Id { get; init; }
}
