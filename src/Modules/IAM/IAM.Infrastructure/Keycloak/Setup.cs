using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using IAM.Application.Keycloak;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Keycloak;

public static class Setup
{
    public static IServiceCollection AddKeycloakInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ServiceAccountTokenCache>();

        services.AddKeycloakHttpClient<IServiceAccountTokenProvider, ServiceAccountTokenProvider>();
        services.AddKeycloakHttpClient<IKeycloakTokenClient, KeycloakTokenClient>();
        services.AddKeycloakHttpClient<IKeycloakPermissionClient, KeycloakPermissionClient>();
        services.AddKeycloakHttpClient<IKeycloakAdminClient, KeycloakAdminClient>();

        return services;
    }

    private static void AddKeycloakHttpClient<TClient, TImplementation>(this IServiceCollection services)
        where TClient : class
        where TImplementation : class, TClient
    {
        services.AddResilientHttpClient<TClient, TImplementation>(
            (sp, httpClient) =>
            {
                var options = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                // Trailing slash is REQUIRED: HttpClient drops the last BaseAddress segment when
                // combining with a relative URI otherwise.
#pragma warning disable S1075
                httpClient.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + '/');
#pragma warning restore S1075
            },
            (resilience, sp) =>
            {
                // Bound per-attempt and total time so a slow Keycloak degrades into a fast 502, not a hung
                // login. Retry and circuit breaker come from the shared Resiliency pipeline.
                var options = sp.GetRequiredService<IOptions<KeycloakOptions>>().Value;
                resilience.AttemptTimeout.Timeout = TimeSpan.FromSeconds(options.AttemptTimeoutSeconds);
                resilience.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(options.TotalRequestTimeoutSeconds);
            });
    }
}
