using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence.Outbox;

public class OutboxDbContext(DbContextOptions<OutboxDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(nameof(Outbox));

        modelBuilder.ApplyConfiguration(new OutboxMessageConfig());
        modelBuilder.ApplyConfiguration(new DeadLetterMessageConfig());
    }

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<DeadLetterMessage> DeadLetterMessages => Set<DeadLetterMessage>();
}
