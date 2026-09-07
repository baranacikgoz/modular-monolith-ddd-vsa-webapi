using System.Net.Http.Json;
using Common.Application.Options;
using IAM.Application.Keycloak;
using IAM.Infrastructure.Keycloak.Representations;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Keycloak;

internal sealed class ServiceAccountTokenProvider(
    HttpClient httpClient,
    ServiceAccountTokenCache cache,
    IOptions<KeycloakOptions> keycloakOptionsProvider,
    TimeProvider timeProvider
) : IServiceAccountTokenProvider
{
    public async Task<string> GetAccessTokenAsync(bool forceRefresh, CancellationToken cancellationToken)
    {
        var options = keycloakOptionsProvider.Value;
        var skew = TimeSpan.FromSeconds(options.ServiceAccountTokenRefreshSkewSeconds);

        if (!forceRefresh && cache.IsValid(timeProvider.GetUtcNow(), skew))
        {
            return cache.AccessToken!;
        }

        await cache.WaitAsync(cancellationToken);
        try
        {
            // Another thread in this process may have refreshed while we waited for the lock.
            if (!forceRefresh && cache.IsValid(timeProvider.GetUtcNow(), skew))
            {
                return cache.AccessToken!;
            }

            using var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = options.ResourceClientId,
                ["client_secret"] = options.ResourceClientSecret
            });

            using var response = await httpClient.PostAsync(
                new Uri(KeycloakPaths.Token(options.Realm), UriKind.Relative), content, cancellationToken);
            response.EnsureSuccessStatusCode();

            var token = await response.Content.ReadFromJsonAsync<TokenResponseRepresentation>(cancellationToken)
                        ?? throw new HttpRequestException("Keycloak returned an empty client_credentials response.");

            cache.Set(token.AccessToken, timeProvider.GetUtcNow().AddSeconds(token.ExpiresIn));
            return token.AccessToken;
        }
        finally
        {
            cache.Release();
        }
    }
}
