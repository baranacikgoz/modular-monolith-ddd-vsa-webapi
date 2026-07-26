using Microsoft.AspNetCore.Authentication;

namespace IAM.Infrastructure.Auth.ApiKey;

internal static class Setup
{
    internal static AuthenticationBuilder AddApiKeyScheme(this AuthenticationBuilder builder)
        => builder.AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(ApiKeyDefaults.Scheme, null);
}
