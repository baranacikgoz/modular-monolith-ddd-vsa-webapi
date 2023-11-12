using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityAndAuth.Persistence.EntityConfigurations;

internal class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");

        builder
            .Property(u => u.FirstName)
            .HasMaxLength(ApplicationUserConstants.FirstNameMaxLength)
            .IsRequired();

        builder
            .Property(u => u.LastName)
            .HasMaxLength(ApplicationUserConstants.LastNameMaxLength)
            .IsRequired();

        builder
            .Property(u => u.NationalIdentityNumber)
            .HasMaxLength(ApplicationUserConstants.NationalIdentityNumberLength)
            .IsRequired();

        builder
            .Property(u => u.BirthDate)
            .IsRequired();

        builder
            .Property(u => u.ImageUrl)
            .IsRequired(false)
            .HasConversion(
                uri => uri == null ? null : uri.ToString(),
                str => str == null ? null : new Uri(str));

        builder
            .Property(u => u.RefreshToken)
            .IsRequired()
            .HasMaxLength(ApplicationUserConstants.RefreshTokenMaxLength);

        builder
            .Property(u => u.RefreshTokenExpiresAt)
            .IsRequired();

    }
}

internal class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder) =>
        builder
            .ToTable("Roles");
}

internal class IdentityRoleClaimConfig : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder) =>
        builder
            .ToTable("RoleClaims");
}

internal class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder) =>
        builder
            .ToTable("UserRoles");
}

internal class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder) =>
        builder
            .ToTable("UserClaims");
}

internal class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder) =>
        builder
            .ToTable("UserLogins");
}

internal class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder) =>
        builder
            .ToTable("UserTokens");
}
