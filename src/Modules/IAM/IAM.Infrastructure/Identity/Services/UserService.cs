using System.Security.Claims;
using Common.Application.Caching;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using IAM.Application.Auth;
using IAM.Application.Extensions;
using IAM.Application.Identity.Services;
using IAM.Infrastructure.Auth;
using IAM.Infrastructure.Persistence;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.Identity.Services;

internal class UserService(
    UserManager<ApplicationUser> userManager,
    ICacheService cacheService,
    IAMDbContext dbContext
    ) : IUserService
{
    public async Task<Result<ApplicationUser>> GetByIdAsync(ApplicationUserId userId, CancellationToken cancellationToken)
        => await Result<ApplicationUser>.CreateAsync(
            taskToAwaitValue: async () => await userManager
                                            .Users
                                            .SingleOrDefaultAsync(x => x.Id == userId, cancellationToken),
            errorIfValueNull: Error.NotFound(nameof(ApplicationUser), userId));

    public async Task<Result<ApplicationUser>> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
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

    public async Task<Result<ApplicationUser>> GetByClaimsPrincipalAsync(ClaimsPrincipal principal, CancellationToken cancellationToken)
    {
        var userId = principal.GetUserId();

        return await GetByIdAsync(userId, cancellationToken);
    }

    public async Task<Result> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var identityResult = await userManager.UpdateAsync(user);

        return identityResult.ToResult();
    }

    public Task<List<string>> GetRoles(ApplicationUserId userId, CancellationToken cancellationToken)
        => cacheService.GetOrCreateAsync(
            key: CacheKeyForRoles(userId),
            factory: async ct => await dbContext
                    .UserRoles
                    .Where(ur => ur.UserId == userId)
                    .Join(dbContext.Roles,
                          ur => ur.RoleId,
                          r => r.Id,
                          (ur, r) => r.Name!)
                    .ToListAsync(ct),
            absoluteExpirationRelativeToNow: TimeSpan.FromDays(7),
            cancellationToken: cancellationToken);

    public async Task<DateTimeOffset?> GetRefreshTokenExpiresAt(ApplicationUserId userId, string refreshToken, CancellationToken cancellationToken)
    {
        var refreshTokenExpiresAt = await userManager
                                        .Users
                                        .Where(u => u.Id == userId && u.RefreshToken == refreshToken)
                                        .Select(u => u.RefreshTokenExpiresAt)
                                        .FirstOrDefaultAsync(cancellationToken);

        return refreshTokenExpiresAt == default ? null : refreshTokenExpiresAt;
    }

    public async Task<bool> HasPermissionAsync(ApplicationUserId userId, string permissionName, CancellationToken cancellationToken)
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
                default:
                    break;
            }
        }

        return false;
    }
    public Task InvalidateRolesCacheAsync(ApplicationUserId userId, CancellationToken cancellationToken)
        => cacheService.RemoveByKeyAsync(CacheKeyForRoles(userId), cancellationToken);

    private static string CacheKeyForRoles(ApplicationUserId userId) => $"roles:{userId}";

    public async Task<ICollection<ApplicationUserId>> GetSeedUserIdsByRoleId(DefaultIdType roleId, int requestedUserCount, CancellationToken cancellationToken)
    {
        var stronglyTypedRoleId = new ApplicationUserId(roleId);
        return await dbContext
            .Users
            .Join(
                dbContext.UserRoles,
                u => u.Id,
                ur => ur.UserId,
                (u, ur) => new { u, ur.RoleId }
            )
            .Where(uur => uur.RoleId == stronglyTypedRoleId)
            .OrderBy(uur => uur.u.CreatedOn)
            .Select(uur => uur.u.Id)
            .Take(requestedUserCount)
            .ToListAsync(cancellationToken);
    }
}
