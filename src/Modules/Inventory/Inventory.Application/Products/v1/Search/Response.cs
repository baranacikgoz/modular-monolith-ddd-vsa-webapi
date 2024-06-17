using Inventory.Domain.Products;

namespace Inventory.Application.Products.v1.Search;
public sealed record Response(ProductId Id, string Name, string Description);
