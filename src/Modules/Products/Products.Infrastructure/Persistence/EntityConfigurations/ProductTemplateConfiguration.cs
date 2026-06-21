using Common.Application.Options;
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
            .Navigation(pt => pt.Products)
            .HasField("_products")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // Universal layer only — proper-noun fields indexed language-neutral so brands are findable by every locale.
        builder
            .Property<NpgsqlTsVector>(FullTextSearchOptions.SearchVectorColumn)
            .HasComputedColumnSql(
                @"setweight(to_tsvector('simple_unaccent', coalesce(""Brand"",'') || ' ' || coalesce(""Model"",'') || ' ' || coalesce(""Color"",'')), 'A')",
                stored: true)
            .HasColumnName(FullTextSearchOptions.SearchVectorColumn);

        builder
            .HasIndex(FullTextSearchOptions.SearchVectorColumn)
            .HasMethod(FullTextSearchOptions.IndexMethod);
    }
}
