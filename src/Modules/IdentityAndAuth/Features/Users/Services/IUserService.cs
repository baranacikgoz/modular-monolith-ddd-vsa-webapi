using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Identity;

namespace IdentityAndAuth.Features.Users.Services;

public interface IUserService
{
    Task<Result<ApplicationUser>> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    Task<List<string>> GetRoles(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> HasPermissionAsync(Guid userId, string permissionName, CancellationToken cancellationToken = default);
    Task InvalidateRolesCacheAsync(Guid userId, CancellationToken cancellationToken = default);
}
