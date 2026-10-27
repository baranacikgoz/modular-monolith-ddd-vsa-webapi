using System.Reflection;
using Inventory.Domain.StockLevels;
using Inventory.Domain.StockLevels.DomainEvents.v1;
using Inventory.Domain.StockLevels.Errors;
using Xunit;

namespace Inventory.Tests.StockLevels;

public class StockLevelTests
{
    private static StockLevel NewStockLevel(int quantityOnHand = 10, int lowStockThreshold = 0)
    {
        var stockLevel = StockLevel.Create(DefaultIdType.CreateVersion7(), quantityOnHand);
        if (lowStockThreshold > 0)
        {
            stockLevel.ChangeLowStockThreshold(lowStockThreshold);
        }

        stockLevel.ClearEvents();
        return stockLevel;
    }

    [Fact]
    public void Receive_PositiveQuantity_IncreasesOnHandAndAppendsALedgerRow()
    {
        var stockLevel = NewStockLevel(10);

        var result = stockLevel.Receive(5, "Supplier delivery");

        Assert.False(result.IsFailure);
        Assert.Equal(15, stockLevel.QuantityOnHand);
        var movement = Assert.Single(stockLevel.Movements);
        Assert.Equal(StockMovementType.Received, movement.Type);
        Assert.Equal(5, movement.Quantity);
        Assert.Equal(15, movement.QuantityOnHandAfter);
        Assert.Equal("Supplier delivery", movement.Reason);
        Assert.Null(movement.ReservationId);
        Assert.Equal(stockLevel.Id, movement.StockLevelId);
    }

    [Fact]
    public void Receive_PositiveQuantity_RaisesOneQuantityChangedEventCarryingTheMovement()
    {
        var stockLevel = NewStockLevel(10);

        stockLevel.Receive(5, null);

        var @event = Assert.Single(stockLevel.Events.OfType<V1StockQuantityChangedDomainEvent>());
        Assert.Equal(stockLevel.Id, @event.StockLevelId);
        Assert.Equal(stockLevel.ProductId, @event.ProductId);
        Assert.Equal(stockLevel.Movements.Single().Id, @event.MovementId);
        Assert.Equal(V1StockQuantityChangedDomainEvent.MovementTypeSnapshot.Received, @event.MovementType);
        Assert.Equal(5, @event.Quantity);
        Assert.Equal(15, @event.QuantityOnHandAfter);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void Receive_NonPositiveQuantity_ReturnsQuantityMustBePositiveAndChangesNothing(int quantity)
    {
        var stockLevel = NewStockLevel(10);

        var result = stockLevel.Receive(quantity, null);

        Assert.Equal(StockLevelErrors.QuantityMustBePositive.Key, result.Error!.Key);
        Assert.Equal(10, stockLevel.QuantityOnHand);
        Assert.Empty(stockLevel.Movements);
        Assert.Empty(stockLevel.Events);
    }

    [Fact]
    public void Adjust_NegativeQuantity_DecreasesOnHandAndRecordsTheReason()
    {
        var stockLevel = NewStockLevel(10);

        var result = stockLevel.Adjust(-4, "Damaged in storage");

        Assert.False(result.IsFailure);
        Assert.Equal(6, stockLevel.QuantityOnHand);
        var movement = Assert.Single(stockLevel.Movements);
        Assert.Equal(StockMovementType.Adjusted, movement.Type);
        Assert.Equal(-4, movement.Quantity);
        Assert.Equal(6, movement.QuantityOnHandAfter);
        Assert.Equal("Damaged in storage", movement.Reason);
    }

    [Fact]
    public void Adjust_ZeroQuantity_ReturnsAdjustmentCannotBeZero()
    {
        var stockLevel = NewStockLevel(10);

        var result = stockLevel.Adjust(0, "No-op");

        Assert.Equal(StockLevelErrors.AdjustmentCannotBeZero.Key, result.Error!.Key);
        Assert.Empty(stockLevel.Movements);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Adjust_BlankReason_ReturnsReasonRequired(string reason)
    {
        var stockLevel = NewStockLevel(10);

        var result = stockLevel.Adjust(-1, reason);

        Assert.Equal(StockLevelErrors.ReasonRequired.Key, result.Error!.Key);
        Assert.Empty(stockLevel.Movements);
    }

    [Fact]
    public void Adjust_BelowZero_ReturnsInsufficientStockAndChangesNothing()
    {
        var stockLevel = NewStockLevel(3);

        var result = stockLevel.Adjust(-4, "Count correction");

        Assert.Equal(StockLevelErrors.InsufficientStock.Key, result.Error!.Key);
        Assert.Equal(3, stockLevel.QuantityOnHand);
        Assert.Empty(stockLevel.Movements);
        Assert.Empty(stockLevel.Events);
    }

    [Fact]
    public void DeductForReservation_RecordsTheReservationOnTheMovement()
    {
        var stockLevel = NewStockLevel(10);
        var reservationId = DefaultIdType.CreateVersion7();

        var result = stockLevel.DeductForReservation(reservationId, 4, alreadyDeducted: false);

        Assert.False(result.IsFailure);
        Assert.Equal(6, stockLevel.QuantityOnHand);
        var movement = Assert.Single(stockLevel.Movements);
        Assert.Equal(StockMovementType.ReservationCommitted, movement.Type);
        Assert.Equal(-4, movement.Quantity);
        Assert.Equal(6, movement.QuantityOnHandAfter);
        Assert.Equal(reservationId, movement.ReservationId);
        Assert.Null(movement.Reason);
    }

    [Fact]
    public void DeductForReservation_MoreThanOnHand_ReturnsInsufficientStock()
    {
        var stockLevel = NewStockLevel(3);

        var result = stockLevel.DeductForReservation(DefaultIdType.CreateVersion7(), 4, alreadyDeducted: false);

        Assert.Equal(StockLevelErrors.InsufficientStock.Key, result.Error!.Key);
        Assert.Empty(stockLevel.Movements);
    }

    [Fact]
    public void DeductForReservation_AlreadyDeducted_ReturnsReservationAlreadyDeductedAndChangesNothing()
    {
        var stockLevel = NewStockLevel(10);

        var result = stockLevel.DeductForReservation(DefaultIdType.CreateVersion7(), 4, alreadyDeducted: true);

        Assert.Equal(StockLevelErrors.ReservationAlreadyDeducted.Key, result.Error!.Key);
        Assert.Equal(10, stockLevel.QuantityOnHand);
        Assert.Empty(stockLevel.Movements);
    }

    [Fact]
    public void TwoMovements_ChainTheOnHandQuantityInTheLedger()
    {
        var stockLevel = NewStockLevel(10);

        stockLevel.Receive(5, null);
        stockLevel.Adjust(-2, "Count correction");

        Assert.Equal([15, 13], stockLevel.Movements.Select(m => m.QuantityOnHandAfter).ToArray());
    }

    [Fact]
    public void ChangeLowStockThreshold_NewValue_StoresItAndRaisesTheChangedEvent()
    {
        var stockLevel = NewStockLevel(10);

        var result = stockLevel.ChangeLowStockThreshold(4);

        Assert.False(result.IsFailure);
        Assert.Equal(4, stockLevel.LowStockThreshold);
        var @event = Assert.Single(stockLevel.Events.OfType<V1StockLowThresholdChangedDomainEvent>());
        Assert.Equal(0, @event.PreviousThreshold);
        Assert.Equal(4, @event.NewThreshold);
    }

    [Fact]
    public void ChangeLowStockThreshold_SameValue_IsANoOpWithoutEvents()
    {
        var stockLevel = NewStockLevel(10, lowStockThreshold: 4);

        var result = stockLevel.ChangeLowStockThreshold(4);

        Assert.False(result.IsFailure);
        Assert.Empty(stockLevel.Events);
    }

    [Fact]
    public void ChangeLowStockThreshold_Negative_ReturnsLowStockThresholdCannotBeNegative()
    {
        var stockLevel = NewStockLevel(10);

        var result = stockLevel.ChangeLowStockThreshold(-1);

        Assert.Equal(StockLevelErrors.LowStockThresholdCannotBeNegative.Key, result.Error!.Key);
        Assert.Equal(0, stockLevel.LowStockThreshold);
    }

    [Fact]
    public void Movement_FromAtOrAboveToBelowTheThreshold_RaisesTheCrossedEventOnce()
    {
        var stockLevel = NewStockLevel(10, lowStockThreshold: 5);

        stockLevel.Adjust(-6, "Count correction");
        stockLevel.Adjust(-1, "Count correction");

        var @event = Assert.Single(stockLevel.Events.OfType<V1StockLowThresholdCrossedDomainEvent>());
        Assert.Equal(4, @event.QuantityOnHand);
        Assert.Equal(5, @event.LowStockThreshold);
    }

    [Fact]
    public void Movement_LandingExactlyOnTheThreshold_DoesNotCross()
    {
        var stockLevel = NewStockLevel(10, lowStockThreshold: 5);

        stockLevel.Adjust(-5, "Count correction");

        Assert.Empty(stockLevel.Events.OfType<V1StockLowThresholdCrossedDomainEvent>());
    }

    [Fact]
    public void Movement_FromBelowToAtOrAboveTheThreshold_RaisesTheRecoveredEventOnce()
    {
        var stockLevel = NewStockLevel(3, lowStockThreshold: 5);

        stockLevel.Receive(2, null);
        stockLevel.Receive(10, null);

        var @event = Assert.Single(stockLevel.Events.OfType<V1StockLowThresholdRecoveredDomainEvent>());
        Assert.Equal(5, @event.QuantityOnHand);
        Assert.Equal(5, @event.LowStockThreshold);
    }

    [Fact]
    public void ChangeLowStockThreshold_RaisedAboveOnHand_RaisesTheCrossedEvent()
    {
        var stockLevel = NewStockLevel(10);

        stockLevel.ChangeLowStockThreshold(12);

        var @event = Assert.Single(stockLevel.Events.OfType<V1StockLowThresholdCrossedDomainEvent>());
        Assert.Equal(10, @event.QuantityOnHand);
        Assert.Equal(12, @event.LowStockThreshold);
    }

    [Fact]
    public void ChangeLowStockThreshold_AlreadyBelow_DoesNotRaiseCrossedAgain()
    {
        var stockLevel = NewStockLevel(3, lowStockThreshold: 5);

        stockLevel.ChangeLowStockThreshold(8);

        Assert.Empty(stockLevel.Events.OfType<V1StockLowThresholdCrossedDomainEvent>());
    }

    [Fact]
    public void ChangeLowStockThreshold_LoweredToOrBelowOnHand_RaisesTheRecoveredEvent()
    {
        var stockLevel = NewStockLevel(3, lowStockThreshold: 5);

        stockLevel.ChangeLowStockThreshold(2);

        var @event = Assert.Single(stockLevel.Events.OfType<V1StockLowThresholdRecoveredDomainEvent>());
        Assert.Equal(3, @event.QuantityOnHand);
        Assert.Equal(2, @event.LowStockThreshold);
    }

    // The ledger is append-only by construction: a row has no mutator and only StockLevel can create one.
    [Fact]
    public void StockMovement_HasNoPublicSetterAndNoPublicFactory()
    {
        var publicSetters = typeof(StockMovement)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(p => p.SetMethod is { IsPublic: true })
            .Select(p => p.Name)
            .ToList();
        var publicMethods = typeof(StockMovement)
            .GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(m => !m.IsSpecialName)
            .Select(m => m.Name)
            .ToList();

        Assert.Empty(publicSetters);
        Assert.Empty(publicMethods);
    }
}
