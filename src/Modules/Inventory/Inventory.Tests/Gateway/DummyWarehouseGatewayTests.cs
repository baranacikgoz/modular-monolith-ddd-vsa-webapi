using System.Net;
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
    public async Task RequestReleaseAsync_AnyProduct_AnswersADeterministicReference()
    {
        var first = await Gateway().RequestReleaseAsync("product-1", 5, CancellationToken.None);
        var again = await Gateway().RequestReleaseAsync("product-1", 5, CancellationToken.None);
        var other = await Gateway().RequestReleaseAsync("product-2", 5, CancellationToken.None);

        Assert.False(first.IsFailure);
        Assert.Equal("dummy-release-product-1", first.Value);
        Assert.Equal(first.Value, again.Value);
        Assert.Equal("dummy-release-product-2", other.Value);
    }

    // A reconcile asks about a release by its product: it finds the one that was made, and reports a missing reference otherwise,
    // the same answers the HTTP providers give for a 200 and a 404 on that route.
    [Fact]
    public async Task ReconcileReleaseAsync_ReleaseWasMade_ReturnsItsReference()
    {
        await Gateway().RequestReleaseAsync("product-1", 5, CancellationToken.None);

        var result = await Gateway().ReconcileReleaseAsync("product-1", CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal("dummy-release-product-1", result.Value);
    }

    [Fact]
    public async Task ReconcileReleaseAsync_NoReleaseWasMade_ReturnsTheReferenceNotFound()
    {
        var result = await Gateway().ReconcileReleaseAsync("product-1", CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("NotFound", result.Error!.Key);
        Assert.Equal("WarehouseReference", result.Error.ParameterName);
        Assert.Equal(HttpStatusCode.NotFound, result.Error.StatusCode);
    }

    [Fact]
    public async Task ReconcileReleaseAsync_ReleaseOfAnotherConnection_IsNotSeen()
    {
        await Gateway(WarehouseConnectionId.New()).RequestReleaseAsync("product-1", 5, CancellationToken.None);

        var result = await Gateway().ReconcileReleaseAsync("product-1", CancellationToken.None);

        Assert.True(result.IsFailure);
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

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => Gateway().RequestReleaseAsync("product-1", 1, cancelled.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => Gateway().ReconcileReleaseAsync("product-1", cancelled.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => Gateway().PushStockAsync([new("product-1", 1)], cancelled.Token));
    }
}
