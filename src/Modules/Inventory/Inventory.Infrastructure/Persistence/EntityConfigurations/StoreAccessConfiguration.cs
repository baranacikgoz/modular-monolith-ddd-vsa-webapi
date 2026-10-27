using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Inventory.Application.StoreAccesses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal sealed class StoreAccessConfiguration : AuditableEntityConfiguration<StoreAccess>
{
    public override void Configure(EntityTypeBuilder<StoreAccess> builder)
    {
        base.Configure(builder);

        builder.HasKey(a => a.StoreId);
        builder.Property(a => a.StoreId).ValueGeneratedNever();

        builder
            .Property(a => a.OwnerId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired();

        builder
            .Property(a => a.IsActive)
            .IsRequired();

        // The access check reads "the active stores of this owner".
        builder.HasIndex(a => new { a.OwnerId, a.IsActive });
    }
}
