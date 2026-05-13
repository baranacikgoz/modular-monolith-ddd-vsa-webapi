using Common.Application.Auth;
using Common.Domain.Events;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Infrastructure.Persistence;

public sealed class ProductsDbContext(
    DbContextOptions<ProductsDbContext> options,
    TimeProvider timeProvider,
    ICurrentUser currentUser,
    ILogger<BaseDbContext> logger,
    EventDispatcher eventDispatcher,
    IntegrationEventOutbox integrationEventOutbox
) : BaseDbContext(options, timeProvider, currentUser, logger, eventDispatcher, integrationEventOutbox), IProductsDbContext
{
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<ProductTemplate> ProductTemplates => Set<ProductTemplate>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(nameof(Products));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductsDbContext).Assembly);

        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfiguration(new AuditLogEntryConfiguration());
    }
}
