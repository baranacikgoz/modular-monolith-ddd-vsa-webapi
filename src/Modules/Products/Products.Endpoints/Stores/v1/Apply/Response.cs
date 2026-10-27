using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Apply;

public sealed record Response
{
    public required StoreId Id { get; init; }
}
