#pragma warning disable CA1707 // Remove the underscores from member name

using Common.Application.Options;
using Common.Application.Validation;
using CachingSetup = Common.Infrastructure.Caching.Setup;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace Common.Tests;

public sealed class CachingSetupTests
{
    private static CachingOptions BuildOptions(bool useRedis, bool allowInMemoryOnlyInProduction) => new()
    {
        UseRedis = useRedis,
        AllowInMemoryOnlyInProduction = allowInMemoryOnlyInProduction,
        EntryDefaults = new CachingEntryDefaults
        {
            Duration = TimeSpan.FromMinutes(5),
            FailSafeMaxDuration = TimeSpan.FromHours(2),
            FailSafeThrottleDuration = TimeSpan.FromSeconds(30),
            FactorySoftTimeout = TimeSpan.FromMilliseconds(100),
            FactoryHardTimeout = TimeSpan.FromSeconds(1.5),
        },
        IdempotencyKeyDuration = TimeSpan.FromDays(1),
        IdempotencyL1Duration = TimeSpan.FromHours(1),
        MemoryCacheSizeLimit = 1000,
    };

    /// <summary>The real registration (in-memory L1 only) with a tiny size limit, so overflow is easy to provoke.</summary>
    private static ServiceProvider BuildCacheServices(long sizeLimit)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["CachingOptions:UseRedis"] = "false",
                ["CachingOptions:AllowInMemoryOnlyInProduction"] = "false",
                ["CachingOptions:EntryDefaults:Duration"] = "00:05:00",
                ["CachingOptions:EntryDefaults:FailSafeMaxDuration"] = "02:00:00",
                ["CachingOptions:EntryDefaults:FailSafeThrottleDuration"] = "00:00:30",
                ["CachingOptions:EntryDefaults:FactorySoftTimeout"] = "00:00:00.1",
                ["CachingOptions:EntryDefaults:FactoryHardTimeout"] = "00:00:30",
                ["CachingOptions:IdempotencyKeyDuration"] = "1.00:00:00",
                ["CachingOptions:IdempotencyL1Duration"] = "01:00:00",
                ["CachingOptions:MemoryCacheSizeLimit"] = sizeLimit.ToString(System.Globalization.CultureInfo.InvariantCulture),
            })
            .Build();

        var services = new ServiceCollection();
        services.AddLogging();
        CachingSetup.AddCommonCaching(services, configuration);
        return services.BuildServiceProvider();
    }

    private static ValidationContext<CachingOptions> BuildContext(CachingOptions options, string environmentName)
    {
        var context = new ValidationContext<CachingOptions>(options);
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
    }

    [Fact]
    public void CreateSerializerOptions_ValueTuple_RoundTripsThroughFusionCacheSerializer()
    {
        var serializer = new FusionCacheSystemTextJsonSerializer(CachingSetup.CreateSerializerOptions());

        var bytes = serializer.Serialize((7, "seven"));
        var roundTripped = serializer.Deserialize<(int Number, string Name)>(bytes);

        Assert.Equal((7, "seven"), roundTripped);
    }

    [Fact]
    public void Validate_ProductionWithoutRedis_Invalid()
    {
        var options = BuildOptions(useRedis: false, allowInMemoryOnlyInProduction: false);

        var result = new CachingOptionsValidator().Validate(BuildContext(options, Environments.Production));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("UseRedis", StringComparison.Ordinal));
    }

    [Fact]
    public void Validate_ProductionWithoutRedisButAllowed_Valid()
    {
        var options = BuildOptions(useRedis: false, allowInMemoryOnlyInProduction: true);

        var result = new CachingOptionsValidator().Validate(BuildContext(options, Environments.Production));

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_DevelopmentWithoutRedis_Valid()
    {
        var options = BuildOptions(useRedis: false, allowInMemoryOnlyInProduction: false);

        var result = new CachingOptionsValidator().Validate(BuildContext(options, Environments.Development));

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task L1_EntriesBeyondTheSizeLimit_AreNotKept_WhileCallSitesWithTheirOwnEntryOptionsDoNotThrow()
    {
        await using var provider = BuildCacheServices(sizeLimit: 3);
        var cache = provider.GetRequiredService<IFusionCache>();

        // Fresh options, as most call sites build them: no Size of their own. A size-limited memory cache would throw
        // for an entry without a size, so this only works because every entry counts as size 1 by default.
        for (var i = 0; i < 5; i++)
        {
            await cache.SetAsync($"key-{i}", i, new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(1) }, token: TestContext.Current.CancellationToken);
        }

        // Counted before any read: a read of a key that is not there must not be what the assertion measures.
        Assert.Equal(3, ((MemoryCache)provider.GetRequiredKeyedService<IMemoryCache>(CachingSetup.L1MemoryCacheServiceKey)).Count);

        var kept = 0;
        for (var i = 0; i < 5; i++)
        {
            if ((await cache.TryGetAsync<int>($"key-{i}", token: TestContext.Current.CancellationToken)).HasValue)
            {
                kept++;
            }
        }

        Assert.Equal(3, kept);
    }

    [Fact]
    public async Task L1_TagsAndClear_WorkWithASizeLimit()
    {
        await using var provider = BuildCacheServices(sizeLimit: 50);
        var cache = provider.GetRequiredService<IFusionCache>();

        await cache.SetAsync("tagged", 1, tags: ["group"], token: TestContext.Current.CancellationToken);
        await cache.RemoveByTagAsync("group", token: TestContext.Current.CancellationToken);
        await cache.SetAsync("other", 2, token: TestContext.Current.CancellationToken);
        await cache.ClearAsync(token: TestContext.Current.CancellationToken);

        Assert.False((await cache.TryGetAsync<int>("tagged", token: TestContext.Current.CancellationToken)).HasValue);
        Assert.False((await cache.TryGetAsync<int>("other", token: TestContext.Current.CancellationToken)).HasValue);
    }

    [Fact]
    public void Validate_NonPositiveMemoryCacheSizeLimit_Invalid()
    {
        var options = BuildOptions(useRedis: false, allowInMemoryOnlyInProduction: false);
        options.MemoryCacheSizeLimit = 0;

        var result = new CachingOptionsValidator().Validate(BuildContext(options, Environments.Development));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(CachingOptions.MemoryCacheSizeLimit));
    }
}

#pragma warning restore CA1707
