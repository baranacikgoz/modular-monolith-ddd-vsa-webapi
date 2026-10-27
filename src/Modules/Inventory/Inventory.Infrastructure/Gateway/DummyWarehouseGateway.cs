using System.Collections.Concurrent;
using Common.Domain.ResultMonad;
using Inventory.Application.Gateway;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;

namespace Inventory.Infrastructure.Gateway;

/// <summary>
///     What the in-memory warehouse of every <see cref="WarehouseProvider.Dummy" /> connection remembers, shared by all the
///     gateways the factory builds (it builds a new one per call) and gone with the process.
/// </summary>
internal sealed class DummyWarehouseState
{
    private readonly ConcurrentDictionary<(WarehouseConnectionId Connection, StockReservationId Reservation), string> _releases = new();
    private readonly ConcurrentDictionary<(WarehouseConnectionId Connection, string Product), int> _stock = new();
    private int _pushes;

    /// <summary>The reference of the release made for <paramref name="reservation" />; the same reservation always gets the same reference.</summary>
    public string RecordRelease(WarehouseConnectionId connection, StockReservationId reservation)
    {
        return _releases.GetOrAdd((connection, reservation), key => $"dummy-release-{key.Reservation}");
    }

    public bool TryGetRelease(WarehouseConnectionId connection, StockReservationId reservation, out string reference)
    {
        var found = _releases.TryGetValue((connection, reservation), out var stored);
        reference = stored ?? string.Empty;
        return found;
    }

    /// <summary>Sets the absolute quantities and returns the reference of the batch, numbered in the order batches arrive.</summary>
    public string RecordStock(WarehouseConnectionId connection, IReadOnlyCollection<WarehouseStockQuantity> quantities)
    {
        foreach (var quantity in quantities)
        {
            _stock[(connection, quantity.ProductReference)] = quantity.Quantity;
        }

        return $"dummy-stock-{Interlocked.Increment(ref _pushes)}";
    }

    /// <summary>The quantity the dummy warehouse of <paramref name="connection" /> shows for the product, or null when it was never pushed.</summary>
    public int? StockOf(WarehouseConnectionId connection, string productReference)
    {
        return _stock.TryGetValue((connection, productReference), out var quantity) ? quantity : null;
    }
}

/// <summary>
///     The in-memory provider: no network, no base address, no credential. It keeps the same outcome rules as the HTTP
///     providers so the code above the gateway behaves the same against it: a release is answered with a reference and is
///     idempotent per reservation (two reservations of one product are two releases), the reconcile call finds the release of
///     a reservation that was made and reports the missing reference otherwise
///     (<see cref="WarehouseGatewayErrors.ReferenceNotFound" />), and a stock push sets the absolute quantities and answers
///     the reference of the batch. It never times out, so it never throws <see cref="WarehouseOutcomeUnknownException" />.
/// </summary>
internal sealed class DummyWarehouseGateway(WarehouseConnectionId connectionId, DummyWarehouseState state) : IWarehouseGateway
{
    public Task<Result<string>> RequestReleaseAsync(
        StockReservationId reservationId, string productReference, int quantity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(Result<string>.Success(state.RecordRelease(connectionId, reservationId)));
    }

    public Task<Result<string>> ReconcileReleaseAsync(StockReservationId reservationId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(state.TryGetRelease(connectionId, reservationId, out var reference)
            ? Result<string>.Success(reference)
            : WarehouseGatewayErrors.ReferenceNotFound);
    }

    public Task<Result<string>> PushStockAsync(IReadOnlyCollection<WarehouseStockQuantity> quantities, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(quantities);

        return Task.FromResult(Result<string>.Success(state.RecordStock(connectionId, quantities)));
    }
}
