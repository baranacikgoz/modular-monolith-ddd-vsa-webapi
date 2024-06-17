using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Inventory.Domain.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal class StoreConfiguration : AuditableEntityConfiguration<Store, StoreId>
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
            .HasMany(s => s.Products)
            .WithOne(p => p.Store)
            .HasForeignKey(p => p.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(s => s.Name)
            .HasMaxLength(Domain.Stores.Constants.NameMaxLength)
            .IsRequired();

        builder
            .Property(s => s.Description)
            .HasMaxLength(Domain.Stores.Constants.DescriptionMaxLength)
            .IsRequired();

        builder
            .Property(s => s.LogoUrl)
            .HasConversion<UriToStringConverter>()
            .HasMaxLength(Domain.Stores.Constants.LogoUrlMaxLength)
            .IsRequired(false);
    }
}
