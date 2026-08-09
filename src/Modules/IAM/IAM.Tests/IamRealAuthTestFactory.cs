using System.Globalization;
using Common.Tests;
using Microsoft.AspNetCore.Hosting;
using Testcontainers.Redis;
using Xunit;

namespace IAM.Tests;

// Real JwtBearer pipeline (no TestAuthHandler) plus a real Redis container, so tests can exercise
// OnTokenValidated end to end, including a genuine Redis-unreachable window (see issue #133).
public class IamRealAuthTestFactory : IntegrationTestWebAppFactory
{
    private const string RedisTestPassword = "iam-tests-redis-password";

    private readonly RedisContainer _redisContainer = new RedisBuilder("redis:7-alpine")
        .WithCommand("redis-server", "--requirepass", RedisTestPassword)
        .Build();

    protected override bool UseTestAuthentication => false;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        // CachingOptions:UseRedis is read at registration time by AddCommonCaching, so it must travel
        // via UseSetting (registration-visible), not AddInMemoryCollection (merged after registration,
        // see CLAUDE.md test-config rule).
        builder.UseSetting("CachingOptions:UseRedis", "true");
        builder.UseSetting("CachingOptions:Redis:Host", _redisContainer.Hostname);
        builder.UseSetting("CachingOptions:Redis:Port",
            _redisContainer.GetMappedPublicPort(6379).ToString(CultureInfo.InvariantCulture));
        builder.UseSetting("CachingOptions:Redis:Password", RedisTestPassword);
        builder.UseSetting("CachingOptions:Redis:AppName", "iam-tests");
    }

    public override async ValueTask InitializeAsync()
    {
        await _redisContainer.StartAsync();
        await base.InitializeAsync();
    }

    public override async ValueTask DisposeAsync()
    {
        await _redisContainer.DisposeAsync();
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    // Simulates Redis becoming unreachable mid-test: the app keeps running, FusionCache's L2 +
    // backplane just stop responding, matching the outage in issue #133.
    public Task StopRedisAsync()
    {
        return _redisContainer.StopAsync();
    }

    public Task StartRedisAsync()
    {
        return _redisContainer.StartAsync();
    }
}

[CollectionDefinition("IamRealAuthCollection")]
public class IamRealAuthCollection : ICollectionFixture<IamRealAuthTestFactory>
{
}
