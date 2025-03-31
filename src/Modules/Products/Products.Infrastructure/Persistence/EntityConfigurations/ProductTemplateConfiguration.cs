using Common.Application.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.ProductTemplates;

namespace Products.Infrastructure.Persistence.EntityConfigurations;

internal sealed class ProductTemplateConfiguration : AuditableEntityConfiguration<ProductTemplate, ProductTemplateId>
{
    public override void Configure(EntityTypeBuilder<ProductTemplate> builder)
    {
        base.Configure(builder);

        builder
            .Property(pt => pt.IsActive)
            .IsRequired();

        builder
            .HasIndex(pt => pt.IsActive);

        builder
            .Property(pt => pt.Brand)
            .HasMaxLength(Constants.BrandMaxLength)
            .IsRequired();

        builder
            .Property(pt => pt.Model)
            .HasMaxLength(Constants.ModelMaxLength)
            .IsRequired();

        builder
            .Property(pt => pt.Color)
            .HasMaxLength(Constants.ColorMaxLength)
            .IsRequired();
    }
}
