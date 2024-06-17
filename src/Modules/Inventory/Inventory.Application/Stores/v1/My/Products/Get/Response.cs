using Inventory.Domain.StoreProducts;

namespace Inventory.Application.Stores.v1.My.Products.Get;
public sealed record Response(StoreProductId Id, string Name, string Description);
