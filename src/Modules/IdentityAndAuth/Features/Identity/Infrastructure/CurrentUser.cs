using System.Security.Claims;
using Common.Core.Auth;
using IdentityAndAuth.Features.Auth.Domain;

namespace IdentityAndAuth.Features.Identity.Infrastructure;

internal sealed class CurrentUser(
    ClaimsPrincipal? user,
    string? ipAddress
    ) : ICurrentUser
{
    public string? IpAddress => ipAddress;
    private bool IsAuthenticated => user?.Identity?.IsAuthenticated ?? false;
    public string? UserName => user?.Identity?.Name;
    public string? IdAsString
        => IsAuthenticated
            ? user?.FindFirstValue(ClaimTypes.NameIdentifier)
            : string.Empty;
    public Guid Id
        => string.IsNullOrEmpty(IdAsString)
            ? Guid.Empty
            : Guid.Parse(IdAsString);
    public string? FullName
        => IsAuthenticated
            ? user?.FindFirstValue(CustomClaims.Fullname)
            : null;
    public string? PhoneNumber
        => IsAuthenticated
            ? user?.FindFirstValue(ClaimTypes.MobilePhone)
            : null;
}
