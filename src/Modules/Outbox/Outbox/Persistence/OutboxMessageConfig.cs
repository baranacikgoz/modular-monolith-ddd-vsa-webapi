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

        builder.HasIndex(x => new { x.IsProcessed, x.FailedOn, x.CreatedOn });

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
            .Property(x => x.TraceId)
            .HasMaxLength(32)
            .IsRequired(false);

        builder
            .Property(x => x.ParentSpanId)
            .HasMaxLength(16)
            .IsRequired(false);
    }
}
