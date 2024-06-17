using Inventory.Domain.Stores;

namespace Inventory.Application.Stores.v1.My.Get;
public sealed record Response(StoreId Id, string Name, string Description, Uri? LogoUrl, int ProductCount);
