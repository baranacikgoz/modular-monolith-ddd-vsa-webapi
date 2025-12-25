using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Common.Application.Persistence.Outbox;

/// <summary>
///     This is a hack to be able to move OutboxDbContext to the Modules/Outbox
/// </summary>
public interface IOutboxDbContext
{
    DatabaseFacade Database { get; }
    DbSet<OutboxMessage> OutboxMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
