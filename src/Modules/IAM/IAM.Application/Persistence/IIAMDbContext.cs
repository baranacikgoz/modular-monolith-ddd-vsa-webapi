using Common.Application.Persistence;
using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IAM.Application.Persistence;

#pragma warning disable S101 // Types should be named in PascalCase
public interface IIAMDbContext : IDbContext
#pragma warning restore S101 // Types should be named in PascalCase
{
    DbSet<ApplicationUser> Users { get; }
    DbSet<IdentityRole<ApplicationUserId>> Roles { get; }
    DbSet<IdentityUserClaim<ApplicationUserId>> UserClaims { get; }
    DbSet<IdentityUserRole<ApplicationUserId>> UserRoles { get; }
    DbSet<IdentityUserLogin<ApplicationUserId>> UserLogins { get; }
    DbSet<IdentityRoleClaim<ApplicationUserId>> RoleClaims { get; }
    DbSet<IdentityUserToken<ApplicationUserId>> UserTokens { get; }

    DbSet<ApplicationUser> ApplicationUsers => Users; // Required for DeleteEntityCommandSourceGenerator
}
