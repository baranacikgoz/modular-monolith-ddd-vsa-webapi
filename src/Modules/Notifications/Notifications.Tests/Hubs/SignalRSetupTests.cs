using Common.Tests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notifications.Infrastructure.Hubs;
using Xunit;

namespace Notifications.Tests.Hubs;

public sealed class SignalRSetupTests
{
    private static IConfiguration BuildConfiguration(bool useRedisBackplane) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SignalROptions:UseRedisBackplane"] = useRedisBackplane.ToString(),
                ["SignalROptions:RedisConnectionString"] = useRedisBackplane ? "localhost:6379" : "",
            })
            .Build();

    private static ServiceCollection BuildServices(string environmentName)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IHostEnvironment>(new FakeHostEnvironment(environmentName));
        return services;
    }

    [Fact]
    public void AddNotificationsSignalR_ProductionWithoutRedisBackplane_Throws()
    {
        var services = BuildServices(Environments.Production);
        var configuration = BuildConfiguration(useRedisBackplane: false);

        var exception = Assert.Throws<InvalidOperationException>(
            () => services.AddNotificationsSignalR(configuration));

        Assert.Contains("UseRedisBackplane", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void AddNotificationsSignalR_ProductionWithRedisBackplane_DoesNotThrow()
    {
        var services = BuildServices(Environments.Production);
        var configuration = BuildConfiguration(useRedisBackplane: true);

        services.AddNotificationsSignalR(configuration);
    }

    [Fact]
    public void AddNotificationsSignalR_DevelopmentWithoutRedisBackplane_DoesNotThrow()
    {
        var services = BuildServices(Environments.Development);
        var configuration = BuildConfiguration(useRedisBackplane: false);

        services.AddNotificationsSignalR(configuration);
    }
}
