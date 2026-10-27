using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WebhookDeliveries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DeliveryConstants = Inventory.Domain.WebhookDeliveries.Constants;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal sealed class WebhookDeliveryConfiguration : AuditableEntityConfiguration<WebhookDelivery, WebhookDeliveryId>
{
    public override void Configure(EntityTypeBuilder<WebhookDelivery> builder)
    {
        base.Configure(builder);

        builder
            .Property(d => d.ConnectionId)
            .HasConversion<StronglyTypedIdValueConverter<WarehouseConnectionId>>()
            .IsRequired();

        builder
            .Property(d => d.DeliveryId)
            .HasMaxLength(DeliveryConstants.DeliveryIdMaxLength)
            .IsRequired();

        builder
            .Property(d => d.ReservationId)
            .HasConversion<StronglyTypedIdValueConverter<StockReservationId>>()
            .IsRequired();

        builder
            .Property(d => d.ReceivedOn)
            .IsRequired();

        // One row per delivery of a connection: the intake checks first, this index is what stays correct when two identical
        // deliveries pass that check together.
        builder
            .HasIndex(d => new { d.ConnectionId, d.DeliveryId })
            .IsUnique();

        // The retention sweep deletes by age.
        builder.HasIndex(d => d.ReceivedOn);
    }
}
