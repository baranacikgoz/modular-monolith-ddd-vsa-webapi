using Common.Application.Persistence.Outbox;
using Common.Infrastructure.Persistence.Outbox;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Outbox.Persistence;

public class OutboxDbContext(
    DbContextOptions<OutboxDbContext> options)
    : DbContext(options), IOutboxDbContext
{
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<DateTimeOffset>().HaveConversion<UtcDateTimeOffsetConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(nameof(Outbox));

        modelBuilder.ApplyConfiguration(new OutboxMessageConfig());
    }
}
