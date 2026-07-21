#pragma warning disable CA1707 // Remove the underscores from member name

using Common.Application.Options;
using Common.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using Xunit;

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
    };

    private static ValidationContext<CachingOptions> BuildContext(CachingOptions options, string environmentName)
    {
        var context = new ValidationContext<CachingOptions>(options);
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
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
}

#pragma warning restore CA1707
