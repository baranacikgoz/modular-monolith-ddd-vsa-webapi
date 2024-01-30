
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Features.Stores.Domain;

namespace Sales.Persistence.EntityConfigurations;

internal class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder
            .Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => new StoreId(value));

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
