using Common.Domain.Events;

namespace Inventory.Domain.StockLevels.DomainEvents.v1;

/// <summary>The on-hand quantity changed by one ledger movement. The signed <see cref="Quantity" /> is plus for stock in.</summary>
public sealed record V1StockQuantityChangedDomainEvent(
    StockLevelId StockLevelId,
    DefaultIdType ProductId,
    StockMovementId MovementId,
    V1StockQuantityChangedDomainEvent.MovementTypeSnapshot MovementType,
    int Quantity,
    int QuantityOnHandAfter,
    string? Reason
) : DomainEvent
{
    public enum MovementTypeSnapshot
    {
        Received,
        Adjusted,
        ReservationCommitted
    }
}

internal static class V1StockQuantityChangedDomainEventExtensions
{
    public static V1StockQuantityChangedDomainEvent.MovementTypeSnapshot ToChangedSnapshot(this StockMovementType type)
    {
        return type switch
        {
            StockMovementType.Received => V1StockQuantityChangedDomainEvent.MovementTypeSnapshot.Received,
            StockMovementType.Adjusted => V1StockQuantityChangedDomainEvent.MovementTypeSnapshot.Adjusted,
            StockMovementType.ReservationCommitted => V1StockQuantityChangedDomainEvent.MovementTypeSnapshot.ReservationCommitted,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
