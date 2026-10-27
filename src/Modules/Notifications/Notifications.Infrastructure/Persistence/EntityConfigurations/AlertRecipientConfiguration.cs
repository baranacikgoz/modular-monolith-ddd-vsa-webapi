using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.Alerts;

namespace Notifications.Infrastructure.Persistence.EntityConfigurations;

internal sealed class AlertRecipientConfiguration : AuditableEntityConfiguration<AlertRecipient>
{
    public override void Configure(EntityTypeBuilder<AlertRecipient> builder)
    {
        base.Configure(builder);

        builder.ToTable("AlertRecipients");

        builder.HasKey(r => r.UserId);

        builder
            .Property(r => r.UserId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .ValueGeneratedNever();

        builder
            .Property(r => r.Email)
            .HasMaxLength(AlertRecipient.EmailMaxLength)
            .IsRequired();

        builder
            .Property(r => r.LastSeenOn)
            .IsRequired();
    }
}
