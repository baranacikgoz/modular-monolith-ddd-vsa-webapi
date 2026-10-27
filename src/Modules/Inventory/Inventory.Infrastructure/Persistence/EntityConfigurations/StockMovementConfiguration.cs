using Common.Infrastructure.Persistence.EntityConfigurations;
using Inventory.Domain.StockLevels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal sealed class StockMovementConfiguration : AuditableEntityConfiguration<StockMovement, StockMovementId>
{
    public override void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        base.Configure(builder);

        // The ledger list: one stock level's movements, newest first.
        builder.HasIndex(m => new { m.StockLevelId, m.CreatedOn });

        builder
            .Property(m => m.Type)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(m => m.Quantity)
            .IsRequired();

        builder
            .Property(m => m.QuantityOnHandAfter)
            .IsRequired();

        builder
            .Property(m => m.Reason)
            .HasMaxLength(Constants.ReasonMaxLength);
    }
}
