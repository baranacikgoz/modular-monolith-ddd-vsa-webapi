using Common.Application.DTOs;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;

namespace Inventory.Application.Stores.v1.Search;

public sealed record Response : AuditableEntityResponse<StoreId>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int ProductCount { get; init; }
    public Uri? LogoUrl { get; init; }
    public ApplicationUserId OwnerId { get; init; }
}
