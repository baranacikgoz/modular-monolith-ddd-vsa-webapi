using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Create;

public sealed record Response
{
    public required StoreId Id { get; init; }
}
