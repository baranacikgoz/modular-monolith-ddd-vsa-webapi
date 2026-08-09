using System.Security.Claims;
using Common.Application.Auth;
using Common.Domain.StronglyTypedIds;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Auth.Services;

// Reads HttpContext.User fresh on every property access instead of snapshotting it in the
// constructor: this type is Scoped, and ASP.NET Core only finalizes HttpContext.User (the
// authenticated principal) after AuthenticationMiddleware runs. Anything that resolves this
// service earlier in the same scope (e.g. inside JwtBearerEvents.OnTokenValidated, which fires
// mid-authentication) would otherwise permanently cache an empty/anonymous principal for the rest
// of the request, since a Scoped service is constructed once per scope regardless of when it's
// first resolved. Reading lazily removes that resolution-order footgun entirely.
internal sealed class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private ClaimsPrincipal? Principal => httpContextAccessor.HttpContext?.User;

    private bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

    public ApplicationUserId Id
    {
        get
        {
            var idAsString = IdAsString;
            return new ApplicationUserId(string.IsNullOrEmpty(idAsString)
                ? DefaultIdType.Empty
                : DefaultIdType.Parse(idAsString));
        }
    }

    public string? IdAsString => IsAuthenticated ? Principal?.FindFirstValue(ClaimTypes.NameIdentifier) : string.Empty;

    // S2365: recomputing (not memoizing) on every access is deliberate, see class remark above.
#pragma warning disable S2365
    public ICollection<string> Roles => IsAuthenticated
        ? Principal?.FindAll(JwtClaimNames.Roles).Select(x => x.Value).ToList() ?? []
        : [];
#pragma warning restore S2365

    public Guid? SessionId => IsAuthenticated && Guid.TryParse(Principal?.FindFirstValue(JwtClaimNames.SessionId), out var sid)
        ? sid
        : null;

    // Role-derived permissions cover human callers (JWT); direct permission claims cover
    // machine callers (API keys), which have no role to derive permissions from.
    public bool HasPermission(string permission)
    {
        if (!IsAuthenticated)
        {
            return false;
        }

        return Principal!.FindAll(JwtClaimNames.Permission).Any(c => string.Equals(c.Value, permission, StringComparison.Ordinal))
               || Roles.Any(role => CustomPermissions.ForRole(role).Contains(permission));
    }
}
