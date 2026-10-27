using Common.Application.Auth;
using Common.Domain.Events;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Inventory.Domain.StockCounters;
using Inventory.Domain.StockLevels;
using Inventory.Domain.StockPushes;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WebhookDeliveries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure.Persistence;

public sealed class InventoryDbContext(
    DbContextOptions<InventoryDbContext> options,
    TimeProvider timeProvider,
    ICurrentUser currentUser,
    ILogger<BaseDbContext> logger,
    EventDispatcher eventDispatcher,
    IntegrationEventOutbox integrationEventOutbox
) : BaseDbContext(options, timeProvider, currentUser, logger, eventDispatcher, integrationEventOutbox), IInventoryDbContext
{
    public DbSet<StockLevel> StockLevels => Set<StockLevel>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();
    public DbSet<StockReservation> StockReservations => Set<StockReservation>();
    public DbSet<ProductSummary> ProductSummaries => Set<ProductSummary>();
    public DbSet<StoreDailyStockTotal> StoreDailyStockTotals => Set<StoreDailyStockTotal>();
    public DbSet<StockPushDirtyItem> StockPushDirtyItems => Set<StockPushDirtyItem>();
    public DbSet<WarehouseConnection> WarehouseConnections => Set<WarehouseConnection>();
    public DbSet<WebhookDelivery> WebhookDeliveries => Set<WebhookDelivery>();

    /// <summary>
    ///     The ledger is append-only: a tracked change to an existing movement fails before anything is written. Domain event
    ///     handlers run before the base save opens its transaction, so the transaction is opened here instead: a handler that
    ///     writes with SQL of its own (the stock push mark) then commits or rolls back with the change that raised the event.
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var rewritten = ChangeTracker.Entries<StockMovement>()
            .FirstOrDefault(e => e.State is EntityState.Modified or EntityState.Deleted);
        if (rewritten is not null)
        {
            throw new InvalidOperationException(
                $"StockMovement {rewritten.Entity.Id} is part of the immutable ledger and cannot be saved as {rewritten.State}.");
        }

        if (Database.CurrentTransaction is not null)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
        var saved = await base.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return saved;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(nameof(Inventory));
        modelBuilder.HasSequence<long>(StockPushDirtyItem.MarkVersionSequence);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);

        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfiguration(new AuditLogEntryConfiguration());
    }
}
