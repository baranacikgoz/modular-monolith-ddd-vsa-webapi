using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.Products;

namespace Products.Infrastructure.Persistence.EntityConfigurations;

internal sealed class ProductImageConfiguration : AuditableEntityConfiguration<ProductImage, ProductImageId>
{
    public override void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        base.Configure(builder);

        builder.ToTable("ProductImages");

        builder
            .Property(i => i.ProductId)
            .HasConversion<StronglyTypedIdValueConverter<ProductId>>()
            .IsRequired();

        builder
            .Property(i => i.ObjectKey)
            .HasMaxLength(Constants.ImageObjectKeyMaxLength)
            .IsRequired();

        builder
            .Property(i => i.ContentType)
            .HasMaxLength(Constants.ImageContentTypeMaxLength)
            .IsRequired();

        builder
            .Property(i => i.SizeBytes)
            .IsRequired();

        // A content-hash key: the same file is attached to one product at most once.
        builder
            .HasIndex(i => new { i.ProductId, i.ObjectKey })
            .IsUnique();
    }
}
