using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
    }
}
