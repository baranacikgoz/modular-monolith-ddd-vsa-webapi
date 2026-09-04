using Common.Domain.Devices;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.Devices;

namespace Notifications.Infrastructure.Persistence.EntityConfigurations;

internal sealed class DeviceRegistrationConfiguration : AuditableEntityConfiguration<DeviceRegistration, DeviceRegistrationId>
{
    public override void Configure(EntityTypeBuilder<DeviceRegistration> builder)
    {
        base.Configure(builder);

        builder
            .Property(r => r.UserId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired();

        builder
            .Property(r => r.ClientId)
            .HasMaxLength(DeviceSessionConstants.ClientIdMaxLength)
            .IsRequired();

        builder
            .Property(r => r.SessionId)
            .HasMaxLength(DeviceSessionConstants.SessionIdMaxLength)
            .IsRequired();

        builder
            .Property(r => r.DeviceName)
            .HasMaxLength(DeviceSessionConstants.DeviceNameMaxLength);

        builder
            .Property(r => r.PushToken)
            .HasMaxLength(DeviceSessionConstants.PushTokenMaxLength);

        // One row per (user, device, client app); a re-login rebinds it instead of inserting a sibling.
        builder
            .HasIndex(r => new { r.UserId, r.DeviceId, r.ClientId })
            .IsUnique();

        // Session-scoped lookups: push-token update for the current session, deactivation on revoke.
        builder
            .HasIndex(r => new { r.UserId, r.SessionId });
    }
}
