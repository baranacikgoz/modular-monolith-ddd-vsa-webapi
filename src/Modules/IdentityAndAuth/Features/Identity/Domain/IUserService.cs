using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain;

internal interface IUserService
{
    Task<Result<ApplicationUser>> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    Task<List<string>> GetRoles(ApplicationUserId userId, CancellationToken cancellationToken = default);
    Task<bool> HasPermissionAsync(ApplicationUserId userId, string permissionName, CancellationToken cancellationToken = default);
    Task InvalidateRolesCacheAsync(ApplicationUserId userId, CancellationToken cancellationToken = default);
}
