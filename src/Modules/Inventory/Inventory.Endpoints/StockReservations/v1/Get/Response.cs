using Inventory.Domain.StockReservations;

namespace Inventory.Endpoints.StockReservations.v1.Get;

public sealed record Response
{
    public required StockReservationId Id { get; init; }
    public required DefaultIdType ProductId { get; init; }
    public required int Quantity { get; init; }
    public required ReservationStatus Status { get; init; }
    public required DateTimeOffset ReservationDeadline { get; init; }
    public string? ProviderReference { get; init; }

    /// <summary>True once the provider reported a status this system could not map; the reservation then takes no local command.</summary>
    public required bool IsReadOnly { get; init; }

    /// <summary>The last status the provider reported, kept for a read-only reservation only; null while it is writable.</summary>
    public ReservationStatus? ProviderStatus { get; init; }
}
