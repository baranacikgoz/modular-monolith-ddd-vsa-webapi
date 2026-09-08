using Common.Infrastructure.Persistence.EntityConfigurations;
using Inventory.Domain.StockReservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal sealed class StockReservationConfiguration : AuditableEntityConfiguration<StockReservation, StockReservationId>
{
    public override void Configure(EntityTypeBuilder<StockReservation> builder)
    {
        base.Configure(builder);

        builder
            .Property(r => r.ProductId)
            .IsRequired();

        builder
            .Property(r => r.Quantity)
            .IsRequired();

        builder
            .Property(r => r.Status)
            .IsRequired();

        builder
            .Property(r => r.ReservationDeadline)
            .IsRequired();

        builder
            .Property(r => r.ProviderReference)
            .IsRequired(false);

        builder
            .Property(r => r.LastReleaseAttemptReference)
            .IsRequired(false);

        // Available-quantity aggregation (GetStockLevelRequestHandler) filters by ProductId + Status.
        builder
            .HasIndex(r => new { r.ProductId, r.Status });
    }
}
