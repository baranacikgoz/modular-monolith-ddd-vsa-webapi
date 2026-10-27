namespace Inventory.Domain.StockLevels;

/// <summary>Why a <see cref="StockMovement" /> exists. Stored as its name, so a member is never renamed or reordered away.</summary>
public enum StockMovementType
{
    /// <summary>Stock arrived (a delivery, a return to shelf).</summary>
    Received,

    /// <summary>A manual correction, plus or minus, always with a reason.</summary>
    Adjusted,

    /// <summary>A committed stock reservation took its quantity out of the on-hand stock.</summary>
    ReservationCommitted
}
