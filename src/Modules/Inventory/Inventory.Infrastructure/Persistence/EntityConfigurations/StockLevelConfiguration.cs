using Common.Infrastructure.Persistence.EntityConfigurations;
using Inventory.Domain.StockLevels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal sealed class StockLevelConfiguration : AuditableEntityConfiguration<StockLevel, StockLevelId>
{
    public override void Configure(EntityTypeBuilder<StockLevel> builder)
    {
        base.Configure(builder);

        builder
            .Property(s => s.ProductId)
            .IsRequired();

        // Same technique as Store.OwnerId (Products module): app-level idempotency check in the
        // IntegrationEvent consumer is the fast path, this unique index is the race-safe DB backstop.
        builder
            .HasIndex(s => s.ProductId)
            .IsUnique();

        builder
            .Property(s => s.QuantityOnHand)
            .IsRequired();
    }
}
