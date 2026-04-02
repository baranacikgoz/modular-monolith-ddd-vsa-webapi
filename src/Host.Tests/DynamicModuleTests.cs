using Common.Infrastructure.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Host.Tests;

public class DynamicModuleTests
{
    [Fact]
    public async Task Boot_WithTestModuleOverride_ShouldOnlyLoadTargetModules()
    {
        // Arrange
        var targetModule = "IAM";
        await using var factory = new HostTestFactory().WithModules(targetModule);
        await factory.InitializeAsync();

        // Act — trigger server boot
        _ = factory.CreateClient();

        // Assert
        var modules = factory.Services.GetServices<IModule>().ToList();
        Assert.Single(modules);
        Assert.Equal(targetModule, modules[0].Name);
    }

    [Fact]
    public async Task Boot_ModulesAreResolvedInPriorityOrder()
    {
        // Arrange — BackgroundJobs (0), IAM (2), and Notifications (3)
        // Notifications depends on IBackgroundJobs at runtime (via UserRegisteredIntegrationEventHandler)
        await using var factory = new HostTestFactory().WithModules("Notifications,IAM,BackgroundJobs");
        await factory.InitializeAsync();

        // Act
        _ = factory.CreateClient();

        // Assert
        var resolvedModules = factory.Services.GetServices<IModule>()
            .OrderBy(m => m.StartupPriority)
            .ToList();

        Assert.Equal(3, resolvedModules.Count);
        Assert.Equal("BackgroundJobs", resolvedModules[0].Name);
        Assert.Equal("IAM", resolvedModules[1].Name);
        Assert.Equal("Notifications", resolvedModules[2].Name);
    }
}
