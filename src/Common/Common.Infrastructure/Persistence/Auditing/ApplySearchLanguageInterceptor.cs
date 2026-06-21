using Common.Application.Options;
using Common.Application.Search;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Common.Infrastructure.Persistence.Auditing;

/// <summary>
/// Stamps the per-row search <c>Language</c> config on inserted <see cref="ISearchLocalized"/> entities, from the
/// current request culture via <see cref="ISearchLanguageResolver"/>. Mirrors <see cref="ApplyAuditingInterceptor"/>
/// so the domain stays pure (no ambient-culture reads, no extra domain event).
/// </summary>
public class ApplySearchLanguageInterceptor(ISearchLanguageResolver resolver) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        foreach (var entry in dbContext
                     .ChangeTracker
                     .Entries<ISearchLocalized>()
                     .Where(e => e.State == EntityState.Added))
        {
            // Set via the change tracker so a private setter is honoured without exposing one publicly.
            entry.Property(FullTextSearchOptions.LanguageColumn).CurrentValue = resolver.ResolveConfig();
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
