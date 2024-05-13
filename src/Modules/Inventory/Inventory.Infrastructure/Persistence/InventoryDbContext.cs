using Common.Application.Auth;
using Common.Domain.Events;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.EventSourcing;
using Inventory.Domain.Products;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure.Persistence;

internal sealed class InventoryDbContext(
    DbContextOptions<InventoryDbContext> options,
    ICurrentUser currentUser,
    ILogger<InventoryDbContext> logger
    ) : BaseDbContext(options, currentUser, logger)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(nameof(Inventory));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);

        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfiguration(new EventStoreEventConfiguration());
    }

    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<StoreProduct> StoreProducts => Set<StoreProduct>();
}
