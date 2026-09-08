using Inventory.Domain.StockReservations;

namespace Inventory.Endpoints.StockReservations.v1.Reserve;

public sealed record Response
{
    public required StockReservationId Id { get; init; }
}
