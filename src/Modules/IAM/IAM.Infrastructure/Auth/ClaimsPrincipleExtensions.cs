using System.Security.Claims;
using Common.Domain.StronglyTypedIds;

namespace IAM.Infrastructure.Auth;

internal static class ClaimsPrincipalExtensions
{
    public static ApplicationUserId GetUserId(this ClaimsPrincipal principal)
        => new ApplicationUserId(new Guid(principal.GetUserIdAsString() ?? string.Empty));
    public static string? GetUserIdAsString(this ClaimsPrincipal principal)
       => principal.FindFirstValue(ClaimTypes.NameIdentifier);
    private static string? FindFirstValue(this ClaimsPrincipal principal, string claimType) =>
        principal is null
            ? throw new ArgumentNullException(nameof(principal))
            : principal.FindFirst(claimType)?.Value;
}
