using Common.Application.DTOs;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Stores;

namespace Products.Application.Stores.DTOs;

public sealed record StoreDto : AuditableEntityDto<StoreId>
{
    public required ApplicationUserId OwnerId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Address { get; init; }
    public required int ProductCount { get; init; }
}
