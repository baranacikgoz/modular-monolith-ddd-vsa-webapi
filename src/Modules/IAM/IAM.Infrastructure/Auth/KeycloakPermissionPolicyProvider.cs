using System.Collections.Concurrent;
using Common.Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Auth;

/// <summary>
///     Turns a policy name of the form <c>resource#scope</c> (see <see cref="KeycloakPermission" />) into a
///     <see cref="KeycloakPermissionRequirement" /> on the fly, so endpoints never register policies by hand.
///     Every other name, plus the default and fallback policies, is handled by ASP.NET Core's default provider,
///     which keeps <c>RequireAuthorization()</c> and test overrides working unchanged.
/// </summary>
internal sealed class KeycloakPermissionPolicyProvider(IOptions<AuthorizationOptions> options) : IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider _fallback = new(options);
    private readonly ConcurrentDictionary<string, AuthorizationPolicy> _policies = new(StringComparer.Ordinal);

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return _fallback.GetDefaultPolicyAsync();
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return _fallback.GetFallbackPolicyAsync();
    }

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!KeycloakPermission.TryParse(policyName, out var permission))
        {
            return _fallback.GetPolicyAsync(policyName);
        }

        var policy = _policies.GetOrAdd(policyName, _ => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddRequirements(new KeycloakPermissionRequirement(permission))
            .Build());

        return Task.FromResult<AuthorizationPolicy?>(policy);
    }
}
