using Common.Application.Options;
using Common.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
