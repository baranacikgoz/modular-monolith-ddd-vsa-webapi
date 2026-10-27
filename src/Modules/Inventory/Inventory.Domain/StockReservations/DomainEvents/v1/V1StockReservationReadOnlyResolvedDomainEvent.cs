using Common.Domain.Events;

namespace Inventory.Domain.StockReservations.DomainEvents.v1;

/// <summary>
///     A person resolved a read-only reservation: it takes local commands again and was brought to
///     <see cref="ResolvedStatus" />. When that is a move, the event of the command that made it (committed, released or
///     expired) is raised right before this one; when the reservation already had that status, only the flag changed.
/// </summary>
public sealed record V1StockReservationReadOnlyResolvedDomainEvent(
    StockReservationId StockReservationId,
    V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot ProviderStatus,
    V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot ResolvedStatus,
    DateTimeOffset ResolvedAt) : DomainEvent
{
    public enum StatusSnapshot
    {
        Active,
        Committed,
        Released,
        Expired,
        RequiresReconciliation,
        Unknown
    }
}

internal static class V1StockReservationReadOnlyResolvedDomainEventExtensions
{
    public static V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot ToResolvedSnapshot(this ReservationStatus status)
    {
        return status switch
        {
            ReservationStatus.Active => V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot.Active,
            ReservationStatus.Committed => V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot.Committed,
            ReservationStatus.Released => V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot.Released,
            ReservationStatus.Expired => V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot.Expired,
            ReservationStatus.RequiresReconciliation =>
                V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot.RequiresReconciliation,
            ReservationStatus.Unknown => V1StockReservationReadOnlyResolvedDomainEvent.StatusSnapshot.Unknown,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
