using Common.Infrastructure.Persistence.EntityConfigurations;
using Inventory.Domain.WarehouseConnections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal sealed class WarehouseConnectionConfiguration : AuditableEntityConfiguration<WarehouseConnection, WarehouseConnectionId>
{
    public override void Configure(EntityTypeBuilder<WarehouseConnection> builder)
    {
        base.Configure(builder);

        builder
            .Property(c => c.StoreId)
            .IsRequired();

        builder
            .Property(c => c.Provider)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(c => c.BaseUrl)
            .HasConversion<string>()
            .HasMaxLength(Constants.BaseUrlMaxLength)
            .IsRequired();

        builder
            .Property(c => c.Status)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(c => c.EncryptedApiCredential)
            .IsRequired();

        builder
            .Property(c => c.EncryptedWebhookSecret)
            .IsRequired();

        // A store has one active connection: the create endpoint checks first, this index is the race-safe backstop.
        builder
            .HasIndex(c => c.StoreId)
            .IsUnique()
            .HasFilter($"\"Status\" = '{nameof(WarehouseConnectionStatus.Active)}'");

        // The list: one store's connections, newest first.
        builder.HasIndex(c => new { c.StoreId, c.CreatedOn });
    }
}
