using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;

namespace Inventory.Application.Stores.v1.My.Create;

public sealed record Response
{
    public required StoreId Id { get; init; }
}

