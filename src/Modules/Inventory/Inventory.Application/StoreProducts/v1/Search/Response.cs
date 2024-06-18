using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;

namespace Inventory.Application.StoreProducts.v1.Search;
public sealed record Response(StoreProductId Id, string Name, string Description, int Quantity, decimal Price);
