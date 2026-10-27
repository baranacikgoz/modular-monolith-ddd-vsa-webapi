using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Inventory.Domain.StockLevels;
using Inventory.Domain.StockPushes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal sealed class StockPushDirtyItemConfiguration : AuditableEntityConfiguration<StockPushDirtyItem>
{
    public override void Configure(EntityTypeBuilder<StockPushDirtyItem> builder)
    {
        base.Configure(builder);

        // StockPushMarks' raw SQL names this table and its columns; keep them in step.
        builder.ToTable("StockPushDirtyItems");

        // The key is what makes the mark an upsert.
        builder.HasKey(d => d.StockLevelId);

        builder
            .Property(d => d.StockLevelId)
            .HasConversion<StronglyTypedIdValueConverter<StockLevelId>>()
            .IsRequired();

        builder.Property(d => d.StoreId).IsRequired();
        builder.Property(d => d.ProductId).IsRequired();
        builder.Property(d => d.MarkVersion).IsRequired();
        builder.Property(d => d.MarkedOn).IsRequired();

        // The mark statements leave PushAttempts out of their INSERT, so the column default is what a new row starts with.
        builder.Property(d => d.PushAttempts).HasDefaultValue(0).IsRequired();
        builder.Property(d => d.LastErrorKey).HasMaxLength(StockPushDirtyItem.LastErrorKeyMaxLength);

        // A push reads the oldest rows of one store first; the sweep reads rows by age.
        builder.HasIndex(d => new { d.StoreId, d.MarkedOn });
        builder.HasIndex(d => d.MarkedOn);
    }
}
