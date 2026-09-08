using Common.Domain.Events;

namespace Inventory.Domain.StockReservations.DomainEvents.v1;

public sealed record V1StockReservationReservedDomainEvent(
    StockReservationId StockReservationId,
    DefaultIdType ProductId,
    int Quantity,
    DateTimeOffset ReservationDeadline) : DomainEvent;
