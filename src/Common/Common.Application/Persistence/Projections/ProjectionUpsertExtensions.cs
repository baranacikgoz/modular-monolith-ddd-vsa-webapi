using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Common.Application.Persistence.Projections;

public static class ProjectionUpsertExtensions
{
    /// <summary>
    ///     Loads the projection row by primary key (tracked), creates it when missing, and applies
    ///     <paramref name="apply" /> only when <paramref name="incomingVersion" /> is newer than the row's
    ///     <see cref="ProjectionEntity.SourceVersion" />. Saves inside, so two consumers racing on the same missing key are
    ///     resolved: the loser's insert hits the unique constraint, is discarded, and the row is re-read and treated as an
    ///     update. Composite keys pass an <c>object[]</c> as <paramref name="key" />. Only a violation of the projection's
    ///     own primary key is treated as that race; any other unique violation (a secondary index, the consumer inbox
    ///     key tracked in the same context) propagates to the caller.
    /// </summary>
    public static async Task<ProjectionUpsertOutcome> UpsertIfNewerAsync<TProjection, TKey>(
        this DbSet<TProjection> set,
        TKey key,
        long incomingVersion,
        Func<TProjection> create,
        Action<TProjection> apply,
        CancellationToken cancellationToken)
        where TProjection : ProjectionEntity
        where TKey : notnull
    {
        var db = set.GetService<ICurrentDbContext>().Context;
        var keyValues = key as object[] ?? [key];

        // FindAsync is the one primary-key lookup that consults the change tracker first, which is what makes the
        // re-read after a lost insert race see the row instead of the discarded local instance.
        var existing = await set.FindAsync(keyValues, cancellationToken);

        if (existing is null)
        {
            var created = create();
            created.AcceptVersion(incomingVersion);
            apply(created);
            set.Add(created);

            try
            {
                await db.SaveChangesAsync(cancellationToken);
                return ProjectionUpsertOutcome.Inserted;
            }
            catch (UniqueConstraintException ex) when (IsPrimaryKeyOf<TProjection>(db, ex))
            {
                db.Entry(created).State = EntityState.Detached;
                existing = await set.FindAsync(keyValues, cancellationToken)
                           ?? throw new InvalidOperationException(
                               $"{typeof(TProjection).Name} insert lost a race but the winning row is not readable.");
            }
        }

        if (!existing.AcceptVersion(incomingVersion))
        {
            return ProjectionUpsertOutcome.Stale;
        }

        apply(existing);
        await db.SaveChangesAsync(cancellationToken);
        return ProjectionUpsertOutcome.Updated;
    }

    private static bool IsPrimaryKeyOf<TProjection>(DbContext db, UniqueConstraintException exception)
    {
        var primaryKeyName = db.Model.FindEntityType(typeof(TProjection))?.FindPrimaryKey()?.GetName();
        return primaryKeyName is not null
               && string.Equals(exception.ConstraintName, primaryKeyName, StringComparison.Ordinal);
    }
}
