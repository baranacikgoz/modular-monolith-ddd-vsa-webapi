using System.Net;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Gateway;
using Xunit;

namespace Inventory.Tests.Gateway;

/// <summary>The in-memory provider answers like the HTTP providers do, with nothing to fake and nothing on the wire.</summary>
public class DummyWarehouseGatewayTests
{
    private readonly DummyWarehouseState _state = new();
    private readonly WarehouseConnectionId _connection = WarehouseConnectionId.New();

    private DummyWarehouseGateway Gateway(WarehouseConnectionId? connection = null)
    {
        return new DummyWarehouseGateway(connection ?? _connection, _state);
    }

    [Fact]
    public async Task RequestReleaseAsync_AnyReservation_AnswersADeterministicReferencePerReservation()
    {
        var reservation = StockReservationId.New();
        var otherReservation = StockReservationId.New();

        var first = await Gateway().RequestReleaseAsync(reservation, "product-1", 5, CancellationToken.None);
        var again = await Gateway().RequestReleaseAsync(reservation, "product-1", 5, CancellationToken.None);
        var other = await Gateway().RequestReleaseAsync(otherReservation, "product-1", 5, CancellationToken.None);

        Assert.False(first.IsFailure);
        Assert.Equal($"dummy-release-{reservation}", first.Value);
        Assert.Equal(first.Value, again.Value);
        Assert.Equal($"dummy-release-{otherReservation}", other.Value);
    }

    // A reconcile asks about a release by its reservation: it finds the one that was made, and reports the missing reference
    // otherwise, the same answers the HTTP providers give for a 200 and for a 404 that says so.
    [Fact]
    public async Task ReconcileReleaseAsync_ReleaseWasMade_ReturnsItsReference()
    {
        var reservation = StockReservationId.New();
        await Gateway().RequestReleaseAsync(reservation, "product-1", 5, CancellationToken.None);

        var result = await Gateway().ReconcileReleaseAsync(reservation, CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal($"dummy-release-{reservation}", result.Value);
    }

    [Fact]
    public async Task ReconcileReleaseAsync_NoReleaseWasMade_ReturnsTheReferenceNotFound()
    {
        var result = await Gateway().ReconcileReleaseAsync(StockReservationId.New(), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("NotFound", result.Error!.Key);
        Assert.Equal("WarehouseReference", result.Error.ParameterName);
        Assert.Equal(HttpStatusCode.NotFound, result.Error.StatusCode);
    }

    [Fact]
    public async Task ReconcileReleaseAsync_ReleaseOfAnotherConnection_IsNotSeen()
    {
        var reservation = StockReservationId.New();
        await Gateway(WarehouseConnectionId.New()).RequestReleaseAsync(reservation, "product-1", 5, CancellationToken.None);

        var result = await Gateway().ReconcileReleaseAsync(reservation, CancellationToken.None);

        Assert.True(result.IsFailure);
    }

    // The old key was the product, so releasing one reservation made the other reservation of the same product look released.
    [Fact]
    public async Task ReconcileReleaseAsync_TwoReservationsOfOneProduct_AreAskedAboutIndependently()
    {
        var released = StockReservationId.New();
        var notReleased = StockReservationId.New();
        await Gateway().RequestReleaseAsync(released, "product-1", 5, CancellationToken.None);

        var answerForReleased = await Gateway().ReconcileReleaseAsync(released, CancellationToken.None);
        var answerForNotReleased = await Gateway().ReconcileReleaseAsync(notReleased, CancellationToken.None);

        Assert.False(answerForReleased.IsFailure);
        Assert.True(answerForNotReleased.IsFailure);
    }

    [Fact]
    public async Task PushStockAsync_Quantities_SetsThemAbsolutelyAndNumbersTheBatches()
    {
        var first = await Gateway().PushStockAsync([new("product-1", 7), new("product-2", 0)], CancellationToken.None);
        var second = await Gateway().PushStockAsync([new("product-1", 3)], CancellationToken.None);

        Assert.Equal("dummy-stock-1", first.Value);
        Assert.Equal("dummy-stock-2", second.Value);
        Assert.Equal(3, _state.StockOf(_connection, "product-1"));
        Assert.Equal(0, _state.StockOf(_connection, "product-2"));
        Assert.Null(_state.StockOf(_connection, "product-3"));
        Assert.Null(_state.StockOf(WarehouseConnectionId.New(), "product-1"));
    }

    // The HTTP providers raise the caller's cancellation as it is, not as an unknown outcome; the dummy does the same.
    [Fact]
    public async Task EveryCall_CallerCancelled_PropagatesTheCancellation()
    {
        using var cancelled = new CancellationTokenSource();
        await cancelled.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => Gateway().RequestReleaseAsync(StockReservationId.New(), "product-1", 1, cancelled.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => Gateway().ReconcileReleaseAsync(StockReservationId.New(), cancelled.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => Gateway().PushStockAsync([new("product-1", 1)], cancelled.Token));
    }
}
