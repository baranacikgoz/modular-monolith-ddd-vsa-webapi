using Microsoft.AspNetCore.Authorization;

namespace IdentityAndAuth.Auth;

internal class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        var defaultPolicy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();

        return Task.FromResult(defaultPolicy);
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        // Implement a fallback policy if needed, or return null.
        return Task.FromResult<AuthorizationPolicy?>(null);
    }

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(CustomClaims.Permission, StringComparison.OrdinalIgnoreCase))
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermissionRequirement(policyName));
            return Task.FromResult<AuthorizationPolicy?>(policy.Build());
        }

        return Task.FromResult<AuthorizationPolicy?>(null);
    }
}

internal class PermissionRequirement : IAuthorizationRequirement
{
    public string PermissionName { get; private set; }

    public PermissionRequirement(string permissionName)
    {
        PermissionName = permissionName;
    }
}
