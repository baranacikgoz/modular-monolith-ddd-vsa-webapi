using Common.Domain.Events;

namespace Inventory.Domain.StockReservations.DomainEvents.v1;

/// <summary>
///     Two different provider references both claim to confirm the same reservation - never silently
///     swallowed, this needs manual reconciliation. Mirrors DepositPayment's double-charge detection.
/// </summary>
public sealed record V1StockReservationCommitConflictDetectedDomainEvent(
    StockReservationId StockReservationId,
    DefaultIdType ProductId,
    string ExistingProviderReference,
    string ConflictingProviderReference,
    DateTimeOffset DetectedAt) : DomainEvent;
