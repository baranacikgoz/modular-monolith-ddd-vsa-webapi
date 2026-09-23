using Common.Application.Auth;
using Common.Domain.Entities;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Persistence.Inbox;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<DateTimeOffset>().HaveConversion<UtcDateTimeOffsetConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Every module DbContext carries the consumer inbox; the table resolves to the schema the module
        // sets with HasDefaultSchema in its own OnModelCreating.
        modelBuilder.ApplyConfiguration(new ProcessedMessageConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await OutboxSaveHelper.SaveWithOutboxAsync(
            this, timeProvider, currentUser, logger,
            eventDispatcher, integrationEventOutbox,
            ct => base.SaveChangesAsync(ct),
            cancellationToken);
    }
}
