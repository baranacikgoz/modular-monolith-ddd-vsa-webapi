using Common.Application.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Common.Infrastructure.Persistence.Outbox;

/// <summary>
///     Abstraction for the Outbox DbContext, used by BaseDbContext to write outbox messages
///     within a shared database transaction, and by OutboxKafkaProcessor for read/update operations.
/// </summary>
public interface IOutboxDbContext
{
    DatabaseFacade Database { get; }
    DbSet<OutboxMessage> OutboxMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
