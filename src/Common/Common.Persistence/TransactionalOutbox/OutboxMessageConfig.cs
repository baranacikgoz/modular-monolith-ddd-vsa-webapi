using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Persistence.TransactionalOutbox;

public class OutboxMessageConfig : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Event)
            .HasConversion<DomainEventConverter>()
            .IsRequired();

        builder.Property(x => x.CreatedOn).IsRequired();
        builder.HasIndex(x => x.CreatedOn);

        builder.Property(x => x.IsProcessed).IsRequired();
        builder.HasIndex(x => x.IsProcessed);

        builder.Property(x => x.ProcessedOn).IsRequired(false);

        builder.Property(x => x.FailedCount).IsRequired();
    }
}

public class DeadLetterMessageConfig : IEntityTypeConfiguration<DeadLetterMessage>
{
    public void Configure(EntityTypeBuilder<DeadLetterMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Event)
            .HasConversion<DomainEventConverter>()
            .IsRequired();

        builder.Property(x => x.CreatedOn).IsRequired();
        builder.Property(x => x.FailedCount).IsRequired();
    }
}

