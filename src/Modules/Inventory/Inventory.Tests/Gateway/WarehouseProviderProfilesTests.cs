using Common.Application.Options;
using Common.Tests;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Gateway;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Inventory.Tests.Gateway;

/// <summary>
///     A provider added to <see cref="WarehouseProvider" /> without its profile in <c>resiliency.json</c> would only fail
///     on the first release through it. This fails the build instead.
/// </summary>
[Collection("IntegrationTestCollection")]
public class WarehouseProviderProfilesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public void ResiliencyConfiguration_HasAKeyedProfileForEveryWarehouseProvider()
    {
        var keyed = Scope.ServiceProvider.GetRequiredService<IOptions<ResiliencyOptions>>().Value.Keyed;

        var missing = Enum.GetValues<WarehouseProvider>()
            .Select(WarehouseResiliencePipelines.ProfileNameOf)
            .Where(profile => !keyed.ContainsKey(profile))
            .ToList();

        Assert.True(missing.Count == 0, $"ResiliencyOptions.Keyed has no profile for: {string.Join(", ", missing)}.");
    }
}
