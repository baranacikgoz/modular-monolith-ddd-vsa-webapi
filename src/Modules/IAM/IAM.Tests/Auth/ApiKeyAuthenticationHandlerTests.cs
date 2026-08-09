using System.Net;
using System.Text.Json;
using Common.Application.Auth;
using Common.Application.Localization.Resources;
using Common.Application.Options;
using Common.Infrastructure.Auth;
using IAM.Application.Persistence;
using IAM.Infrastructure.Auth;
using IAM.Infrastructure.Auth.ApiKey;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Xunit;

namespace IAM.Tests.Auth;

// Standalone host, deliberately NOT the shared IntegrationTestWebAppFactory: that factory forces
// TestAuthHandler as the default scheme and registers AllowAllAuthorizationHandler, which succeeds
// every pending requirement (including PermissionRequirement) for any authenticated principal. That's
// correct for the rest of the suite (auth is a given, tests focus on business logic) but it makes the
// 401/403 distinction this feature depends on unobservable. This host wires the real
// AddAuthInfrastructure pipeline (MultiAuth forwarding, PermissionPolicyProvider,
// PermissionAuthorizationHandler, ApiKeyAuthenticationHandler) with no Postgres and no bypass.
public sealed class ApiKeyAuthenticationHandlerTests : IAsyncLifetime
{
    private static readonly string GrantedPermission =
        CustomPermission.NameFor(CustomActions.Manage, CustomResources.Hangfire);

    private IHost _host = null!;
    private HttpClient _client = null!;

    public async ValueTask InitializeAsync()
    {
        _host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder.UseTestServer();

                webBuilder.ConfigureAppConfiguration((_, config) =>
                {
                    config.AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        ["JwtOptions:Secret"] = "test-secret-test-secret-test-secret-32b",
                        ["JwtOptions:Issuer"] = "test-issuer",
                        ["JwtOptions:Audience"] = "test-audience",
                        ["JwtOptions:AccessTokenExpirationInMinutes"] = "15",
                        ["JwtOptions:RefreshTokenExpirationInDays"] = "14",
                        ["JwtOptions:SessionAbsoluteExpirationInDays"] = "90",
                        ["JwtOptions:RefreshTokenReuseGraceWindowInSeconds"] = "30",
                        ["JwtOptions:SessionRevocationCacheDurationInSeconds"] = "30",
                        ["JwtOptions:AllowedClientIds:0"] = "test-app",
                        ["ApiKeysOptions:Keys:0:Name"] = "test-caller-granted",
                        ["ApiKeysOptions:Keys:0:KeyHash"] = ApiKeyHasher.Hash(ValidKey),
                        ["ApiKeysOptions:Keys:0:Permissions:0"] = GrantedPermission,
                        ["ApiKeysOptions:Keys:1:Name"] = "test-caller-ungranted",
                        ["ApiKeysOptions:Keys:1:KeyHash"] = ApiKeyHasher.Hash(UngrantedKey),
                        ["ApiKeysOptions:Keys:1:Permissions:0"] = "Permissions.SomethingElse.Read"
                    });
                });

                webBuilder.ConfigureServices((context, services) =>
                {
                    services.AddSingleton(Substitute.For<IResxLocalizer>());
                    services.AddProblemDetails();

                    services.AddHttpContextAccessor();
                    services.AddCommonAuth();
                    // Unused by these tests (RoleService is never invoked) but AddAuthInfrastructure
                    // registers RoleService, and ServiceProvider validates every registration on build.
                    services.AddSingleton(Substitute.For<IIAMDbContext>());

                    services.AddAuthInfrastructure(context.Configuration);
                    services.Configure<ApiKeysOptions>(context.Configuration.GetSection(nameof(ApiKeysOptions)));

                    services.AddRouting();
                });

                webBuilder.Configure(app =>
                {
                    app.UseRouting();
                    app.UseAuthentication();
                    app.UseAuthorization();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapGet("/probe", (HttpContext ctx) =>
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.NoContent;
                            return Task.CompletedTask;
                        }).RequireAuthorization(GrantedPermission);
                    });
                });
            })
            .StartAsync();

        _client = _host.GetTestClient();
    }

    public async ValueTask DisposeAsync()
    {
        _client.Dispose();
        await _host.StopAsync();
        _host.Dispose();
    }

    private const string ValidKey = "unit-test-raw-api-key-0123456789";
    private const string UngrantedKey = "unit-test-raw-api-key-ungranted-key";

    [Fact]
    public async Task NoCredentials_Returns401()
    {
        var response = await _client.GetAsync(new Uri("/probe", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UnrecognizedApiKey_Returns401WithUnauthorizedErrorKey()
    {
        _client.DefaultRequestHeaders.Add(ApiKeyDefaults.HeaderName, "not-a-real-key");

        var response = await _client.GetAsync(new Uri("/probe", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.Equal(nameof(HttpStatusCode.Unauthorized), await ReadErrorKeyAsync(response));
    }

    [Fact]
    public async Task ValidApiKey_WithGrantedPermission_Returns204()
    {
        _client.DefaultRequestHeaders.Add(ApiKeyDefaults.HeaderName, ValidKey);

        var response = await _client.GetAsync(new Uri("/probe", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task ValidApiKey_WithoutGrantedPermission_Returns403WithForbiddenErrorKey()
    {
        _client.DefaultRequestHeaders.Add(ApiKeyDefaults.HeaderName, UngrantedKey);

        var response = await _client.GetAsync(new Uri("/probe", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        Assert.Equal(nameof(HttpStatusCode.Forbidden), await ReadErrorKeyAsync(response));
    }

    private static async Task<string?> ReadErrorKeyAsync(HttpResponseMessage response)
    {
        var raw = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(raw);
        return doc.RootElement.TryGetProperty("errorKey", out var value) ? value.GetString() : null;
    }
}
