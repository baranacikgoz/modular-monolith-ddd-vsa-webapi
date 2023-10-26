using System.Security.Claims;
using Common.Core.Auth;
using IdentityAndAuth.Auth;

namespace IdentityAndAuth.Features.Users.Services;

internal sealed class CurrentUser : ICurrentUser
{
    public CurrentUser(ClaimsPrincipal? user, string? ipAddress)
    {
        IsAuthenticated = user?.Identity?.IsAuthenticated ?? false;
        UserName = user?.Identity?.Name;
        IpAddress = ipAddress;

        IdAsString = IsAuthenticated
                ? user?.FindFirstValue(ClaimTypes.NameIdentifier)
                : string.Empty;

        Id = string.IsNullOrEmpty(IdAsString)
                ? Guid.Empty
                : Guid.Parse(IdAsString);

        FullName = IsAuthenticated
                ? user?.FindFirstValue(CustomClaims.Fullname)
                : null;

        PhoneNumber = IsAuthenticated
                ? user?.FindFirstValue(ClaimTypes.MobilePhone)
                : null;
    }
    private bool IsAuthenticated { get; }

    public string? UserName { get; }

    public string? IpAddress { get; }

    public Guid Id { get; }
    public string? IdAsString { get; }

    public string? FullName { get; }
    public string? PhoneNumber { get; }
}
