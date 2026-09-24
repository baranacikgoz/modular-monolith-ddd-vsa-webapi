namespace Common.Application.Persistence.Inbox;

/// <summary>
///     Inbox for a module that owns no database (IAM: Keycloak holds every row). There is no transaction to join,
///     so nothing is marked and nothing is cleaned up; such a module's consumers keep only the cache pre-filter of
///     <c>IntegrationEventHandlerBase</c> and must be idempotent on their own (an idempotent third-party call, a
///     conditional write). Register it explicitly in that module's installer; a module with a DbContext gets the
///     transactional <c>IInboxStore&lt;TDbContext&gt;</c> from <c>AddModuleDbContext</c> instead and must never use this.
/// </summary>
public sealed class StatelessInboxStore : IInboxStore
{
    public static StatelessInboxStore Instance { get; } = new();

    public void Mark(string consumerName, DefaultIdType messageId)
    {
        // No database: nothing to mark.
    }

    public Task SaveIfPendingAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<int> CleanupAsync(DateTimeOffset olderThan, int batchSize, CancellationToken cancellationToken)
    {
        return Task.FromResult(0);
    }
}
