using Common.Domain.Aggregates;
using Common.Domain.Events;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.StockLevels.DomainEvents.v1;
using Inventory.Domain.StockLevels.Errors;

namespace Inventory.Domain.StockLevels;

public readonly record struct StockLevelId(DefaultIdType Value) : IStronglyTypedId
{
    public static StockLevelId New()
    {
        return new StockLevelId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out StockLevelId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

/// <summary>
///     The on-hand stock of one product. Created by consuming Products' <c>ProductCreatedIntegrationEvent</c> (see
///     Inventory.Application/IntegrationEventHandlers) with the product's initial quantity; from then on the quantity
///     changes only through the movement commands (<see cref="Receive" />, <see cref="Adjust" />,
///     <see cref="DeductForReservation" />), each of which appends one immutable <see cref="StockMovement" /> to the
///     ledger. Later changes to Product.Quantity are not published as integration events in this template, so they do
///     not reach this quantity. <see cref="LowStockThreshold" /> 0 means no low stock alert.
/// </summary>
public class StockLevel : AggregateRoot<StockLevelId>
{
    // Carries only the movements added in the current unit of work: the ledger grows without bound and is never Included.
    private readonly List<StockMovement> _movements = [];

#pragma warning disable CS8618
    private StockLevel() : base(new StockLevelId(DefaultIdType.Empty)) { }
#pragma warning restore CS8618

    public DefaultIdType ProductId { get; private set; }
    public int QuantityOnHand { get; private set; }
    public int LowStockThreshold { get; private set; }

    public IReadOnlyCollection<StockMovement> Movements => _movements.AsReadOnly();

    public static StockLevel Create(DefaultIdType productId, int quantityOnHand)
    {
        var id = StockLevelId.New();

        var @event = new V1StockLevelCreatedDomainEvent(id, productId, quantityOnHand);

        var stockLevel = new StockLevel
        {
            Id = id,
            ProductId = productId,
            QuantityOnHand = quantityOnHand
        };

        stockLevel.RaiseEvent(@event);
        return stockLevel;
    }

    /// <summary>Stock arrived.</summary>
    public Result Receive(int quantity, string? reason)
    {
        if (quantity <= 0)
        {
            return StockLevelErrors.QuantityMustBePositive;
        }

        return ApplyMovement(StockMovementType.Received, quantity, reason, reservationId: null);
    }

    /// <summary>A manual correction, plus or minus. The reason is mandatory: it is the only explanation the ledger row carries.</summary>
    public Result Adjust(int signedQuantity, string reason)
    {
        if (signedQuantity == 0)
        {
            return StockLevelErrors.AdjustmentCannotBeZero;
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            return StockLevelErrors.ReasonRequired;
        }

        return ApplyMovement(StockMovementType.Adjusted, signedQuantity, reason, reservationId: null);
    }

    /// <summary>
    ///     A committed stock reservation takes its quantity out of the on-hand stock. <paramref name="alreadyDeducted" /> is
    ///     the caller's queried fact (the ledger holds a deduction for this reservation); the race between two callers is
    ///     closed by the unique index on the ledger's reservation id.
    /// </summary>
    public Result DeductForReservation(DefaultIdType reservationId, int quantity, bool alreadyDeducted)
    {
        if (alreadyDeducted)
        {
            return StockLevelErrors.ReservationAlreadyDeducted;
        }

        if (quantity <= 0)
        {
            return StockLevelErrors.QuantityMustBePositive;
        }

        return ApplyMovement(StockMovementType.ReservationCommitted, -quantity, reason: null, reservationId);
    }

    /// <summary>
    ///     An unchanged value raises nothing. Moving the threshold over the current quantity is a crossing too: raising it
    ///     above the quantity raises the crossed event, lowering it to or below the quantity raises the recovered event.
    /// </summary>
    public Result ChangeLowStockThreshold(int threshold)
    {
        if (threshold < 0)
        {
            return StockLevelErrors.LowStockThresholdCannotBeNegative;
        }

        if (threshold == LowStockThreshold)
        {
            return Result.Success;
        }

        var @event = new V1StockLowThresholdChangedDomainEvent(Id, ProductId, LowStockThreshold, threshold);
        var crossedEvent = QuantityOnHand >= LowStockThreshold && QuantityOnHand < threshold
            ? new V1StockLowThresholdCrossedDomainEvent(Id, ProductId, QuantityOnHand, threshold)
            : null;
        var recoveredEvent = QuantityOnHand < LowStockThreshold && QuantityOnHand >= threshold
            ? new V1StockLowThresholdRecoveredDomainEvent(Id, ProductId, QuantityOnHand, threshold)
            : null;

        LowStockThreshold = threshold;

        RaiseEvent(@event);
        RaiseWhenNotNull(crossedEvent);
        RaiseWhenNotNull(recoveredEvent);

        return Result.Success;
    }

    /// <summary>
    ///     The only place the quantity changes. Events are built from the quantity before the change, then the state
    ///     changes, then they are raised.
    /// </summary>
    private Result ApplyMovement(StockMovementType type, int signedQuantity, string? reason, DefaultIdType? reservationId)
    {
        var before = QuantityOnHand;
        var after = before + signedQuantity;
        if (after < 0)
        {
            return StockLevelErrors.InsufficientStock;
        }

        var movement = StockMovement.Create(Id, type, signedQuantity, after, reason, reservationId);
        var @event = new V1StockQuantityChangedDomainEvent(
            Id, ProductId, movement.Id, type.ToChangedSnapshot(), signedQuantity, after, movement.Reason);
        var crossedEvent = before >= LowStockThreshold && after < LowStockThreshold
            ? new V1StockLowThresholdCrossedDomainEvent(Id, ProductId, after, LowStockThreshold)
            : null;
        var recoveredEvent = before < LowStockThreshold && after >= LowStockThreshold
            ? new V1StockLowThresholdRecoveredDomainEvent(Id, ProductId, after, LowStockThreshold)
            : null;

        _movements.Add(movement);
        QuantityOnHand = after;

        RaiseEvent(@event);
        RaiseWhenNotNull(crossedEvent);
        RaiseWhenNotNull(recoveredEvent);

        return Result.Success;
    }

    private void RaiseWhenNotNull(DomainEvent? @event)
    {
        if (@event is not null)
        {
            RaiseEvent(@event);
        }
    }
}
