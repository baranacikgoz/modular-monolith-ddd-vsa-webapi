using Common.Application.Auth;
using Common.Domain.Events;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Inventory.Domain.StockLevels;
using Inventory.Domain.StockReservations;
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

    /// <summary>The ledger is append-only: a tracked change to an existing movement fails before anything is written.</summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var rewritten = ChangeTracker.Entries<StockMovement>()
            .FirstOrDefault(e => e.State is EntityState.Modified or EntityState.Deleted);
        if (rewritten is not null)
        {
            throw new InvalidOperationException(
                $"StockMovement {rewritten.Entity.Id} is part of the immutable ledger and cannot be saved as {rewritten.State}.");
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(nameof(Inventory));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);

        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfiguration(new AuditLogEntryConfiguration());
    }
}
