using Common.Application.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Outbox.Persistence;

public class OutboxDbContext(
    DbContextOptions<OutboxDbContext> options)
    : DbContext(options), IOutboxDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(nameof(Outbox));

        modelBuilder.ApplyConfiguration(new OutboxMessageConfig());
    }

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
}
