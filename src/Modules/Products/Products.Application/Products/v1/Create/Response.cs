using Products.Domain.Products;

namespace Products.Application.Products.v1.Create;

internal sealed record Response
{
    public required ProductId Id { get; init; }
}
