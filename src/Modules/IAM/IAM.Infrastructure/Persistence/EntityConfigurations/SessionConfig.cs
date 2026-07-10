using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using IAM.Domain.Identity;
using IAM.Domain.Identity.Sessions;
using Microsoft.EntityFrameworkCore;
using SessionConstants = IAM.Domain.Identity.Sessions.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAM.Infrastructure.Persistence.EntityConfigurations;

internal sealed class SessionConfig : AuditableEntityConfiguration<Session, SessionId>
{
    public override void Configure(EntityTypeBuilder<Session> builder)
    {
        base.Configure(builder);

        builder.ToTable("Sessions");

        builder
            .Property(s => s.UserId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>();

        builder
            .Property(s => s.ClientId)
            .HasMaxLength(SessionConstants.ClientIdMaxLength)
            .IsRequired();

        builder
            .Property(s => s.DeviceName)
            .HasMaxLength(SessionConstants.DeviceNameMaxLength);

        builder
            .Property(s => s.RevokedReason)
            .HasConversion<string>();

        builder
            .HasIndex(s => new { s.UserId, s.DeviceId, s.ClientId })
            .IsUnique();

        builder
            .HasOne<ApplicationUser>()
            .WithMany(u => u.Sessions)
            .HasForeignKey(s => s.UserId);

        builder
            .HasMany(s => s.RefreshTokens)
            .WithOne()
            .HasForeignKey(rt => rt.SessionId);

        builder
            .Navigation(s => s.RefreshTokens)
            .HasField("_refreshTokens")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
