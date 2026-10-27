using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;

namespace Inventory.Domain.StockLevels;

public readonly record struct StockMovementId(DefaultIdType Value) : IStronglyTypedId
{
    public static StockMovementId New()
    {
        return new StockMovementId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out StockMovementId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

/// <summary>
///     One row of a stock level's ledger. Never updated and never deleted: it has no mutator, only
///     <see cref="StockLevel" /> can create one, and a wrong row is corrected by a new <see cref="StockMovementType.Adjusted" />
///     movement. Who and when come from the auditing interceptor (<c>CreatedBy</c>, <c>CreatedOn</c>); a movement made by
///     a consumer has no <c>CreatedBy</c>. The ledger records changes from the stock level's creation on, and
///     <see cref="QuantityOnHandAfter" /> carries the running quantity so a row stands on its own. <see cref="Sequence" /> is
///     the row's place in its stock level's ledger.
/// </summary>
public class StockMovement : AuditableEntity<StockMovementId>
{
    private StockMovement() : base(new StockMovementId(DefaultIdType.Empty))
    {
    } // EF Core needs a parameterless ctor

    public StockLevelId StockLevelId { get; private set; }

    /// <summary>
    ///     The row's place in its stock level's ledger: 1 for the first, one more for each row after it, assigned by
    ///     <see cref="StockLevel" /> when it appends the row. The id and the timestamp cannot say which of two rows of one save
    ///     came first (the id is a version 7 uuid, random inside a millisecond), this can. Unique per stock level.
    /// </summary>
    public long Sequence { get; private set; }

    public StockMovementType Type { get; private set; }

    /// <summary>Signed: plus for stock in, minus for stock out.</summary>
    public int Quantity { get; private set; }

    public int QuantityOnHandAfter { get; private set; }
    public string? Reason { get; private set; }

    /// <summary>Set only on a <see cref="StockMovementType.ReservationCommitted" /> movement.</summary>
    public DefaultIdType? ReservationId { get; private set; }

    internal static StockMovement Create(
        StockLevelId stockLevelId,
        long sequence,
        StockMovementType type,
        int quantity,
        int quantityOnHandAfter,
        string? reason,
        DefaultIdType? reservationId)
    {
        return new StockMovement
        {
            Id = StockMovementId.New(),
            StockLevelId = stockLevelId,
            Sequence = sequence,
            Type = type,
            Quantity = quantity,
            QuantityOnHandAfter = quantityOnHandAfter,
            Reason = string.IsNullOrWhiteSpace(reason) ? null : reason.Trim(),
            ReservationId = reservationId
        };
    }
}
