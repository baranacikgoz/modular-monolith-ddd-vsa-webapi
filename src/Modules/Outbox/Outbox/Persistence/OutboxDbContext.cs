using Common.Application.Persistence.Outbox;
using Common.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Outbox.Persistence;

public class OutboxDbContext(
    DbContextOptions<OutboxDbContext> options)
    : DbContext(options), IOutboxDbContext
{
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<IntegrationEventOutboxMessage> IntegrationEventOutboxMessages => Set<IntegrationEventOutboxMessage>();

    DatabaseFacade IOutboxDbContext.Database => Database;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(nameof(Outbox));

        modelBuilder.ApplyConfiguration(new OutboxMessageConfig());
        modelBuilder.ApplyConfiguration(new IntegrationEventOutboxMessageConfig());
    }
}
