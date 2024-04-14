
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
            .IsRequired();

        builder
            .HasMany(a => a.Products)
            .WithOne(p => p.Store)
            .HasForeignKey(p => p.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
