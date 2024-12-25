using Common.Infrastructure.Options;
using Common.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Outbox.Persistence;

public class OutboxDbContext(
    DbContextOptions<OutboxDbContext> options,
    IOptions<ObservabilityOptions> observabilityOptionsProvider,
    ILogger<OutboxDbContext> logger)
    : DbContext(options), IOutboxDbContext
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (observabilityOptionsProvider.Value.LogGeneratedSqlQueries)
        {
#pragma warning disable
            optionsBuilder.LogTo(
            sql => logger.LogDebug(sql),                  // Log the SQL query
            new[] { DbLoggerCategory.Database.Command.Name }, // Only log database commands
            LogLevel.Information                           // Set the log level
            );
#pragma warning restore
        }
    }
}
