using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;

namespace Inventory.Application.Stores.v1.My.StoreProducts.Search;
public sealed record Response(StoreProductId Id, string Name, string Description, int Quantity, decimal Price);
