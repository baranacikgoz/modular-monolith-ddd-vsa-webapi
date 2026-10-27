using Common.Domain.Events;

namespace Inventory.Domain.StockReservations.DomainEvents.v1;

/// <summary>
///     The warehouse system reported a status in a word this system has no mapping for. The reservation's own status did not
///     change; it is read-only from here on. <see cref="RawStatus" /> is the provider's word, trimmed and cut to
///     <see cref="Constants.ProviderStatusMaxLength" />: the audit trail is the only place it is kept.
/// </summary>
public sealed record V1StockReservationProviderStatusUnrecognizedDomainEvent(
    StockReservationId StockReservationId,
    string RawStatus,
    DateTimeOffset ReceivedAt) : DomainEvent;
