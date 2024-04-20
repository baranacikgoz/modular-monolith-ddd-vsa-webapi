
using Common.Core.Contracts.Identity;
using Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Features.Products.Domain;
using Sales.Features.Stores.Domain;

namespace Sales.Persistence.EntityConfigurations;

internal class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder
            .Property(a => a.Id)
            .HasConversion<StronglyTypedIdValueConverter<StoreId>>()
            .IsRequired();

        builder
            .Property(a => a.OwnerId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired();

        builder
            .HasMany(a => a.Products)
            .WithOne(p => p.Store)
            .HasForeignKey(p => p.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(storeEvent => storeEvent.CreatedOn)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.CreatedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.LastModifiedOn)
            .IsRequired(false);

        builder
            .Property(storeEvent => storeEvent.LastModifiedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired(false);
    }
}
