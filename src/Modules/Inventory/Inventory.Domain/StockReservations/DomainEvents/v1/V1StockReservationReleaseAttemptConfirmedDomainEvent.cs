using Common.Domain.Events;

namespace Inventory.Domain.StockReservations.DomainEvents.v1;

/// <summary>
///     The warehouse system confirmed the release an attempt marker stood for, on a read-only reservation: the marker is
///     cleared and nothing else of the reservation changes. On a writable reservation the confirmation is the release itself
///     and raises <see cref="V1StockReservationReleasedDomainEvent" /> instead.
/// </summary>
public sealed record V1StockReservationReleaseAttemptConfirmedDomainEvent(
    StockReservationId StockReservationId,
    DateTimeOffset ConfirmedAt) : DomainEvent;
