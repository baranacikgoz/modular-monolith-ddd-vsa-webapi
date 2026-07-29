using Common.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Push;
using Notifications.Infrastructure.Push;
using Notifications.Infrastructure.Push.Firebase;
using Xunit;

namespace Notifications.Tests.Push;

public sealed class PushSetupTests
{
    private static IConfiguration BuildConfiguration(PushProvider provider) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [$"{nameof(PushOptions)}:{nameof(PushOptions.Provider)}"] = provider.ToString(),
                [$"{nameof(PushOptions)}:{nameof(PushOptions.SendTimeoutSeconds)}"] = "8",
            })
            .Build();

    [Fact]
    public void AddPushServices_DummyProvider_RegistersDummyGateway()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(PushProvider.Dummy);

        services.AddPushServices(configuration);

        Assert.Contains(services, descriptor =>
            descriptor.ServiceType == typeof(IPushGateway) && descriptor.ImplementationType == typeof(DummyPushGateway));
    }

    [Fact]
    public void AddPushServices_FirebaseProvider_RegistersFirebaseGateway()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(PushProvider.Firebase);

        services.AddPushServices(configuration);

        Assert.Contains(services, descriptor =>
            descriptor.ServiceType == typeof(IPushGateway) && descriptor.ImplementationType == typeof(FirebasePushGateway));
    }
}
