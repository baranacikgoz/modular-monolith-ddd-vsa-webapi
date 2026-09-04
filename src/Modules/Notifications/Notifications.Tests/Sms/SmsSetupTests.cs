using Common.Application.Options;
using Common.Application.Validation;
using Common.Tests;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Sms;
using Xunit;

namespace Notifications.Tests.Sms;

public sealed class SmsSetupTests
{
    private static ValidationContext<SmsOptions> BuildContext(SmsOptions options, string environmentName)
    {
        var context = new ValidationContext<SmsOptions>(options);
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
    }

    [Fact]
    public void Validate_NetGsmMissingCredentials_Invalid()
    {
        var options = new SmsOptions
        {
            Provider = SmsProvider.NetGsm,
            ThrottleCounterTtlHours = 25,
            Templates = new SmsTemplatesOptions { Otp = { ["en"] = "code {0}" } },
        };

        var result = new SmsOptionsValidator().Validate(BuildContext(options, Environments.Development));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_NetGsmWithCredentials_Valid()
    {
        var options = new SmsOptions
        {
            Provider = SmsProvider.NetGsm,
            BaseUrl = "https://api.netgsm.com.tr",
            UserCode = "userCode",
            Password = "password",
            MsgHeader = "HEADER",
            AttemptTimeoutSeconds = 4,
            TotalRequestTimeoutSeconds = 8,
            MaxRetryAttempts = 1,
            MaxPerPhoneNumberPerDay = 10,
            MaxPerDay = 5000,
            ThrottleCounterTtlHours = 25,
            Templates = new SmsTemplatesOptions { Otp = { ["en"] = "code {0}" } },
        };

        var result = new SmsOptionsValidator().Validate(BuildContext(options, Environments.Production));

        Assert.True(result.IsValid);
    }

    private static IConfiguration BuildConfiguration(SmsProvider provider) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.Provider)}"] = provider.ToString(),
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.BaseUrl)}"] = "https://api.netgsm.com.tr",
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.UserCode)}"] = "userCode",
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.Password)}"] = "password",
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.MsgHeader)}"] = "HEADER",
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.AttemptTimeoutSeconds)}"] = "4",
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.TotalRequestTimeoutSeconds)}"] = "8",
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.MaxRetryAttempts)}"] = "1",
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.MaxPerPhoneNumberPerDay)}"] = "10",
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.MaxPerDay)}"] = "5000",
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.ThrottleCounterTtlHours)}"] = "25",
            })
            .Build();

    [Fact]
    public void AddNotificationServices_DummyProvider_RegistersDummyGateway()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(SmsProvider.Dummy);

        services.AddNotificationServices(configuration);

        Assert.Contains(services, descriptor =>
            descriptor.ServiceType == typeof(ISmsGateway) && descriptor.ImplementationType == typeof(DummySmsGateway));
    }

    [Fact]
    public void AddNotificationServices_NetGsmProvider_RegistersGatewayBehindThrottle()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(SmsProvider.NetGsm);

        services.AddNotificationServices(configuration);

        Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(ISmsGateway));
        Assert.DoesNotContain(services, descriptor => descriptor.ImplementationType == typeof(DummySmsGateway));
    }
}
