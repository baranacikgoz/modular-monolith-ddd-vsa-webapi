using Common.Domain.Entities;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Inventory.Domain.StockCounters;
using Inventory.Domain.StockLevels;
using Inventory.Domain.StockPushes;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WebhookDeliveries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Inventory.Tests.Support;

/// <summary>
///     Wraps a real context and runs <paramref name="beforeFirstSave" /> right before its first save, so a test can play
///     the concurrent writer that commits between a runner's read and its save (the save then fails its concurrency token).
/// </summary>
internal sealed class ConflictingInventoryDbContext(IInventoryDbContext inner, Func<Task> beforeFirstSave) : IInventoryDbContext
{
    private bool _conflictInjected;

    public DbSet<StockLevel> StockLevels => inner.StockLevels;
    public DbSet<StockMovement> StockMovements => inner.StockMovements;
    public DbSet<StockReservation> StockReservations => inner.StockReservations;
    public DbSet<ProductSummary> ProductSummaries => inner.ProductSummaries;
    public DbSet<StoreDailyStockTotal> StoreDailyStockTotals => inner.StoreDailyStockTotals;
    public DbSet<StockPushDirtyItem> StockPushDirtyItems => inner.StockPushDirtyItems;
    public DbSet<StockBulkAdjustJob> StockBulkAdjustJobs => inner.StockBulkAdjustJobs;
    public DbSet<WarehouseConnection> WarehouseConnections => inner.WarehouseConnections;
    public DbSet<WebhookDelivery> WebhookDeliveries => inner.WebhookDeliveries;
    public ChangeTracker ChangeTracker => inner.ChangeTracker;
    public DatabaseFacade Database => inner.Database;
    public DbSet<AuditLogEntry> AuditLog => inner.AuditLog;

    public DbSet<TEntity> Set<TEntity>() where TEntity : class => inner.Set<TEntity>();

    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class => inner.Entry(entity);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (!_conflictInjected)
        {
            _conflictInjected = true;
            await beforeFirstSave();
        }

        return await inner.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        // The wrapped context belongs to the test's scope.
    }
}
