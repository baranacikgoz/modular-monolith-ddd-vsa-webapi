using Common.Domain.StronglyTypedIds;
using Inventory.Domain.StockLevels;

namespace Inventory.Endpoints.StockLevels.v1.Movements;

/// <summary>One ledger row. <see cref="ActingUserId" /> is empty for a movement a consumer made without a user.</summary>
public sealed record Response
{
    public required StockMovementId Id { get; init; }
    public required StockMovementType Type { get; init; }
    public required int Quantity { get; init; }
    public required int QuantityOnHandAfter { get; init; }
    public string? Reason { get; init; }
    public DefaultIdType? ReservationId { get; init; }
    public ApplicationUserId? ActingUserId { get; init; }
    public required DateTimeOffset CreatedOn { get; init; }
}
