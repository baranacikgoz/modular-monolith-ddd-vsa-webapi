using Common.Application.Persistence;
using Common.Application.Persistence.Inbox;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence.Inbox;

/// <summary>
///     Scoped: shares the module DbContext instance of the consumer's scope, so the row it tracks commits
///     inside the handler's own SaveChangesAsync.
/// </summary>
public sealed class InboxStore<TDbContext>(
    TDbContext db,
    TimeProvider timeProvider,
    string moduleName
) : IInboxStore<TDbContext>, IInboxCleanupTarget
    where TDbContext : IDbContext
{
    private ProcessedMessage? _pending;

    public string ModuleName => moduleName;

    public void Mark(string consumerName, DefaultIdType messageId)
    {
        _pending = ProcessedMessage.Create(consumerName, messageId, timeProvider.GetUtcNow());
        db.Set<ProcessedMessage>().Add(_pending);
    }

    public async Task SaveIfPendingAsync(CancellationToken cancellationToken)
    {
        if (_pending is null || db.Entry(_pending).State != EntityState.Added)
        {
            return;
        }

        await db.SaveChangesAsync(cancellationToken);
    }

    public Task<int> CleanupAsync(DateTimeOffset olderThan, int batchSize, CancellationToken cancellationToken)
        => db.Set<ProcessedMessage>()
            .Where(m => m.ProcessedOn < olderThan)
            .Take(batchSize)
            .ExecuteDeleteAsync(cancellationToken);
}
