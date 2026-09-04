using System.Collections.Concurrent;
using Common.Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Common.Tests;

/// <summary>
///     Resolves <c>resource#scope</c> policy names (see <see cref="KeycloakPermission" />) when the IAM module,
///     which owns the real Keycloak-backed provider, is not part of the test host. The policy only requires an
///     authenticated user; <see cref="AllowAllAuthorizationHandler" /> then satisfies it for slice tests.
/// </summary>
public sealed class TestPermissionPolicyProvider(IOptions<AuthorizationOptions> options) : IAuthorizationPolicyProvider
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
        if (!KeycloakPermission.TryParse(policyName, out _))
        {
            return _fallback.GetPolicyAsync(policyName);
        }

        var policy = _policies.GetOrAdd(policyName, _ => new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(TestAuthHandler.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build());

        return Task.FromResult<AuthorizationPolicy?>(policy);
    }
}
