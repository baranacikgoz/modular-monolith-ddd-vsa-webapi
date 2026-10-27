using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.Application.Exports;
using Products.Application.Persistence;
using ZiggyCreatures.Caching.Fusion;

namespace Products.Infrastructure.Exports;

/// <summary>The queue consumer behind every product export.</summary>
public sealed class RunProductExportJobHandler(
    ProductExportJobRunner runner,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IProductsDbContext> inbox,
    ILogger<RunProductExportJobHandler> logger
) : IntegrationEventHandlerBase<ProductExportRequestedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override Task ProcessAsync(ProductExportRequestedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        return runner.RunAsync(new ProductExportJobId(@event.JobId), cancellationToken);
    }
}

/// <summary>Bounds how many exports one pod builds at once: memory per pod is roughly this limit times one whole CSV
/// (capped by <see cref="ProductsOptions.ExportMaxRows" />). The prefetch count equals the limit.</summary>
public sealed class RunProductExportJobHandlerDefinition(IOptions<ProductsOptions> options)
    : ConsumerDefinition<RunProductExportJobHandler>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<RunProductExportJobHandler> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.SetConcurrency(options.Value.ExportJobConcurrentMessageLimit);
    }
}
