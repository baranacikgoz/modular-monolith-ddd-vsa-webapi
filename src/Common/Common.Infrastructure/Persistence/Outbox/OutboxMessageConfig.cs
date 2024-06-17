using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Persistence.Outbox;

public class OutboxMessageConfig : AuditableEntityConfiguration<OutboxMessage>
{
    public override void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.CreatedOn);
        builder.HasIndex(x => x.IsProcessed);

        builder.Property(x => x.Event)
            .HasConversion<DomainEventConverter>()
            .IsRequired();

        builder
            .Property(x => x.IsProcessed)
            .IsRequired();

        builder
            .Property(x => x.ProcessedOn)
            .IsRequired(false);

        builder
            .Property(x => x.FailedCount)
            .IsRequired();
    }
}

public class DeadLetterMessageConfig : AuditableEntityConfiguration<DeadLetterMessage>
{
    public override void Configure(EntityTypeBuilder<DeadLetterMessage> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Event)
            .HasConversion<DomainEventConverter>()
            .IsRequired();

        builder
            .Property(x => x.FailedCount)
            .IsRequired();
    }
}

