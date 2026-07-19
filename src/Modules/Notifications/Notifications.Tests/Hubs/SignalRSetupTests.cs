using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    public void AddNotificationsSignalR_ProductionWithoutRedisBackplane_Throws()
    {
        WithEnvironment("Production", () =>
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration(useRedisBackplane: false);

            var exception = Assert.Throws<InvalidOperationException>(
                () => services.AddNotificationsSignalR(configuration));

            Assert.Contains("UseRedisBackplane", exception.Message, StringComparison.Ordinal);
        });
    }

    [Fact]
    public void AddNotificationsSignalR_ProductionWithRedisBackplane_DoesNotThrow()
    {
        WithEnvironment("Production", () =>
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration(useRedisBackplane: true);

            services.AddNotificationsSignalR(configuration);
        });
    }

    [Fact]
    public void AddNotificationsSignalR_DevelopmentWithoutRedisBackplane_DoesNotThrow()
    {
        WithEnvironment("Development", () =>
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration(useRedisBackplane: false);

            services.AddNotificationsSignalR(configuration);
        });
    }
}
