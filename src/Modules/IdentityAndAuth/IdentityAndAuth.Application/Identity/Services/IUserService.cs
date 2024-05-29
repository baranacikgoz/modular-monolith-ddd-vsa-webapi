using System.Security.Claims;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using IdentityAndAuth.Domain.Identity;

namespace IdentityAndAuth.Application.Identity.Services;

public interface IUserService
{
    Task<Result<ApplicationUser>> GetByIdAsync(ApplicationUserId userId, CancellationToken cancellationToken);
    Task<Result<ApplicationUser>> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<Result<ApplicationUser>> GetByClaimsPrincipalAsync(ClaimsPrincipal principal, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken);
    Task<List<string>> GetRoles(ApplicationUserId userId, CancellationToken cancellationToken);
    Task<bool> HasPermissionAsync(ApplicationUserId userId, string permissionName, CancellationToken cancellationToken);
    Task InvalidateRolesCacheAsync(ApplicationUserId userId, CancellationToken cancellationToken);
    Task<DateTime?> GetRefreshTokenExpiresAt(ApplicationUserId userId, string refreshToken, CancellationToken cancellationToken);
    Task<ICollection<ApplicationUserId>> GetSeedUserIdsByRoleId(Guid roleId, int requestedUserCount, CancellationToken cancellationToken);

}