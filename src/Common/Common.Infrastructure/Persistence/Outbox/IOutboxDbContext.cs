using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Common.Infrastructure.Persistence.Outbox;

/// <summary>
/// This is a hack to be able to move OutboxDbContext to the Modules/Outbox
/// </summary>
public interface IOutboxDbContext
{
    DatabaseFacade Database { get; }
    DbSet<OutboxMessage> OutboxMessages { get; }
    DbSet<DeadLetterMessage> DeadLetterMessages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
