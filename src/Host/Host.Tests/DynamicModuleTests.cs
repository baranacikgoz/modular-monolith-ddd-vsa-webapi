using Common.Infrastructure.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Host.Tests;

// Each test boots an independent host to verify module selection isolation.
// Sharing a factory (IClassFixture) is intentionally avoided because tests require different module combinations.
public class DynamicModuleTests
{
    [Fact]
    public async Task Boot_WithTestModuleOverride_ShouldLoadTargetModulePlusCoreModules()
    {
        // Arrange
        var targetModule = "IAM";
        await using var factory = new HostTestFactory().WithModules(targetModule);
        await factory.InitializeAsync();

        // Act — trigger server boot
        _ = factory.CreateClient();

        // Assert — ICoreModules (BackgroundJobs, Outbox) are always loaded alongside the target module
        var modules = factory.Services.GetServices<IModule>().ToList();
        Assert.Equal(3, modules.Count);
        Assert.Contains(modules, m => m.Name == targetModule);
        Assert.Contains(modules, m => m is ICoreModule);
    }

    [Fact]
    public async Task Boot_ModulesAreResolvedInPriorityOrder()
    {
        // Arrange — Outbox (1) always loads as ICoreModule alongside the explicitly requested modules
        await using var factory = new HostTestFactory().WithModules("Notifications,IAM,BackgroundJobs");
        await factory.InitializeAsync();

        // Act
        _ = factory.CreateClient();

        // Assert — BackgroundJobs(0), Outbox(1), IAM(2), Notifications(3)
        var resolvedModules = factory.Services.GetServices<IModule>()
            .OrderBy(m => m.StartupPriority)
            .ToList();

        Assert.Equal(4, resolvedModules.Count);
        Assert.Equal("BackgroundJobs", resolvedModules[0].Name);
        Assert.Equal("Outbox", resolvedModules[1].Name);
        Assert.Equal("IAM", resolvedModules[2].Name);
        Assert.Equal("Notifications", resolvedModules[3].Name);
    }
}
