using System.Collections.Frozen;

namespace Inventory.Domain.StockReservations;

/// <summary>
///     The one place that decides which status changes of a reservation are legal. The commands of
///     <see cref="StockReservation" /> ask it before moving, and a unit test holds every status change a command makes to it.
///     A status maps to the statuses it may move to, so a status missing from the table (Released, Expired,
///     RequiresReconciliation, Unknown) is final, and a move from a status to itself is not a move at all: the commands answer
///     that case as an idempotent no-op before they ask.
/// </summary>
public static class ReservationStatusTransitions
{
    private static readonly FrozenDictionary<ReservationStatus, FrozenSet<ReservationStatus>> Allowed =
        new Dictionary<ReservationStatus, FrozenSet<ReservationStatus>>
        {
            [ReservationStatus.Active] = new[]
            {
                ReservationStatus.Committed,
                ReservationStatus.Released,
                ReservationStatus.Expired
            }.ToFrozenSet(),

            // A second, different confirmation of a committed reservation is not applied: it is flagged for a person.
            [ReservationStatus.Committed] = new[] { ReservationStatus.RequiresReconciliation }.ToFrozenSet()
        }.ToFrozenDictionary();

    public static bool CanTransition(ReservationStatus from, ReservationStatus to)
    {
        return Allowed.TryGetValue(from, out var targets) && targets.Contains(to);
    }
}
