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
        var context = new ValidationContext<SmsOptions>(new SmsOptions
        {
            Provider = provider,
            ThrottleCounterTtlHours = 25,
            Templates = new SmsTemplatesOptions { Otp = { ["en"] = "code {0}" } },
        });
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
    public void AddOtpServices_WithoutRedis_RegistersOtpService()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(useRedis: false);

        services.AddOtpServices(configuration);

        Assert.Contains(services, descriptor =>
            descriptor.ServiceType == typeof(IOtpService) && descriptor.ImplementationType == typeof(OtpService));
    }

    private static ValidationContext<OtpOptions> BuildOtpContext(string? dummyCode, string environmentName)
    {
        var context = new ValidationContext<OtpOptions>(new OtpOptions
        {
            Length = 6,
            ExpirationInMinutes = 5,
            ResendIntervalSeconds = 60,
            MaxSendsPerPhonePerWindow = 5,
            PhoneQuotaWindowMinutes = 60,
            DummyCode = dummyCode,
        });
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
    }

    [Fact]
    public void Validate_DummyCodeInProduction_Invalid()
    {
        var result = new OtpOptionsValidator().Validate(BuildOtpContext("123456", Environments.Production));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("DummyCode", StringComparison.Ordinal));
    }

    [Fact]
    public void Validate_DummyCodeInDevelopment_Valid()
    {
        var result = new OtpOptionsValidator().Validate(BuildOtpContext("123456", Environments.Development));

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_NoDummyCodeInProduction_Valid()
    {
        var result = new OtpOptionsValidator().Validate(BuildOtpContext(null, Environments.Production));

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("1234567")]
    [InlineData("12345a")]
    public void Validate_DummyCodeNotLengthDigits_Invalid(string dummyCode)
    {
        var result = new OtpOptionsValidator().Validate(BuildOtpContext(dummyCode, Environments.Development));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Length digits", StringComparison.Ordinal));
    }
}
