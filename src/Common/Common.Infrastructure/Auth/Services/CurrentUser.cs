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
        _directPermissions = isAuthenticated
            ? user?.FindAll(JwtClaimNames.Permission).Select(x => x.Value).ToHashSet(StringComparer.Ordinal) ?? []
            : [];
        SessionId = isAuthenticated && Guid.TryParse(user?.FindFirstValue(JwtClaimNames.SessionId), out var sid)
            ? sid
            : null;
    }

    private readonly HashSet<string> _directPermissions;

    public ApplicationUserId Id { get; }

    public string? IdAsString { get; }

    public ICollection<string> Roles { get; }

    public Guid? SessionId { get; }

    // Role-derived permissions cover human callers (JWT); direct permission claims cover
    // machine callers (API keys), which have no role to derive permissions from.
    public bool HasPermission(string permission)
        => _directPermissions.Contains(permission)
           || Roles.Any(role => CustomPermissions.ForRole(role).Contains(permission));
}
