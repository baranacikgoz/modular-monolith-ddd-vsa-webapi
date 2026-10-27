using System.Net;
using System.Text;
using Common.Application.Caching;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.IAM;
using IAM.Application.Keycloak;
using IAM.Infrastructure.InterModuleRequestHandlers;
using IAM.Infrastructure.Keycloak;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Tests.InterModuleRequestHandlers;

#pragma warning disable CA1707 // Remove the underscores from member name

/// <summary>
///     What walking a role's members costs Keycloak, against a counting stub of the Admin REST API and a real in-memory
///     FusionCache: no Keycloak container is needed, so this class stays out of the integration collection. The role
///     <c>member</c> has direct holders, the composite <c>lead</c> that includes it has holders of its own, and one user holds
///     both.
/// </summary>
public sealed class GetUsersInRolePageCostTests : IDisposable
{
    private const string Role = "member";
    private const string Composite = "lead";

    private readonly FusionCache _cache = new(new FusionCacheOptions());
    private readonly CountingKeycloakHandler _keycloak = new();
    private readonly HttpClient _http;

    public GetUsersInRolePageCostTests()
    {
        _http = new HttpClient(_keycloak) { BaseAddress = new Uri("http://keycloak.test/") };
    }

    public void Dispose()
    {
        _http.Dispose();
        _keycloak.Dispose();
        _cache.Dispose();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(100)]
    public async Task HandleAsync_AnyPageSize_WalksIndirectHoldersFirstThenTheRoleOnlyHolders(int pageSize)
    {
        var handler = CreateHandler();

        var walked = await WalkAsync(handler, pageSize);

        Assert.Equal(_keycloak.ExpectedSequence, walked);
    }

    [Fact]
    public async Task HandleAsync_FullWalkOfARoleWithIndirectHolders_ReadsKeycloakNoMoreThanTheFirstPageDid()
    {
        var handler = CreateHandler();

        var firstPage = await handler.HandleAsync(new GetUsersInRolePageRequest(Role, 0, 2), CancellationToken.None);
        var requestsForFirstPage = _keycloak.RequestCount;
        var remaining = await WalkAsync(handler, pageSize: 2, startAt: 2);

        Assert.True(firstPage.HasMore);
        Assert.Equal(_keycloak.ExpectedSequence, firstPage.Users.Select(u => u.Id).Concat(remaining));
        Assert.True(requestsForFirstPage > 0);
        Assert.Equal(requestsForFirstPage, _keycloak.RequestCount);
    }

    [Fact]
    public async Task HandleAsync_SnapshotEvicted_ReadsKeycloakAgainAndSeesTheChange()
    {
        var handler = CreateHandler();
        var before = await WalkAsync(handler, pageSize: 3);
        var afterFirstWalk = _keycloak.RequestCount;
        _keycloak.RoleUsers[Composite].Add(new StubUser(UserId(14)));

        var stale = await WalkAsync(handler, pageSize: 3);
        var afterStaleWalk = _keycloak.RequestCount;
        await _cache.RemoveAsync(CacheKeys.For.KeycloakRoleMembers(Role));
        var fresh = await WalkAsync(handler, pageSize: 3);

        Assert.Equal(before, stale);
        Assert.Equal(afterFirstWalk, afterStaleWalk);
        Assert.Equal([UserId(3), UserId(12), UserId(13), UserId(14), .. _keycloak.ExpectedSequence.Skip(3)], fresh);
        Assert.True(_keycloak.RequestCount > afterStaleWalk);
    }

    [Fact]
    public async Task HandleAsync_RoleWithoutIndirectHolders_CostsOneUsersReadPerPageAfterTheFirst()
    {
        _keycloak.Composites.Clear();
        var handler = CreateHandler();

        await handler.HandleAsync(new GetUsersInRolePageRequest(Role, 0, 4), CancellationToken.None);
        var afterFirstPage = _keycloak.RequestCount;
        var secondPage = await handler.HandleAsync(new GetUsersInRolePageRequest(Role, 4, 4), CancellationToken.None);

        Assert.Equal(afterFirstPage + 1, _keycloak.RequestCount);
        Assert.Equal(_keycloak.RoleUsers[Role].Where(u => u.Enabled).Skip(4).Take(4).Select(u => u.Id), secondPage.Users.Select(u => u.Id));
    }

    private static ApplicationUserId UserId(int n) => new(new Guid(n, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));

    private static async Task<List<ApplicationUserId>> WalkAsync(
        GetUsersInRolePageRequestHandler handler, int pageSize, int startAt = 0)
    {
        var ids = new List<ApplicationUserId>();
        var skip = startAt;
        while (true)
        {
            var page = await handler.HandleAsync(new GetUsersInRolePageRequest(Role, skip, pageSize), CancellationToken.None);
            ids.AddRange(page.Users.Select(u => u.Id));
            if (!page.HasMore)
            {
                return ids;
            }

            skip += pageSize;
        }
    }

    private GetUsersInRolePageRequestHandler CreateHandler()
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

        var adminClient = new KeycloakAdminClient(
            _http,
            new FixedTokenProvider(),
            keycloakOptions,
            Options.Create(new IdentitySchemeOptions { Scheme = IdentityScheme.PhoneNumber }),
            _cache,
            NullLogger<KeycloakAdminClient>.Instance);

        return new GetUsersInRolePageRequestHandler(adminClient, _cache, keycloakOptions);
    }

    private sealed class FixedTokenProvider : IServiceAccountTokenProvider
    {
        public Task<string> GetAccessTokenAsync(bool forceRefresh, CancellationToken cancellationToken)
        {
            return Task.FromResult("token");
        }
    }

    private sealed record StubUser(ApplicationUserId Id, bool Enabled = true);

    /// <summary>
    ///     Serves the Admin REST reads the role members need and counts every request. Direct holders of <c>member</c> are 1..11
    ///     in Keycloak's order (10 is disabled); <c>lead</c> includes <c>member</c> and is held by 3, 12 and 13, so 3 holds both
    ///     roles. The expected sequence is the indirect holders sorted by id (3, 12, 13) and then the roles-only holders in
    ///     Keycloak's order, disabled users left out.
    /// </summary>
    private sealed class CountingKeycloakHandler : HttpMessageHandler
    {
        private int _requestCount;

        public int RequestCount => Volatile.Read(ref _requestCount);

        public Dictionary<string, string[]> Composites { get; } = new(StringComparer.Ordinal)
        {
            [Composite] = [Role]
        };

        public Dictionary<string, List<StubUser>> RoleUsers { get; } = new(StringComparer.Ordinal)
        {
            [Role] = Enumerable.Range(1, 11).Select(n => new StubUser(UserId(n), Enabled: n != 10)).ToList(),
            [Composite] = [new StubUser(UserId(13)), new StubUser(UserId(3)), new StubUser(UserId(12))]
        };

        public List<ApplicationUserId> ExpectedSequence { get; } =
            [UserId(3), UserId(12), UserId(13), .. new[] { 1, 2, 4, 5, 6, 7, 8, 9, 11 }.Select(UserId)];

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Interlocked.Increment(ref _requestCount);
            var uri = request.RequestUri!;
            var path = uri.AbsolutePath;
            var rolesIndex = path.IndexOf("/roles", StringComparison.Ordinal);
            if (rolesIndex < 0)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            var rest = path[(rolesIndex + "/roles".Length)..].Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
            return Task.FromResult(rest switch
            {
                [] => Json("[" + string.Join(",", RoleNames().Select(RoleJson)) + "]"),
                [var name, "composites"] => Json("[" + string.Join(",", (Composites.GetValueOrDefault(name) ?? []).Select(n => RoleJson(n))) + "]"),
                [var name, "users"] => Json(UsersJson(name, uri.Query)),
                [_, "groups"] => Json("[]"),
                _ => new HttpResponseMessage(HttpStatusCode.NotFound)
            });
        }

        private IEnumerable<string> RoleNames()
        {
            return Composites.Keys.Concat(Composites.Values.SelectMany(v => v)).Distinct(StringComparer.Ordinal);
        }

        private string RoleJson(string name)
        {
            return $$"""{"name":"{{name}}","composite":{{(Composites.ContainsKey(name) ? "true" : "false")}},"clientRole":false}""";
        }

        private string UsersJson(string role, string query)
        {
            var first = int.Parse(QueryValue(query, "first"), System.Globalization.CultureInfo.InvariantCulture);
            var max = int.Parse(QueryValue(query, "max"), System.Globalization.CultureInfo.InvariantCulture);
            var users = (RoleUsers.GetValueOrDefault(role) ?? []).Skip(first).Take(max);
            return "[" + string.Join(",", users.Select(u =>
                $$"""{"id":"{{u.Id.Value}}","username":"u{{u.Id.Value}}","email":"{{u.Id.Value}}@example.com","enabled":{{(u.Enabled ? "true" : "false")}}}""")) + "]";
        }

        private static string QueryValue(string query, string key)
        {
            return query.TrimStart('?').Split('&').Select(p => p.Split('=')).Single(p => p[0] == key)[1];
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
