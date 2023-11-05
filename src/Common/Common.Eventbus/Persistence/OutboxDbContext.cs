using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Common.Eventbus.Persistence;

public class OutboxDbContext(DbContextOptions<OutboxDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(ModuleConstants.OutboxSchemaName);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}
