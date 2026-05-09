using Common.Application.Persistence.Outbox;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Outbox.Persistence;

public class IntegrationEventOutboxMessageConfig : IEntityTypeConfiguration<IntegrationEventOutboxMessage>
{
    public void Configure(EntityTypeBuilder<IntegrationEventOutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.CreatedOn, x.IsProcessed });

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
    }
}
