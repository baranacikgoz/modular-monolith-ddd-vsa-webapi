using Common.Application.DTOs;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Get;

public sealed record Response : AuditableEntityResponse<StoreId>
{
    public ApplicationUserId OwnerId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Address { get; init; }
    public required int ProductCount { get; init; }
}
