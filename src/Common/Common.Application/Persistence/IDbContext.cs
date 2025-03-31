using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

// This is not the best way, and actually I hate it, but I don't want to move all DbContext, EntityConfigurations, and Migrations to Application layer.

namespace Common.Application.Persistence;

public interface IDbContext : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    ChangeTracker ChangeTracker { get; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}
