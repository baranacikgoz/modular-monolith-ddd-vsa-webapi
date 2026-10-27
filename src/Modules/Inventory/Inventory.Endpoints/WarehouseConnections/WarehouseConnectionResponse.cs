using Common.Application.DTOs;
using Inventory.Domain.WarehouseConnections;

namespace Inventory.Endpoints.WarehouseConnections;

/// <summary>
///     Shared by every slice that returns a connection. It never carries a credential or a webhook secret, in any form:
///     the webhook secret is shown once by the slice that generates it.
/// </summary>
public sealed record WarehouseConnectionResponse : AuditableEntityResponse<WarehouseConnectionId>
{
    public required DefaultIdType StoreId { get; init; }
    public required WarehouseProvider Provider { get; init; }
    public required Uri BaseUrl { get; init; }
    public required WarehouseConnectionStatus Status { get; init; }
}
