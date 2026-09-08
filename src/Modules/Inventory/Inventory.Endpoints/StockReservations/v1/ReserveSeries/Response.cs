using Inventory.Domain.StockReservations;

namespace Inventory.Endpoints.StockReservations.v1.ReserveSeries;

public sealed record Response
{
    public required ICollection<StockReservationId> Ids { get; init; }
}
