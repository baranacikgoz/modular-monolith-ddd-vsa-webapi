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

        // The ledger list: one stock level's movements, newest first, by the sequence the stock level handed out. Unique, so
        // the sequence is also the proof that no two rows of one ledger share a place.
        builder
            .HasIndex(m => new { m.StockLevelId, m.Sequence })
            .IsUnique();

        builder
            .Property(m => m.Sequence)
            .IsRequired();

        // A reservation takes its quantity out of the stock once: the consumer checks the ledger under the row lock, this
        // index is the race-safe backstop.
        builder
            .HasIndex(m => m.ReservationId)
            .IsUnique()
            .HasFilter($"\"Type\" = '{nameof(StockMovementType.ReservationCommitted)}'");

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
