namespace Inventory.Endpoints.StockLevels.v1.DailyTotals;

/// <summary>Committed reservations of one store on one business day. A day with none has no row.</summary>
public sealed record Response
{
    public required DateOnly Day { get; init; }
    public required int CommittedReservations { get; init; }
    public required long CommittedUnits { get; init; }
}
