using Common.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlTypes;
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

        builder
            .Property<NpgsqlTsVector>(FullTextSearch.SearchVectorColumnName)
            .IsGeneratedTsVectorColumn(FullTextSearch.Language, nameof(ProductTemplate.Brand), nameof(ProductTemplate.Model), nameof(ProductTemplate.Color))
            .HasColumnName(FullTextSearch.SearchVectorColumnName);

        builder
            .HasIndex(FullTextSearch.SearchVectorColumnName)
            .HasMethod(FullTextSearch.GinIndexMethod);
    }
}
