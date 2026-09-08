using Common.Domain.Events;

namespace Inventory.Domain.StockReservations.DomainEvents.v1;

public sealed record V1StockReservationExpiredDomainEvent(
    StockReservationId StockReservationId,
    DefaultIdType ProductId,
    int Quantity,
    DateTimeOffset ExpiredAt) : DomainEvent;
