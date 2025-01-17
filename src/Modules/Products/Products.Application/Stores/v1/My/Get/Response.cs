using Common.Application.DTOs;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Stores;

namespace Products.Application.Stores.v1.My.Get;

public sealed record Response : AuditableEntityResponse<StoreId>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int ProductCount { get; init; }
    public Uri? LogoUrl { get; init; }
    public ApplicationUserId OwnerId { get; init; }
}

