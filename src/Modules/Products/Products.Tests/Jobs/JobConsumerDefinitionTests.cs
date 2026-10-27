using MassTransit;
using NSubstitute;
using Products.Infrastructure.Exports;
using Products.Infrastructure.Imports;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Jobs;

/// <summary>A prefetched message waiting for a free slot sits unacked and can trip the broker's consumer timeout, so a job
/// consumer's prefetch count and concurrent message limit must be the same value, taken from its option.</summary>
public class JobConsumerDefinitionTests
{
    [Fact]
    public void ImportConsumer_PrefetchCountAndConcurrentMessageLimitAreTheOptionsValue()
    {
        var definition = new RunProductImportJobHandlerDefinition(TestOptions.Products(o => o.ImportJobConcurrentMessageLimit = 3));
        var endpoint = Substitute.For<IReceiveEndpointConfigurator>();

        ((IConsumerDefinition<RunProductImportJobHandler>)definition).Configure(
            endpoint, Substitute.For<IConsumerConfigurator<RunProductImportJobHandler>>(), Substitute.For<IRegistrationContext>());

        Assert.Equal(3, endpoint.PrefetchCount);
        Assert.Equal(3, endpoint.ConcurrentMessageLimit);
    }

    [Fact]
    public void ExportConsumer_PrefetchCountAndConcurrentMessageLimitAreTheOptionsValue()
    {
        var definition = new RunProductExportJobHandlerDefinition(TestOptions.Products(o => o.ExportJobConcurrentMessageLimit = 4));
        var endpoint = Substitute.For<IReceiveEndpointConfigurator>();

        ((IConsumerDefinition<RunProductExportJobHandler>)definition).Configure(
            endpoint, Substitute.For<IConsumerConfigurator<RunProductExportJobHandler>>(), Substitute.For<IRegistrationContext>());

        Assert.Equal(4, endpoint.PrefetchCount);
        Assert.Equal(4, endpoint.ConcurrentMessageLimit);
    }
}
