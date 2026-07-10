using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using IAM.Domain.Identity.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAM.Infrastructure.Persistence.EntityConfigurations;

internal sealed class RefreshTokenConfig : AuditableEntityConfiguration<RefreshToken, RefreshTokenId>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        builder.ToTable("RefreshTokens");

        builder
            .Property(rt => rt.SessionId)
            .HasConversion<StronglyTypedIdValueConverter<SessionId>>();

        builder
            .Property(rt => rt.ReplacedByTokenId)
            .HasConversion(
                id => id == null ? (Guid?)null : id.Value.Value,
                value => value == null ? (RefreshTokenId?)null : new RefreshTokenId(value.Value));

        builder
            .Property(rt => rt.TokenHash)
            .IsRequired();

        // Unique: the refresh endpoint looks up by hash with SingleOrDefaultAsync — a duplicate hash
        // would make that throw for every future request against it, 500ing the flow permanently.
        builder
            .HasIndex(rt => rt.TokenHash)
            .IsUnique();
    }
}
