using Common.Domain.Events;

namespace Inventory.Domain.StockReservations.DomainEvents.v1;

/// <summary>
///     A delivery that carries the provider's own timestamp was applied, and its stamp is the latest the reservation has seen.
///     Raised after the event of whatever the delivery changed.
/// </summary>
public sealed record V1StockReservationProviderEventTimeRecordedDomainEvent(
    StockReservationId StockReservationId,
    DateTimeOffset? PreviousOccurredOn,
    DateTimeOffset OccurredOn,
    DateTimeOffset RecordedAt) : DomainEvent;
