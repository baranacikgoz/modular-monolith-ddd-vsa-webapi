using Common.Application.Options;
using Common.Application.Validation;
using Common.Tests;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notifications.Application.Otp;
using Notifications.Infrastructure.Otp;
using Xunit;

namespace Notifications.Tests.Otp;

public sealed class OtpSetupTests
{
    private static IConfiguration BuildConfiguration(bool useRedis) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["CachingOptions:UseRedis"] = useRedis.ToString(),
            })
            .Build();

    private static ValidationContext<SmsOptions> BuildContext(SmsProvider provider, string environmentName)
    {
        var context = new ValidationContext<SmsOptions>(new SmsOptions { Provider = provider });
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
    }

    [Fact]
    public void Validate_DummyProviderInProduction_Invalid()
    {
        var result = new SmsOptionsValidator().Validate(BuildContext(SmsProvider.Dummy, Environments.Production));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Dummy", StringComparison.Ordinal));
    }

    [Fact]
    public void Validate_DummyProviderInDevelopment_Valid()
    {
        var result = new SmsOptionsValidator().Validate(BuildContext(SmsProvider.Dummy, Environments.Development));

        Assert.True(result.IsValid);
    }

    [Fact]
    public void AddOtpServices_UseRedis_RegistersRedisOtpService()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(useRedis: true);

        services.AddOtpServices(configuration);

        Assert.Contains(services, descriptor =>
            descriptor.ServiceType == typeof(IOtpService) && descriptor.ImplementationType == typeof(RedisOtpService));
    }

    [Fact]
    public void AddOtpServices_WithoutRedis_RegistersDummyOtpService()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(useRedis: false);

        services.AddOtpServices(configuration);

        Assert.Contains(services, descriptor =>
            descriptor.ServiceType == typeof(IOtpService) && descriptor.ImplementationType == typeof(DummyOtpService));
    }
}
