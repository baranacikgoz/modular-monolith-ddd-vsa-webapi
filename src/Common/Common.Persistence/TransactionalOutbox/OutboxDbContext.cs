using Microsoft.EntityFrameworkCore;

namespace Common.Persistence.TransactionalOutbox;

public class OutboxDbContext(DbContextOptions<OutboxDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(nameof(TransactionalOutbox));

        modelBuilder.ApplyConfiguration(new OutboxMessageConfig());
        modelBuilder.ApplyConfiguration(new DeadLetterMessageConfig());
    }

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<DeadLetterMessage> DeadLetterMessages => Set<DeadLetterMessage>();
}
