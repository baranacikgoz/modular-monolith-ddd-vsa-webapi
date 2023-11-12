using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain;

internal interface IUserService
{
    Task<Result<ApplicationUser>> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    Task<List<string>> GetRoles(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> HasPermissionAsync(Guid userId, string permissionName, CancellationToken cancellationToken = default);
    Task InvalidateRolesCacheAsync(Guid userId, CancellationToken cancellationToken = default);
}
