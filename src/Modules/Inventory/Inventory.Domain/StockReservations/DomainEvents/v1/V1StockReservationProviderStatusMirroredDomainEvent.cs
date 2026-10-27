using Common.Domain.Events;

namespace Inventory.Domain.StockReservations.DomainEvents.v1;

/// <summary>
///     A read-only reservation took over the status its provider reported. Only the mirror changed: the reservation's own
///     status and every other field stayed.
/// </summary>
public sealed record V1StockReservationProviderStatusMirroredDomainEvent(
    StockReservationId StockReservationId,
    V1StockReservationProviderStatusMirroredDomainEvent.ProviderStatusSnapshot ProviderStatus,
    DateTimeOffset MirroredAt) : DomainEvent
{
    public enum ProviderStatusSnapshot
    {
        Active,
        Committed,
        Released,
        Expired,
        RequiresReconciliation,
        Unknown
    }
}

internal static class V1StockReservationProviderStatusMirroredDomainEventExtensions
{
    public static V1StockReservationProviderStatusMirroredDomainEvent.ProviderStatusSnapshot ToMirroredSnapshot(
        this ReservationStatus status)
    {
        return status switch
        {
            ReservationStatus.Active => V1StockReservationProviderStatusMirroredDomainEvent.ProviderStatusSnapshot.Active,
            ReservationStatus.Committed => V1StockReservationProviderStatusMirroredDomainEvent.ProviderStatusSnapshot.Committed,
            ReservationStatus.Released => V1StockReservationProviderStatusMirroredDomainEvent.ProviderStatusSnapshot.Released,
            ReservationStatus.Expired => V1StockReservationProviderStatusMirroredDomainEvent.ProviderStatusSnapshot.Expired,
            ReservationStatus.RequiresReconciliation =>
                V1StockReservationProviderStatusMirroredDomainEvent.ProviderStatusSnapshot.RequiresReconciliation,
            ReservationStatus.Unknown => V1StockReservationProviderStatusMirroredDomainEvent.ProviderStatusSnapshot.Unknown,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
