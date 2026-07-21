using Common.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Sms;
using Xunit;

namespace Notifications.Tests.Sms;

public sealed class SmsSetupTests
{
    private static IConfiguration BuildConfiguration(SmsProvider provider) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [$"{nameof(SmsOptions)}:{nameof(SmsOptions.Provider)}"] = provider.ToString(),
            })
            .Build();

    [Fact]
    public void AddNotificationServices_DummyProvider_RegistersDummyGatewayAndService()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(SmsProvider.Dummy);

        services.AddNotificationServices(configuration);

        Assert.Contains(services, descriptor =>
            descriptor.ServiceType == typeof(ISmsService) && descriptor.ImplementationType == typeof(DummySmsService));
        Assert.Contains(services, descriptor =>
            descriptor.ServiceType == typeof(ISmsGateway) && descriptor.ImplementationType == typeof(DummySmsGateway));
    }

    [Fact]
    public void AddNotificationServices_RealProvider_ThrowsBecauseNoRealGatewayExists()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(SmsProvider.Real);

        Assert.Throws<NotImplementedException>(() => services.AddNotificationServices(configuration));
    }
}
