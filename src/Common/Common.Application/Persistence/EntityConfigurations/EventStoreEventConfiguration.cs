using Common.Application.ValueConverters;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Application.Persistence.EntityConfigurations;

public class EventStoreEventConfiguration : AuditableEntityConfiguration<EventStoreEvent>
{
    public override void Configure(EntityTypeBuilder<EventStoreEvent> builder)
    {
        base.Configure(builder);

        builder.HasKey(storeEvent => new { storeEvent.AggregateId, storeEvent.Version });

        builder
            .Property(storeEvent => storeEvent.AggregateType)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .HasIndex(storeEvent => storeEvent.AggregateType);

        builder
            .Property(storeEvent => storeEvent.AggregateId)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.EventType)
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.Event)
            .HasColumnType("jsonb")
            .HasConversion<DomainEventConverter>()
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.Version)
            .IsRequired();

        builder
            .HasIndex(storeEvent => storeEvent.CreatedBy);
    }
}
