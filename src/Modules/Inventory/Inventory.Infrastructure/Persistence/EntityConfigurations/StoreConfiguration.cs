using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Inventory.Domain.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal class StoreConfiguration : AuditableEntityConfiguration<Store, StoreId>
{
    public override void Configure(EntityTypeBuilder<Store> builder)
    {
        base.Configure(builder);

        builder
            .Property(a => a.OwnerId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired();

        builder
            .HasMany(a => a.Products)
            .WithOne(p => p.Store)
            .HasForeignKey(p => p.StoreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
