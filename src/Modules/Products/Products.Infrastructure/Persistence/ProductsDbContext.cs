using Common.Application.Options;
using Common.Application.Persistence;
using Common.Application.Persistence.EntityConfigurations;
using Common.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Application.Persistence;

namespace Products.Infrastructure.Persistence;

public sealed class ProductsDbContext(
    DbContextOptions<ProductsDbContext> options,
    ILogger<ProductsDbContext> logger,
    IOptions<ObservabilityOptions> observabilityOptionsProvider
    ) : BaseDbContext(options, logger, observabilityOptionsProvider), IProductsDbContext
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
    public DbSet<ProductTemplate> ProductTemplates => Set<ProductTemplate>();
    public DbSet<Product> Products => Set<Product>();
}
