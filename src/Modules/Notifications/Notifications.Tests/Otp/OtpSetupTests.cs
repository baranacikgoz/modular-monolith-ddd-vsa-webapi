using Common.Tests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Otp;
using Notifications.Infrastructure.Sms;
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

    private static ServiceCollection BuildServices(string environmentName)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IHostEnvironment>(new FakeHostEnvironment(environmentName));
        return services;
    }

    [Fact]
    public void AddOtpServices_Production_ThrowsBecauseSmsGatewayIsDummy()
    {
        var services = BuildServices(Environments.Production);
        var configuration = BuildConfiguration(useRedis: true);

        var exception = Assert.Throws<InvalidOperationException>(
            () => services.AddOtpServices(configuration));

        Assert.Contains(nameof(DummySmsGateway), exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void AddOtpServices_Development_DoesNotThrow()
    {
        var services = BuildServices(Environments.Development);
        var configuration = BuildConfiguration(useRedis: false);

        services.AddOtpServices(configuration);

        Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(ISmsGateway));
    }
}
