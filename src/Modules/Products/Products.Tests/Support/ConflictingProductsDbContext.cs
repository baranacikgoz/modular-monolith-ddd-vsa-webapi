using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Products.Application.Exports;
using Products.Application.Imports;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Tests.Support;

/// <summary>
///     Wraps a real context and runs <paramref name="beforeFirstSave" /> right before its first save, so a test can play
///     the concurrent writer that commits between a runner's read and its save (the save then fails its concurrency token).
/// </summary>
internal sealed class ConflictingProductsDbContext(IProductsDbContext inner, Func<Task> beforeFirstSave) : IProductsDbContext
{
    private bool _conflictInjected;

    public DbSet<Store> Stores => inner.Stores;
    public DbSet<ProductTemplate> ProductTemplates => inner.ProductTemplates;
    public DbSet<Product> Products => inner.Products;
    public DbSet<ProductImportJob> ImportJobs => inner.ImportJobs;
    public DbSet<ProductExportJob> ExportJobs => inner.ExportJobs;
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
