using Common.Application.Auth;
using Common.Domain.Events;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Domain.Products;
using Products.Domain.StoreProducts;
using Products.Domain.Stores;

namespace Products.Infrastructure.Persistence;

internal sealed class ProductsDbContext(
    DbContextOptions<ProductsDbContext> options,
    ICurrentUser currentUser,
    ILogger<ProductsDbContext> logger
    ) : BaseDbContext(options, currentUser, logger)
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
