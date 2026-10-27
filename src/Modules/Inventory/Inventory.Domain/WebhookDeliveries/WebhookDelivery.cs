using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;

namespace Inventory.Domain.WebhookDeliveries;

public readonly record struct WebhookDeliveryId(DefaultIdType Value) : IStronglyTypedId
{
    public static WebhookDeliveryId New()
    {
        return new WebhookDeliveryId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out WebhookDeliveryId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

/// <summary>
///     The record that one webhook delivery of a warehouse connection was applied, keyed by the delivery id the warehouse
///     system assigned. The warehouse delivers at least once, so the same delivery id can arrive again, even at the same
///     moment; a unique index on (connection, delivery id) makes the insert the arbiter, and it is saved in the same
///     transaction as the delivery's effect so the two stand or fall together. No events, no behavior: bookkeeping, hence
///     the entity rather than an aggregate.
/// </summary>
public class WebhookDelivery : AuditableEntity<WebhookDeliveryId>
{
    private WebhookDelivery() : base(new WebhookDeliveryId(DefaultIdType.Empty))
    {
    } // EF Core needs a parameterless ctor

    public WarehouseConnectionId ConnectionId { get; private set; }

    /// <summary>The warehouse system's own id of the delivery, unique per connection.</summary>
    public string DeliveryId { get; private set; } = string.Empty;

    public StockReservationId ReservationId { get; private set; }
    public DateTimeOffset ReceivedOn { get; private set; }

    public static WebhookDelivery Receive(
        WarehouseConnectionId connectionId, string deliveryId, StockReservationId reservationId, DateTimeOffset receivedOn)
    {
        return new WebhookDelivery
        {
            Id = WebhookDeliveryId.New(),
            ConnectionId = connectionId,
            DeliveryId = deliveryId,
            ReservationId = reservationId,
            ReceivedOn = receivedOn
        };
    }
}
