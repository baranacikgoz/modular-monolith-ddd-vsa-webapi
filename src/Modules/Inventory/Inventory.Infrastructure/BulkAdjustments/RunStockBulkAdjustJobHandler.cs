using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Infrastructure.BulkAdjustments;

/// <summary>The queue consumer behind every bulk adjustment message (the first one and every continuation).</summary>
public sealed class RunStockBulkAdjustJobHandler(
    StockBulkAdjustJobRunner runner,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox,
    ILogger<RunStockBulkAdjustJobHandler> logger
) : IntegrationEventHandlerBase<StockBulkAdjustRequestedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override Task ProcessAsync(StockBulkAdjustRequestedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        return runner.RunAsync(new StockBulkAdjustJobId(@event.JobId), cancellationToken);
    }
}

/// <summary>Bounds how many bulk adjustment messages one pod works on at once: memory per pod is roughly this limit times one
/// message's slice of rows. The prefetch count equals the limit: a prefetched message waiting for a slot sits unacked
/// and would trip the broker's consumer timeout.</summary>
public sealed class RunStockBulkAdjustJobHandlerDefinition(IOptions<InventoryOptions> options)
    : ConsumerDefinition<RunStockBulkAdjustJobHandler>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<RunStockBulkAdjustJobHandler> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.SetConcurrency(options.Value.BulkAdjustJobConcurrentMessageLimit);
    }
}
