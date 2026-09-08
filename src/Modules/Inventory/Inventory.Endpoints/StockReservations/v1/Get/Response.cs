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
}
