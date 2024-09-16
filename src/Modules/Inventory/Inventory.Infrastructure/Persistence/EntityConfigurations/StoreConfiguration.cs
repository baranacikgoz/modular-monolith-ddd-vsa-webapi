using Ardalis.Specification;
using System.Linq.Expressions;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Inventory.Domain.Stores;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

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
            .Property(s => s.LogoUrl)
            .HasConversion<UriToStringConverter>()
            .HasMaxLength(Constants.LogoUrlMaxLength)
            .IsRequired(false);
    }
}
