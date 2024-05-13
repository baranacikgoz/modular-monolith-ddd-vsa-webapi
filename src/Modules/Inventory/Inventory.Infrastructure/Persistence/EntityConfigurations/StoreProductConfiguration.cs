using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Inventory.Domain.Products;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal class StoreProductConfiguration : AuditableEntityConfiguration<StoreProduct, StoreProductId>
{
    public override void Configure(EntityTypeBuilder<StoreProduct> builder)
    {
        base.Configure(builder);

        builder
            .HasIndex(a => new { a.StoreId, a.ProductId })
            .IsUnique();

        builder
            .Property(a => a.StoreId)
            .HasConversion<StronglyTypedIdValueConverter<StoreId>>()
            .IsRequired();

        builder
            .Property(a => a.ProductId)
            .HasConversion<StronglyTypedIdValueConverter<ProductId>>()
            .IsRequired();

        builder
            .Property(a => a.Quantity)
            .IsRequired();

        builder
            .Property(a => a.Price)
            .IsRequired();
    }
}
