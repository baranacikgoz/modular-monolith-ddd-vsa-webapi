using Common.Application.Persistence.EntityConfigurations;
using Common.Application.ValueConverters;
using Common.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Outbox.Persistence;

public class OutboxMessageConfig : AuditableEntityConfiguration<OutboxMessage>
{
    public override void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.CreatedOn, x.IsProcessed });

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
