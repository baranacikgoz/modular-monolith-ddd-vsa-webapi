using Common.Domain.StronglyTypedIds;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.ListApplications;

public sealed record Response
{
    public required StoreId Id { get; init; }
    public required ApplicationUserId OwnerId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Address { get; init; }
    public required DateTimeOffset CreatedOn { get; init; }
}
