using Common.Application.Auth;
using Common.Domain.Events;
using Common.Infrastructure.Options;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.Domain.Products;
using Products.Domain.StoreProducts;
using Products.Domain.Stores;

namespace Products.Infrastructure.Persistence;

internal sealed class ProductsDbContext(
    DbContextOptions<ProductsDbContext> options,
    ILogger<ProductsDbContext> logger,
    IOptions<ObservabilityOptions> observabilityOptionsProvider
    ) : BaseDbContext(options, logger, observabilityOptionsProvider)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(nameof(Products));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductsDbContext).Assembly);

        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfiguration(new EventStoreEventConfiguration());
    }

    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<StoreProduct> StoreProducts => Set<StoreProduct>();
}
