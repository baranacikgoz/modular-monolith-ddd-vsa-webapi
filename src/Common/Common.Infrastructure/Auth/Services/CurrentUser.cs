using System.Security.Claims;
using Common.Application.Auth;
using Common.Domain.StronglyTypedIds;

namespace Common.Infrastructure.Auth.Services;

internal sealed class CurrentUser : ICurrentUser
{
    public CurrentUser(ClaimsPrincipal? user)
    {
        var isAuthenticated = user?.Identity?.IsAuthenticated ?? false;

        IdAsString = isAuthenticated
            ? user?.FindFirstValue(ClaimTypes.NameIdentifier)
            : string.Empty;
        Id = new ApplicationUserId(string.IsNullOrEmpty(IdAsString)
            ? DefaultIdType.Empty
            : DefaultIdType.Parse(IdAsString));
        Roles = isAuthenticated
            ? user?.FindAll(JwtClaimNames.Roles).Select(x => x.Value).ToList() ?? []
            : [];
    }

    public ApplicationUserId Id { get; }

    public string? IdAsString { get; }

    public ICollection<string> Roles { get; }

    public bool HasPermission(string permission)
        => Roles.Any(role => CustomPermissions.ForRole(role).Contains(permission));
}
