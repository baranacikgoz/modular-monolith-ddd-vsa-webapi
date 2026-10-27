using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Infrastructure.StockPushes;

/// <summary>
///     The queue consumer behind every stock push message (the one a quantity change queued, a continuation, and the sweep's).
///     A run that loses the connection's lease acknowledges and does nothing, so the message is harmless however often it is
///     delivered or duplicated.
/// </summary>
public sealed class PushStockToWarehouseHandler(
    StockPushRunner runner,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox,
    ILogger<PushStockToWarehouseHandler> logger
) : IntegrationEventHandlerBase<StockPushDueIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override Task ProcessAsync(StockPushDueIntegrationEvent @event, CancellationToken cancellationToken)
    {
        return runner.RunAsync(new WarehouseConnectionId(@event.ConnectionId), cancellationToken);
    }
}

/// <summary>Bounds how many connections one pod pushes at once. The prefetch count equals the limit: a prefetched message waiting
/// for a slot sits unacked and would trip the broker's consumer timeout.</summary>
public sealed class PushStockToWarehouseHandlerDefinition(IOptions<InventoryOptions> options)
    : ConsumerDefinition<PushStockToWarehouseHandler>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<PushStockToWarehouseHandler> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.SetConcurrency(options.Value.StockPushConcurrentMessageLimit);
    }
}
