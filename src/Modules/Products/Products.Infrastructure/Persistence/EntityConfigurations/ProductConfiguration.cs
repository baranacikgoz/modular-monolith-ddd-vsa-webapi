using Common.Application.Persistence.EntityConfigurations;
using Common.Application.ValueConverters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Infrastructure.Persistence.EntityConfigurations;

internal sealed class ProductConfiguration : AuditableEntityConfiguration<Product, ProductId>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder
            .HasIndex(sp => new { sp.StoreId, sp.ProductTemplateId })
            .IsUnique();

        builder
            .Property(sp => sp.StoreId)
            .HasConversion<StronglyTypedIdValueConverter<StoreId>>()
            .IsRequired();

        builder
            .HasOne(sp => sp.Store)
            .WithMany(s => s.Products)
            .HasForeignKey(sp => sp.StoreId);

        builder
            .Property(sp => sp.ProductTemplateId)
            .HasConversion<StronglyTypedIdValueConverter<ProductTemplateId>>()
            .IsRequired();

        builder
            .HasOne(sp => sp.ProductTemplate)
            .WithMany(p => p.Products)
            .HasForeignKey(sp => sp.ProductTemplateId);

        builder
            .Property(sp => sp.Quantity)
            .IsRequired();

        builder
            .Property(sp => sp.Price)
            .IsRequired();
    }
}
