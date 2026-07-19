#pragma warning disable CA1707 // Remove the underscores from member name

using Common.Infrastructure.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Common.Tests;

public sealed class CachingSetupTests
{
    private static IConfiguration BuildConfiguration(bool useRedis, bool allowInMemoryOnlyInProduction) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["CachingOptions:UseRedis"] = useRedis.ToString(),
                ["CachingOptions:AllowInMemoryOnlyInProduction"] = allowInMemoryOnlyInProduction.ToString(),
                ["CachingOptions:EntryDefaults:Duration"] = "00:05:00",
                ["CachingOptions:EntryDefaults:FailSafeMaxDuration"] = "02:00:00",
                ["CachingOptions:EntryDefaults:FailSafeThrottleDuration"] = "00:00:30",
                ["CachingOptions:EntryDefaults:FactorySoftTimeout"] = "00:00:00.1",
                ["CachingOptions:EntryDefaults:FactoryHardTimeout"] = "00:00:01.5",
                ["CachingOptions:IdempotencyKeyDuration"] = "1.00:00:00",
                ["CachingOptions:IdempotencyL1Duration"] = "01:00:00",
            })
            .Build();

    private static ServiceCollection BuildServices(string environmentName)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IHostEnvironment>(new FakeHostEnvironment(environmentName));
        return services;
    }

    [Fact]
    public void AddCommonCaching_ProductionWithoutRedis_Throws()
    {
        var services = BuildServices(Environments.Production);
        var configuration = BuildConfiguration(useRedis: false, allowInMemoryOnlyInProduction: false);

        var exception = Assert.Throws<InvalidOperationException>(
            () => services.AddCommonCaching(configuration));

        Assert.Contains("UseRedis", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void AddCommonCaching_ProductionWithoutRedisButAllowed_DoesNotThrow()
    {
        var services = BuildServices(Environments.Production);
        var configuration = BuildConfiguration(useRedis: false, allowInMemoryOnlyInProduction: true);

        services.AddCommonCaching(configuration);
    }

    [Fact]
    public void AddCommonCaching_DevelopmentWithoutRedis_DoesNotThrow()
    {
        var services = BuildServices(Environments.Development);
        var configuration = BuildConfiguration(useRedis: false, allowInMemoryOnlyInProduction: false);

        services.AddCommonCaching(configuration);
    }
}

#pragma warning restore CA1707
