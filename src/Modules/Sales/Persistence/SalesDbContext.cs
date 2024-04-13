using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Persistence;
using Common.Persistence.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sales.Features.Products.Domain;
using Sales.Features.Stores.Domain;

namespace Sales.Persistence;

internal sealed class SalesDbContext(
    DbContextOptions<SalesDbContext> options,
    ICurrentUser currentUser,
    ILogger<SalesDbContext> logger
    ) : BaseDbContext(options, currentUser, logger)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(nameof(Sales));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);

        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfiguration(new EventStoreEventConfiguration());
    }

    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Product> Products => Set<Product>();
}
