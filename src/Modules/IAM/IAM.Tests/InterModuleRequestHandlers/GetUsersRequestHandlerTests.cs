using Common.Application.Caching;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.IAM;
using IAM.Application.Keycloak;
using IAM.Infrastructure.InterModuleRequestHandlers;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Tests.InterModuleRequestHandlers;

#pragma warning disable CA1707 // Remove the underscores from member name

/// <summary>
///     The bulk display lookup's cache and fan-out against a substituted admin client and a real in-memory
///     FusionCache: no Keycloak container needed, so this class stays out of the integration collection.
/// </summary>
public sealed class GetUsersRequestHandlerTests : IDisposable
{
    private readonly IKeycloakAdminClient _adminClient = Substitute.For<IKeycloakAdminClient>();
    private readonly FusionCache _cache = new(new FusionCacheOptions());

    public void Dispose()
    {
        _cache.Dispose();
    }

    [Fact]
    public async Task HandleAsync_SameIdsRequestedTwice_AsksKeycloakOncePerId()
    {
        var ids = NewIds(3);
        _adminClient.GetUserAsync(Arg.Any<ApplicationUserId>(), Arg.Any<CancellationToken>())
            .Returns(call => Task.FromResult<Result<KeycloakUser>>(NewUser(call.Arg<ApplicationUserId>())));
        var handler = CreateHandler(concurrency: 8);

        var first = await handler.HandleAsync(new GetUsersRequest(ids), CancellationToken.None);
        var second = await handler.HandleAsync(new GetUsersRequest(ids), CancellationToken.None);

        await _adminClient.Received(3).GetUserAsync(Arg.Any<ApplicationUserId>(), Arg.Any<CancellationToken>());
        Assert.Equal(ids, first.Users.Select(u => u.Id));
        Assert.Equal(ids, second.Users.Select(u => u.Id));
        Assert.All(second.Users, u =>
        {
            Assert.Equal("Ada", u.FirstName);
            Assert.Equal("Lovelace", u.LastName);
        });
    }

    [Fact]
    public async Task HandleAsync_UnknownUser_CachedAsNegativeEntryAndLeftOutOfTheResponse()
    {
        var known = new ApplicationUserId(Guid.NewGuid());
        var unknown = new ApplicationUserId(Guid.NewGuid());
        _adminClient.GetUserAsync(known, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Result<KeycloakUser>>(NewUser(known)));
        _adminClient.GetUserAsync(unknown, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Result<KeycloakUser>>(Error.NotFound(nameof(KeycloakUser), unknown)));
        var handler = CreateHandler(concurrency: 8);

        var first = await handler.HandleAsync(new GetUsersRequest([known, unknown]), CancellationToken.None);
        var second = await handler.HandleAsync(new GetUsersRequest([known, unknown]), CancellationToken.None);

        await _adminClient.Received(1).GetUserAsync(unknown, Arg.Any<CancellationToken>());
        Assert.Equal([known], first.Users.Select(u => u.Id));
        Assert.Equal([known], second.Users.Select(u => u.Id));
    }

    [Fact]
    public async Task HandleAsync_DuplicateIdsInOneRequest_LookedUpOnce()
    {
        var id = new ApplicationUserId(Guid.NewGuid());
        _adminClient.GetUserAsync(id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Result<KeycloakUser>>(NewUser(id)));
        var handler = CreateHandler(concurrency: 8);

        var response = await handler.HandleAsync(new GetUsersRequest([id, id, id]), CancellationToken.None);

        await _adminClient.Received(1).GetUserAsync(id, Arg.Any<CancellationToken>());
        Assert.Single(response.Users);
    }

    [Fact]
    public async Task HandleAsync_ManyMisses_NeverExceedsUserLookupConcurrency()
    {
        const int concurrency = 3;
        var ids = NewIds(12);
        var gauge = new InFlightGauge();
        _adminClient.GetUserAsync(Arg.Any<ApplicationUserId>(), Arg.Any<CancellationToken>())
            .Returns(call => gauge.LookupAsync(call.Arg<ApplicationUserId>(), call.Arg<CancellationToken>()));
        var handler = CreateHandler(concurrency);

        var response = await handler.HandleAsync(new GetUsersRequest(ids), CancellationToken.None);

        Assert.Equal(ids.Count, response.Users.Count);
        Assert.InRange(gauge.MaxInFlight, 1, concurrency);
    }

    [Fact]
    public async Task HandleAsync_AfterTheCacheEntryIsEvicted_AsksKeycloakAgain()
    {
        var id = new ApplicationUserId(Guid.NewGuid());
        _adminClient.GetUserAsync(id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Result<KeycloakUser>>(NewUser(id)));
        var handler = CreateHandler(concurrency: 8);

        await handler.HandleAsync(new GetUsersRequest([id]), CancellationToken.None);
        await _cache.RemoveAsync(CacheKeys.For.UserDisplay(id));
        await handler.HandleAsync(new GetUsersRequest([id]), CancellationToken.None);

        await _adminClient.Received(2).GetUserAsync(id, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_KeycloakFailsAfterAnEntryExpired_ServesTheStaleEntry()
    {
        var id = new ApplicationUserId(Guid.NewGuid());
        _adminClient.GetUserAsync(id, Arg.Any<CancellationToken>())
            .Returns(
                Task.FromResult<Result<KeycloakUser>>(NewUser(id)),
                Task.FromException<Result<KeycloakUser>>(new HttpRequestException("Keycloak is down.")));
        var handler = CreateHandler(concurrency: 8, cacheDurationSeconds: 0);

        await handler.HandleAsync(new GetUsersRequest([id]), CancellationToken.None);
        var response = await handler.HandleAsync(new GetUsersRequest([id]), CancellationToken.None);

        Assert.Equal([id], response.Users.Select(u => u.Id));
    }

    private GetUsersRequestHandler CreateHandler(int concurrency, int cacheDurationSeconds = 300)
    {
        var options = Options.Create(new KeycloakOptions
        {
            BaseUrl = "http://localhost:8080",
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
            UserLookupConcurrency = concurrency,
            UserDisplayCacheDurationSeconds = cacheDurationSeconds,
            UserDisplayCacheDistributedDurationSeconds = 86400,
            UserDisplayNegativeCacheDurationSeconds = 300,
            RoleGraphCacheSeconds = 60
        });

        return new GetUsersRequestHandler(_adminClient, _cache, options);
    }

    private static List<ApplicationUserId> NewIds(int count)
    {
        return Enumerable.Range(0, count).Select(_ => new ApplicationUserId(Guid.NewGuid())).ToList();
    }

    private static KeycloakUser NewUser(ApplicationUserId id)
    {
        return new KeycloakUser(id, id.ToString(), $"{id}@example.com", "Ada", "Lovelace", null, null, true, DateTimeOffset.UtcNow);
    }

    /// <summary>A slow fake lookup that records how many calls overlapped, so the concurrency cap can be asserted.</summary>
    private sealed class InFlightGauge
    {
        private readonly Lock _lock = new();
        private int _inFlight;

        public int MaxInFlight { get; private set; }

        public async Task<Result<KeycloakUser>> LookupAsync(ApplicationUserId id, CancellationToken cancellationToken)
        {
            lock (_lock)
            {
                _inFlight++;
                MaxInFlight = Math.Max(MaxInFlight, _inFlight);
            }

            await Task.Delay(30, cancellationToken);

            lock (_lock)
            {
                _inFlight--;
            }

            return NewUser(id);
        }
    }
}

#pragma warning restore CA1707
