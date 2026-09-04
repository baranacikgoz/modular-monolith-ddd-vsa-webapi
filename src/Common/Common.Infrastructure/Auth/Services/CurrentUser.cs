using System.Security.Claims;
using Common.Application.Auth;
using Common.Domain.StronglyTypedIds;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.Auth.Services;

// Reads HttpContext.User fresh on every property access instead of snapshotting it in the
// constructor: this type is Scoped, and ASP.NET Core only finalizes HttpContext.User (the
// authenticated principal) after AuthenticationMiddleware runs. Anything that resolves this
// service earlier in the same scope would otherwise permanently cache an empty/anonymous
// principal for the rest of the request. Reading lazily removes that resolution-order footgun.
internal sealed class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private ClaimsPrincipal? Principal => httpContextAccessor.HttpContext?.User;

    private bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

    public ApplicationUserId Id
    {
        get
        {
            var idAsString = IdAsString;
            return new ApplicationUserId(
                !string.IsNullOrEmpty(idAsString) && DefaultIdType.TryParse(idAsString, out var parsed)
                    ? parsed
                    : DefaultIdType.Empty);
        }
    }

    public string? IdAsString => IsAuthenticated ? Principal?.FindFirstValue(JwtClaimNames.Subject) : string.Empty;

    // S2365: recomputing (not memoizing) on every access is deliberate, see class remark above.
#pragma warning disable S2365
    public ICollection<string> Roles => IsAuthenticated
        ? Principal?.FindAll(JwtClaimNames.Roles).Select(x => x.Value).ToList() ?? []
        : [];
#pragma warning restore S2365

    public string? SessionId => IsAuthenticated ? Principal?.FindFirstValue(JwtClaimNames.SessionId) : null;
}
