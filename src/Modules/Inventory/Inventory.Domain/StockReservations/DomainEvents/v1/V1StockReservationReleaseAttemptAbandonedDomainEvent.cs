using Common.Domain.Events;

namespace Inventory.Domain.StockReservations.DomainEvents.v1;

/// <summary>
///     Only safe after a DEFINITE rejection from the external warehouse system - never after an ambiguous
///     timeout, where the attempt marker must stay in place. Mirrors DepositPayment.AbandonItemOutcomeAttempt.
/// </summary>
public sealed record V1StockReservationReleaseAttemptAbandonedDomainEvent(
    StockReservationId StockReservationId,
    DateTimeOffset AbandonedAt) : DomainEvent;
