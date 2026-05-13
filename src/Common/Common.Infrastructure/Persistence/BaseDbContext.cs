using Common.Application.Auth;
using Common.Domain.Entities;
using Common.Infrastructure.EventBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.Persistence;

public abstract partial class BaseDbContext(
    DbContextOptions options,
    TimeProvider timeProvider,
    ICurrentUser currentUser,
    ILogger<BaseDbContext> logger,
    EventDispatcher eventDispatcher,
    IntegrationEventOutbox integrationEventOutbox
) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<AuditLogEntry> AuditLog => Set<AuditLogEntry>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await OutboxSaveHelper.SaveWithOutboxAsync(
            this, timeProvider, currentUser, logger,
            eventDispatcher, integrationEventOutbox,
            ct => base.SaveChangesAsync(ct),
            cancellationToken);
    }
}
