using Inventory.Domain.Products;

namespace Inventory.Application.Products.v1.Create;

internal sealed record Response
{
    public required ProductId Id { get; init; }
}
