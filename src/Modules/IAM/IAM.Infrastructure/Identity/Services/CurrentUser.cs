using System.Security.Claims;
using Common.Application.Auth;
using Common.Domain.StronglyTypedIds;
using IAM.Application.Auth;

namespace IAM.Infrastructure.Identity.Services;

internal sealed class CurrentUser : ICurrentUser
{
    public CurrentUser(ClaimsPrincipal? user, string? ipAddress)
    {
        var isAuthenticated = user?.Identity?.IsAuthenticated ?? false;

        IpAddress = ipAddress;
        IdAsString = isAuthenticated
            ? user?.FindFirstValue(ClaimTypes.NameIdentifier)
            : string.Empty;
        Id = new(string.IsNullOrEmpty(IdAsString)
            ? DefaultIdType.Empty
            : DefaultIdType.Parse(IdAsString));
        Roles = isAuthenticated
            ? user?.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList() ?? []
            : [];
    }

    public string? IpAddress { get; }

    public ApplicationUserId Id { get; }

    public string? IdAsString { get; }

    public ICollection<string> Roles { get; }

    public bool HasPermission(string permission)
    {
        foreach (var role in Roles)
        {
            switch (role)
            {
                case CustomRoles.SystemAdmin:
                    if (CustomPermissions.SystemAdmin.Contains(permission))
                    {
                        return true;
                    }
                    break;
                case CustomRoles.Basic:
                    if (CustomPermissions.Basic.Contains(permission))
                    {
                        return true;
                    }
                    break;

                // Populate as more roles are added

                default:
                    break;
            }
        }

        return false;
    }
}
