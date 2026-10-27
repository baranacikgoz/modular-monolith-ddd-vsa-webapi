using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Products.Infrastructure.Exports;
using Products.Infrastructure.Imports;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Jobs;

/// <summary>The consumers are picked up by the Host's assembly scan; nothing registers them by hand, so this is the check
/// that a rename or a moved namespace does not silently drop a job's queue.</summary>
public class JobConsumerRegistrationTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    [Theory]
    [InlineData(typeof(RunProductImportJobHandler))]
    [InlineData(typeof(RunProductExportJobHandler))]
    public void Host_HasAReceiveEndpointForEachJobConsumer(Type consumer)
    {
        var health = Factory.Services.GetRequiredService<IBusControl>().CheckHealth();

        Assert.Contains(health.Endpoints.Keys, name => name.EndsWith($"/{consumer.Name}", StringComparison.Ordinal));
    }
}
