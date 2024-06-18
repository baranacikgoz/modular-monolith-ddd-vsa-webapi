using Inventory.Domain.StoreProducts;

namespace Inventory.Application.Stores.v1.My.StoreProducts.Get;
public sealed record Response(StoreProductId Id, string Name, string Description);
