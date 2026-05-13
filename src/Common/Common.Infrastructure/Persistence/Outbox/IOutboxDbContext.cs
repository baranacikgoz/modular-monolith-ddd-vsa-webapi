using Common.Application.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence.Outbox;

/// <summary>
///     Abstraction for the Outbox DbContext, used by OutboxProcessor for read/update operations.
/// </summary>
public interface IOutboxDbContext
{
    DbSet<OutboxMessage> OutboxMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
