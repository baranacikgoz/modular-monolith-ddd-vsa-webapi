using System.Security.Claims;
using Common.Application.Auth;
using Common.Domain.StronglyTypedIds;

namespace IAM.Infrastructure.Identity.Services;

internal sealed class CurrentUser(
    ClaimsPrincipal? user,
    string? ipAddress
    ) : ICurrentUser
{
    public string? IpAddress => ipAddress;
    private bool IsAuthenticated => user?.Identity?.IsAuthenticated ?? false;
    public string? IdAsString
        => IsAuthenticated
            ? user?.FindFirstValue(ClaimTypes.NameIdentifier)
            : string.Empty;
    public ApplicationUserId Id
        => new(string.IsNullOrEmpty(IdAsString)
            ? Guid.Empty
            : Guid.Parse(IdAsString));
}
