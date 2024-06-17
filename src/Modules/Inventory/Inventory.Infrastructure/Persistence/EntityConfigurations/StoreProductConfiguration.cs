using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Inventory.Domain.Products;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal class StoreProductConfiguration : AuditableEntityConfiguration<StoreProduct, StoreProductId>
{
    public override void Configure(EntityTypeBuilder<StoreProduct> builder)
    {
        base.Configure(builder);

        builder
            .HasIndex(sp => new { sp.StoreId, sp.ProductId })
            .IsUnique();

        builder
            .Property(sp => sp.StoreId)
            .HasConversion<StronglyTypedIdValueConverter<StoreId>>()
            .IsRequired();

        builder
            .HasOne(sp => sp.Store)
            .WithMany(s => s.StoreProducts)
            .HasForeignKey(sp => sp.StoreId);

        builder
            .Property(sp => sp.ProductId)
            .HasConversion<StronglyTypedIdValueConverter<ProductId>>()
            .IsRequired();

        builder
            .HasOne(sp => sp.Product)
            .WithMany(p => p.StoreProducts)
            .HasForeignKey(sp => sp.ProductId);

        builder
            .Property(sp => sp.Quantity)
            .IsRequired();

        builder
            .Property(sp => sp.Price)
            .IsRequired();
    }
}
