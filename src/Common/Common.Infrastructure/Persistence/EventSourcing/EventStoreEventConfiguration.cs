using Common.Domain.Entities;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Persistence.EventSourcing;

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
