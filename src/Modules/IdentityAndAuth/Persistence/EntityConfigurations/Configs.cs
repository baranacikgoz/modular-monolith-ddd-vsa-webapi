using Common.Core.Contracts.Identity;
using Common.Persistence;
using Common.Persistence.EventSourcing;
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
            .Property(u => u.Id)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>();

        builder
            .Property(u => u.Name)
            .HasMaxLength(Constants.NameMaxLength)
            .IsRequired();

        builder
            .Property(u => u.LastName)
            .HasMaxLength(Constants.LastNameMaxLength)
            .IsRequired();

        builder
            .Property(u => u.NationalIdentityNumber)
            .HasMaxLength(Constants.NationalIdentityNumberLength)
            .IsRequired();

        builder
            .HasIndex(u => u.NationalIdentityNumber)
            .IsUnique();

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
            .HasMaxLength(Constants.RefreshTokenMaxLength);

        builder
            .Property(u => u.RefreshTokenExpiresAt)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.CreatedOn)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.CreatedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.LastModifiedOn)
            .IsRequired(false);

        builder
            .Property(storeEvent => storeEvent.LastModifiedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired(false);
    }
}

internal class ApplicationRoleConfig : IEntityTypeConfiguration<IdentityRole<ApplicationUserId>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<ApplicationUserId>> builder) =>
        builder
            .ToTable("Roles")
            .Property(u => u.Id)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>();
}

internal class IdentityRoleClaimConfig : IEntityTypeConfiguration<IdentityRoleClaim<ApplicationUserId>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<ApplicationUserId>> builder) =>
        builder
            .ToTable("RoleClaims")
            .Property(u => u.RoleId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>();
}

internal class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<ApplicationUserId>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<ApplicationUserId>> builder) =>
        builder
            .ToTable("UserRoles")
            .Property(u => u.UserId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>();
}

internal class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<ApplicationUserId>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<ApplicationUserId>> builder) =>
        builder
            .ToTable("UserClaims")
            .Property(u => u.UserId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>();
}

internal class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<ApplicationUserId>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<ApplicationUserId>> builder) =>
        builder
            .ToTable("UserLogins")
            .Property(u => u.UserId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>();
}

internal class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<ApplicationUserId>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<ApplicationUserId>> builder) =>
        builder
            .ToTable("UserTokens")
            .Property(u => u.UserId)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>();
}
