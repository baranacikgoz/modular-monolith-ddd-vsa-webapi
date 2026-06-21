using Common.Application.Options;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlTypes;
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

        // Per-row authored language feeding the prose layer of the two-layer vector. Stamped by interceptor.
        builder
            .Property(sp => sp.Language)
            .HasColumnName(FullTextSearchOptions.LanguageColumn)
            .HasDefaultValue(FullTextSearchOptions.UniversalConfig)
            .IsRequired();

        // Two-layer generated tsvector: universal simple_unaccent over Name (A) + per-row-language Description (B).
        // IMMUTABLE wrapper fts_product is created in the migration; Npgsql's IsGeneratedTsVectorColumn cannot express it.
        builder
            .Property<NpgsqlTsVector>(FullTextSearchOptions.SearchVectorColumn)
            .HasComputedColumnSql(@"fts_product(""Language"", ""Name"", ""Description"")", stored: true)
            .HasColumnName(FullTextSearchOptions.SearchVectorColumn);

        builder
            .HasIndex(FullTextSearchOptions.SearchVectorColumn)
            .HasMethod(FullTextSearchOptions.IndexMethod);
    }
}
