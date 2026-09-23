namespace Common.Application.Persistence.Inbox;

/// <summary>
///     Transactional inbox over one module DbContext. <see cref="Mark"/> only tracks the
///     <see cref="ProcessedMessage"/> row; the handler's own <c>SaveChangesAsync</c> commits it together with
///     the side effects, and <see cref="SaveIfPendingAsync"/> covers a handler that saved nothing.
/// </summary>
public interface IInboxStore
{
    void Mark(string consumerName, DefaultIdType messageId);

    /// <summary>Saves only when the row tracked by <see cref="Mark"/> is still unsaved (state Added).</summary>
    Task SaveIfPendingAsync(CancellationToken cancellationToken);

    /// <summary>Deletes up to <paramref name="batchSize"/> rows processed before <paramref name="olderThan"/>.</summary>
    Task<int> CleanupAsync(DateTimeOffset olderThan, int batchSize, CancellationToken cancellationToken);
}

/// <summary>
///     The inbox bound to one module's DbContext interface. Every module DbContext registers one, so a
///     consumer injects the store of its own module (<c>IInboxStore&lt;IInventoryDbContext&gt;</c>) and the
///     inbox row lands in the same DbContext, and therefore the same transaction, as its side effects.
///     The non-generic <see cref="IInboxStore"/> is deliberately not registered: with several module
///     contexts in one process it would resolve to whichever module registered last.
/// </summary>
#pragma warning disable S2326 // TDbContext is the registration key, not used by a member.
public interface IInboxStore<TDbContext> : IInboxStore
    where TDbContext : IDbContext
{
}
#pragma warning restore S2326

/// <summary>One entry per module DbContext, enumerated by the inbox cleanup job.</summary>
public interface IInboxCleanupTarget
{
    string ModuleName { get; }

    Task<int> CleanupAsync(DateTimeOffset olderThan, int batchSize, CancellationToken cancellationToken);
}
