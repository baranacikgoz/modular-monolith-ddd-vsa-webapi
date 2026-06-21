using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlTypes;
using Products.Domain.Stores;

namespace Products.Infrastructure.Persistence.EntityConfigurations;

internal sealed class StoreConfiguration : AuditableEntityConfiguration<Store, StoreId>
{
    public override void Configure(EntityTypeBuilder<Store> builder)
    {
        base.Configure(builder);

        builder
            .Property(s => s.OwnerId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired();

        builder
            .HasIndex(s => s.OwnerId)
            .IsUnique();

        builder
            .Property(s => s.Name)
            .HasMaxLength(Constants.NameMaxLength)
            .IsRequired();

        builder
            .Property(s => s.Description)
            .HasMaxLength(Constants.DescriptionMaxLength)
            .IsRequired();

        builder
            .Property(s => s.Address)
            .HasMaxLength(Constants.AddressMaxLength)
            .IsRequired();

        builder
            .Navigation(s => s.Products)
            .HasField("_products")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // Per-row authored language feeding the prose layer of the two-layer vector. Stamped by interceptor.
        builder
            .Property(s => s.Language)
            .HasColumnName(FullTextSearchOptions.LanguageColumn)
            .HasDefaultValue(FullTextSearchOptions.UniversalConfig)
            .IsRequired();

        // Two-layer generated tsvector: universal simple_unaccent over Name (A) + Address (B) + per-row Description (C).
        // IMMUTABLE wrapper fts_store is created in the migration.
        builder
            .Property<NpgsqlTsVector>(FullTextSearchOptions.SearchVectorColumn)
            .HasComputedColumnSql(@"fts_store(""Language"", ""Name"", ""Address"", ""Description"")", stored: true)
            .HasColumnName(FullTextSearchOptions.SearchVectorColumn);

        builder
            .HasIndex(FullTextSearchOptions.SearchVectorColumn)
            .HasMethod(FullTextSearchOptions.IndexMethod);
    }
}
