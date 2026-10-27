using Common.Application.DTOs;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.GetApplication;

/// <summary>The fields of a list item (<c>ListApplications</c>) plus the status and the audit columns.</summary>
public sealed record Response : AuditableEntityResponse<StoreId>
{
    public required ApplicationUserId OwnerId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Address { get; init; }
    public required StoreStatus Status { get; init; }
}
