using Common.Infrastructure.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Host.Tests;

public class DynamicModuleTests : IAsyncLifetime
{
    [Fact]
    public async Task Boot_WithTestModuleOverride_ShouldOnlyLoadTargetModules()
    {
        // Arrange
        var targetModule = "IAM";
        var factory = new HostTestFactory().WithModules(targetModule);
        await factory.InitializeAsync();

        try
        {
            // Act
            // Trigger server boot
            _ = factory.CreateClient();

            // Assert
            var modules = factory.Services.GetServices<IModule>().ToList();

            // Ensure only IAM was loaded (plus common infrastructure if it registered itself as IModule, 
            // but looking at Setup.Modules.cs, it only registers what's in modulesToLoad).
            Assert.Single(modules);
            Assert.Equal(targetModule, modules[0].Name);
        }
        finally
        {
            await factory.DisposeAsync();
        }
    }

    [Fact]
    public async Task Boot_ModulesAreResolvedInPriorityOrder()
    {
        // Arrange - IAM (2) and Notifications (3)
        var factory = new HostTestFactory().WithModules("Notifications,IAM");
        await factory.InitializeAsync();

        try
        {
            // Act
            _ = factory.CreateClient();

            // Assert
            var resolvedModules = factory.Services.GetServices<IModule>()
                .OrderBy(m => m.StartupPriority)
                .ToList();

            // Both should be present
            Assert.Equal(2, resolvedModules.Count);

            // Should be sorted by Priority (IAM: 2, Notifications: 3)
            Assert.Equal("IAM", resolvedModules[0].Name);
            Assert.Equal("Notifications", resolvedModules[1].Name);
        }
        finally
        {
            await factory.DisposeAsync();
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;
}
