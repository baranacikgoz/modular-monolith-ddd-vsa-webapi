using Common.Application.Persistence.Outbox;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Outbox.Persistence;

public class OutboxMessageConfig : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);

        // Partial index for the claim query only: it always filters IsProcessed = false AND FailedOn IS NULL,
        // then ranges on NextRetryAt and orders by CreatedOn. Processed and failed rows (the bulk of the table)
        // never enter the index.
        builder
            .HasIndex(x => new { x.NextRetryAt, x.CreatedOn })
            .HasFilter("\"IsProcessed\" = false AND \"FailedOn\" IS NULL");

        builder
            .Property(x => x.CreatedOn)
            .IsRequired();

        builder.Property(x => x.Event)
            .HasConversion<IntegrationEventConverter>()
            .IsRequired();

        builder
            .Property(x => x.IsProcessed)
            .IsRequired();

        builder
            .Property(x => x.ProcessedOn)
            .IsRequired(false);

        builder
            .Property(x => x.RetryCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder
            .Property(x => x.FailedOn)
            .IsRequired(false);

        builder
            .Property(x => x.NextRetryAt)
            .IsRequired(false);

        builder
            .Property(x => x.TraceId)
            .HasMaxLength(32)
            .IsRequired(false);

        builder
            .Property(x => x.ParentSpanId)
            .HasMaxLength(16)
            .IsRequired(false);
    }
}
