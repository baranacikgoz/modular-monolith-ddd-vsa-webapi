using System.Net.Http.Headers;
using System.Net.Http.Json;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using IAM.Application.Keycloak;
using IAM.Infrastructure.Keycloak;
using IAM.Infrastructure.Keycloak.Representations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IAM.Tests;

/// <summary>Reads a custom attribute straight from Keycloak's Admin REST API, for attributes <see cref="KeycloakUser" /> does not expose.</summary>
internal static class KeycloakUserAttributeReader
{
    internal static async Task<string?> ReadAsync(
        IntegrationTestWebAppFactory factory, IServiceProvider services, ApplicationUserId userId, string attribute)
    {
        var tokenProvider = services.GetRequiredService<IServiceAccountTokenProvider>();
        var realm = services.GetRequiredService<IOptions<KeycloakOptions>>().Value.Realm;

        using var http = new HttpClient { BaseAddress = new Uri(factory.KeycloakBaseAddress + "/") };
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer", await tokenProvider.GetAccessTokenAsync(false, CancellationToken.None));

        var user = await http.GetFromJsonAsync<UserRepresentation>(
            new Uri(KeycloakPaths.Admin(realm, $"users/{userId}"), UriKind.Relative));

        return user?.Attributes?.GetValueOrDefault(attribute)?.FirstOrDefault();
    }

    internal static Task<string?> PhoneNumberVerifiedAsync(
        IntegrationTestWebAppFactory factory, IServiceProvider services, ApplicationUserId userId)
    {
        return ReadAsync(factory, services, userId, UserAttributes.PhoneNumberVerified);
    }
}
