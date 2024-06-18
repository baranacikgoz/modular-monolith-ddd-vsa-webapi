using Inventory.Domain.StoreProducts;

namespace Inventory.Application.StoreProducts.v1.My.Get;
public sealed record Response(StoreProductId Id, string Name, string Description);
