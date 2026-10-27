using System.Net;
using System.Text;
using Common.Application.Caching;
using Common.Application.Options;
using IAM.Application.Keycloak;
using IAM.Infrastructure.Keycloak;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Tests;

#pragma warning disable CA1707 // Remove the underscores from member name

/// <summary>
///     The role graph read behind <see cref="IKeycloakAdminClient.GetRolesContainingAsync" /> against a counting stub of
///     the Admin REST API and a real in-memory FusionCache: no Keycloak container is needed, so this class stays out of the
///     integration collection.
/// </summary>
public sealed class KeycloakAdminClientRoleGraphTests : IDisposable
{
    private readonly FusionCache _cache = new(new FusionCacheOptions());
    private readonly CountingKeycloakHandler _keycloak = new();
    private readonly HttpClient _http;

    public KeycloakAdminClientRoleGraphTests()
    {
        _http = new HttpClient(_keycloak) { BaseAddress = new Uri("http://keycloak.test/") };
    }

    public void Dispose()
    {
        _http.Dispose();
        _keycloak.Dispose();
        _cache.Dispose();
    }

    [Fact]
    public async Task GetRolesContainingAsync_SecondCallInsideTheWindow_DoesNotAskKeycloakAgain()
    {
        var client = CreateClient();

        var first = await client.GetRolesContainingAsync("leaf", CancellationToken.None);
        var callsAfterFirst = _keycloak.RequestCount;
        var second = await client.GetRolesContainingAsync("leaf", CancellationToken.None);

        Assert.Equal(["middle", "top"], first);
        Assert.Equal(first, second);
        Assert.True(callsAfterFirst > 0);
        Assert.Equal(callsAfterFirst, _keycloak.RequestCount);
    }

    [Fact]
    public async Task GetRolesContainingAsync_AnotherRoleInsideTheWindow_ReusesTheSameGraph()
    {
        var client = CreateClient();

        await client.GetRolesContainingAsync("leaf", CancellationToken.None);
        var callsAfterFirst = _keycloak.RequestCount;
        var other = await client.GetRolesContainingAsync("middle", CancellationToken.None);

        Assert.Equal(["top"], other);
        Assert.Equal(callsAfterFirst, _keycloak.RequestCount);
    }

    [Fact]
    public async Task GetRolesContainingAsync_EntryEvicted_ReadsKeycloakAgainAndSeesTheChange()
    {
        var client = CreateClient();
        var before = await client.GetRolesContainingAsync("leaf", CancellationToken.None);
        var callsAfterFirst = _keycloak.RequestCount;
        _keycloak.Composites["extra"] = ["leaf"];

        var stale = await client.GetRolesContainingAsync("leaf", CancellationToken.None);
        await _cache.RemoveAsync(CacheKeys.For.KeycloakRoleGraph);
        var fresh = await client.GetRolesContainingAsync("leaf", CancellationToken.None);

        Assert.Equal(before, stale);
        Assert.Equal(["extra", "middle", "top"], fresh);
        Assert.True(_keycloak.RequestCount > callsAfterFirst);
    }

    [Fact]
    public async Task GetRolesContainingAsync_KeycloakFailsOnTheFirstRead_DoesNotCacheTheFailure()
    {
        var client = CreateClient();
        _keycloak.FailRoleListing = true;

        await Assert.ThrowsAsync<HttpRequestException>(() => client.GetRolesContainingAsync("leaf", CancellationToken.None));
        _keycloak.FailRoleListing = false;
        var roles = await client.GetRolesContainingAsync("leaf", CancellationToken.None);

        Assert.Equal(["middle", "top"], roles);
    }

    private KeycloakAdminClient CreateClient()
    {
        var keycloakOptions = Options.Create(new KeycloakOptions
        {
            BaseUrl = "http://keycloak.test",
            Realm = "modular-monolith",
            ResourceClientId = "backend-api",
            ResourceClientSecret = "secret",
            TrustedLoginClientId = "backend-trusted-login",
            TrustedLoginClientSecret = "secret",
            RequireHttpsMetadata = false,
            DecisionCacheMaxDurationSeconds = 300,
            ServiceAccountTokenRefreshSkewSeconds = 30,
            AttemptTimeoutSeconds = 5,
            TotalRequestTimeoutSeconds = 15,
            UserLookupConcurrency = 8,
            UserDisplayCacheDurationSeconds = 300,
            UserDisplayCacheDistributedDurationSeconds = 86400,
            UserDisplayNegativeCacheDurationSeconds = 300,
            RoleGraphCacheSeconds = 60
        });

        return new KeycloakAdminClient(
            _http,
            new FixedTokenProvider(),
            keycloakOptions,
            Options.Create(new IdentitySchemeOptions { Scheme = IdentityScheme.PhoneNumber }),
            _cache,
            NullLogger<KeycloakAdminClient>.Instance);
    }

    private sealed class FixedTokenProvider : IServiceAccountTokenProvider
    {
        public Task<string> GetAccessTokenAsync(bool forceRefresh, CancellationToken cancellationToken)
        {
            return Task.FromResult("token");
        }
    }

    /// <summary>
    ///     Answers the two Admin REST reads the role graph needs and counts every request. The graph it serves:
    ///     <c>top</c> includes <c>middle</c> and <c>leaf</c>, <c>middle</c> includes <c>leaf</c>.
    /// </summary>
    private sealed class CountingKeycloakHandler : HttpMessageHandler
    {
        private int _requestCount;

        public int RequestCount => Volatile.Read(ref _requestCount);

        public bool FailRoleListing { get; set; }

        public Dictionary<string, string[]> Composites { get; } = new(StringComparer.Ordinal)
        {
            ["top"] = ["middle", "leaf"],
            ["middle"] = ["leaf"]
        };

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Interlocked.Increment(ref _requestCount);
            var path = request.RequestUri!.AbsolutePath;

            if (path.EndsWith("/roles", StringComparison.Ordinal))
            {
                return Task.FromResult(FailRoleListing ? new HttpResponseMessage(HttpStatusCode.InternalServerError) : Json(RoleListing()));
            }

            const string compositesSuffix = "/composites";
            if (path.EndsWith(compositesSuffix, StringComparison.Ordinal))
            {
                var name = path[(path.LastIndexOf("/roles/", StringComparison.Ordinal) + "/roles/".Length)..^compositesSuffix.Length];
                return Task.FromResult(Json(Role(Composites.GetValueOrDefault(name) ?? [], composite: false)));
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        private string RoleListing()
        {
            var names = Composites.Keys.Concat(Composites.Values.SelectMany(v => v)).Distinct(StringComparer.Ordinal);
            return "[" + string.Join(",", names.Select(n => RoleJson(n, Composites.ContainsKey(n)))) + "]";
        }

        private static string Role(string[] names, bool composite)
        {
            return "[" + string.Join(",", names.Select(n => RoleJson(n, composite))) + "]";
        }

        private static string RoleJson(string name, bool composite)
        {
            return $$"""{"name":"{{name}}","composite":{{(composite ? "true" : "false")}},"clientRole":false}""";
        }

        private static HttpResponseMessage Json(string body)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };
        }
    }
}

#pragma warning restore CA1707
