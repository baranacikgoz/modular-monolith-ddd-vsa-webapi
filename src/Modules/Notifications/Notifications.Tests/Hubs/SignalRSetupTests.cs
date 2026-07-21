using Common.Application.Options;
using Common.Application.Validation;
using Common.Tests;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notifications.Application.Hubs;
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

    private static ValidationContext<SignalROptions> BuildContext(bool useRedisBackplane, string environmentName)
    {
        var options = new SignalROptions
        {
            UseRedisBackplane = useRedisBackplane,
            RedisConnectionString = useRedisBackplane ? "localhost:6379" : "",
        };
        var context = new ValidationContext<SignalROptions>(options);
        context.RootContextData[ValidationContextExtensions.HostEnvironmentKey] = new FakeHostEnvironment(environmentName);
        return context;
    }

    [Fact]
    public void Validate_ProductionWithoutRedisBackplane_Invalid()
    {
        var result = new SignalROptionsValidator().Validate(BuildContext(useRedisBackplane: false, Environments.Production));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("UseRedisBackplane", StringComparison.Ordinal));
    }

    [Fact]
    public void Validate_ProductionWithRedisBackplane_Valid()
    {
        var result = new SignalROptionsValidator().Validate(BuildContext(useRedisBackplane: true, Environments.Production));

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_DevelopmentWithoutRedisBackplane_Valid()
    {
        var result = new SignalROptionsValidator().Validate(BuildContext(useRedisBackplane: false, Environments.Development));

        Assert.True(result.IsValid);
    }

    [Fact]
    public void AddNotificationsSignalR_WithoutRedisBackplane_RegistersDispatcher()
    {
        var services = new ServiceCollection();
        var configuration = BuildConfiguration(useRedisBackplane: false);

        services.AddNotificationsSignalR(configuration);

        Assert.Contains(services, descriptor => descriptor.ServiceType == typeof(INotificationDispatcher));
    }
}
