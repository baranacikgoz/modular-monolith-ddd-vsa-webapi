using Common.Caching;
using Common.Core.Contracts.Results;
using IdentityAndAuth.Extensions;
using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.Features.Identity.Infrastructure;

internal class UserService(
    UserManager<ApplicationUser> userManager,
    ICacheService cacheService,
    IdentityDbContext identityDbContext
    ) : IUserService
{
    public async Task<Result<ApplicationUser>> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Error.NotFound(nameof(ApplicationUser), phoneNumber);
        }

        var user = await userManager
                            .Users
                            .SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);

        if (user is null)
        {
            return Error.NotFound(nameof(ApplicationUser), phoneNumber);
        }

        return user;
    }

    public async Task<Result> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        var identityResult = await userManager.UpdateAsync(user);

        return identityResult.ToResult();
    }

    public Task<List<string>> GetRoles(Guid userId, CancellationToken cancellationToken = default)
        => cacheService.GetOrSetAsync(
            CacheKeyForRoles(userId),
            () => identityDbContext
                    .UserRoles
                    .Where(ur => ur.UserId == userId)
                    .Join(identityDbContext.Roles,
                          ur => ur.RoleId,
                          r => r.Id,
                          (ur, r) => r.Name!)
                    .ToListAsync(cancellationToken),
                    slidingExpiration: TimeSpan.FromDays(7), cancellationToken: cancellationToken);

    public async Task<bool> HasPermissionAsync(Guid userId, string permissionName, CancellationToken cancellationToken = default)
    {
        var userRoles = await GetRoles(userId, cancellationToken);

        foreach (var role in userRoles)
        {
            switch (role)
            {
                case CustomRoles.SystemAdmin:
                    if (CustomPermissions.SystemAdmin.Contains(permissionName))
                    {
                        return true;
                    }
                    break;
                case CustomRoles.Basic:
                    if (CustomPermissions.Basic.Contains(permissionName))
                    {
                        return true;
                    }
                    break;
            }
        }

        return false;
    }
    public Task InvalidateRolesCacheAsync(Guid userId, CancellationToken cancellationToken = default)
        => cacheService.RemoveAsync(CacheKeyForRoles(userId), cancellationToken);

    private static string CacheKeyForRoles(Guid userId) => $"roles:{userId}";
}
