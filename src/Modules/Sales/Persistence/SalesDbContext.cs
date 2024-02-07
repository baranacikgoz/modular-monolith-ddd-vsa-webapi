using Common.Core.Auth;
using Common.EventBus.Contracts;
using Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sales.Features.Products.Domain;
using Sales.Features.Stores.Domain;

namespace Sales.Persistence;

internal sealed class SalesDbContext(
    DbContextOptions<SalesDbContext> options,
    ICurrentUser currentUser,
    IEventBus eventBus,
    ILogger<SalesDbContext> logger
    ) : BaseDbContext(options, currentUser, eventBus, logger)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(nameof(Sales));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
    }

    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Product> Products => Set<Product>();
}
