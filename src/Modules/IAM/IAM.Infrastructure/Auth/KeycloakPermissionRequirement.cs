using Common.Application.Auth;
using Microsoft.AspNetCore.Authorization;

namespace IAM.Infrastructure.Auth;

internal sealed class KeycloakPermissionRequirement(KeycloakPermission permission) : IAuthorizationRequirement
{
    public KeycloakPermission Permission { get; } = permission;
}
