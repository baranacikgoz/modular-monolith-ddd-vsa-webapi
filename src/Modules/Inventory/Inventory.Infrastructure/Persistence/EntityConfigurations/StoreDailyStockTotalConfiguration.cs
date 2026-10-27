using Common.Infrastructure.Persistence.EntityConfigurations;
using Inventory.Domain.StockCounters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal sealed class StoreDailyStockTotalConfiguration : AuditableEntityConfiguration<StoreDailyStockTotal>
{
    public override void Configure(EntityTypeBuilder<StoreDailyStockTotal> builder)
    {
        base.Configure(builder);

        // StockCounterWriter's raw SQL names this table and its columns; keep them in step.
        builder.ToTable("StoreDailyStockTotals");

        builder.HasKey(t => new { t.StoreId, t.Day });

        builder
            .Property(t => t.CommittedReservations)
            .IsRequired();

        builder
            .Property(t => t.CommittedUnits)
            .IsRequired();
    }
}
