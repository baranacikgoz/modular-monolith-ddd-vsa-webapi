using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.Application.Imports;
using Products.Application.Persistence;
using ZiggyCreatures.Caching.Fusion;

namespace Products.Infrastructure.Imports;

/// <summary>The queue consumer behind every product import message (the first one and every continuation).</summary>
public sealed class RunProductImportJobHandler(
    ProductImportJobRunner runner,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IProductsDbContext> inbox,
    ILogger<RunProductImportJobHandler> logger
) : IntegrationEventHandlerBase<ProductImportRequestedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override Task ProcessAsync(ProductImportRequestedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        return runner.RunAsync(new ProductImportJobId(@event.JobId), cancellationToken);
    }
}

/// <summary>Bounds how many import messages one pod works on at once: memory per pod is roughly this limit times one
/// message's slice of rows. The prefetch count equals the limit: a prefetched message waiting for a slot sits unacked
/// and would trip the broker's consumer timeout.</summary>
public sealed class RunProductImportJobHandlerDefinition(IOptions<ProductsOptions> options)
    : ConsumerDefinition<RunProductImportJobHandler>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<RunProductImportJobHandler> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.SetConcurrency(options.Value.ImportJobConcurrentMessageLimit);
    }
}
