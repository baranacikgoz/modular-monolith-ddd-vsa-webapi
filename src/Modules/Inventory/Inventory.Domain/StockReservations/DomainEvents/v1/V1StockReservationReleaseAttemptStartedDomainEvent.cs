using Common.Domain.Events;

namespace Inventory.Domain.StockReservations.DomainEvents.v1;

/// <summary>
///     Recorded BEFORE calling the external warehouse system to release the hold - callers must save this
///     and only then make the call, mirroring DepositPayment.BeginItemOutcomeAttempt.
/// </summary>
public sealed record V1StockReservationReleaseAttemptStartedDomainEvent(
    StockReservationId StockReservationId,
    string AttemptReference,
    DateTimeOffset StartedAt) : DomainEvent;
