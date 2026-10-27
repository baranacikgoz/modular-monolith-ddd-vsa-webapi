using Common.Infrastructure.Persistence.EntityConfigurations;
using Inventory.Application.ProductSummaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal sealed class ProductSummaryConfiguration : AuditableEntityConfiguration<ProductSummary>
{
    public override void Configure(EntityTypeBuilder<ProductSummary> builder)
    {
        base.Configure(builder);

        builder.HasKey(p => p.ProductId);
        builder.Property(p => p.ProductId).ValueGeneratedNever();

        builder
            .Property(p => p.StoreId)
            .IsRequired();

        builder
            .Property(p => p.Name)
            .HasMaxLength(ProductSummary.NameMaxLength)
            .IsRequired();
    }
}
