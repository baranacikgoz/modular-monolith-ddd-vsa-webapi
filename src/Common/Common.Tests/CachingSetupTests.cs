#pragma warning disable CA1707 // Remove the underscores from member name

using Common.Infrastructure.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

    private static void WithEnvironment(string? environment, Action action)
    {
        var original = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        try
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", environment);
            action();
        }
        finally
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", original);
        }
    }

    [Fact]
    public void AddCommonCaching_ProductionWithoutRedis_Throws()
    {
        WithEnvironment("Production", () =>
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration(useRedis: false, allowInMemoryOnlyInProduction: false);

            var exception = Assert.Throws<InvalidOperationException>(
                () => services.AddCommonCaching(configuration));

            Assert.Contains("UseRedis", exception.Message, StringComparison.Ordinal);
        });
    }

    [Fact]
    public void AddCommonCaching_ProductionWithoutRedisButAllowed_DoesNotThrow()
    {
        WithEnvironment("Production", () =>
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration(useRedis: false, allowInMemoryOnlyInProduction: true);

            services.AddCommonCaching(configuration);
        });
    }

    [Fact]
    public void AddCommonCaching_DevelopmentWithoutRedis_DoesNotThrow()
    {
        WithEnvironment("Development", () =>
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration(useRedis: false, allowInMemoryOnlyInProduction: false);

            services.AddCommonCaching(configuration);
        });
    }
}

#pragma warning restore CA1707
