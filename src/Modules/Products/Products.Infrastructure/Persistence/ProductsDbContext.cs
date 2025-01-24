using Common.Domain.Events;
using Common.Infrastructure.Options;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Infrastructure.Persistence;

public sealed class ProductsDbContext(
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
    public DbSet<ProductTemplate> Products => Set<ProductTemplate>();
    public DbSet<Product> StoreProducts => Set<Product>();
}
