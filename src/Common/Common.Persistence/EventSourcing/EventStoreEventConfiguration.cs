using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Common.Core.Contracts;
using Common.Core.Contracts.Identity;

namespace Common.Persistence.EventSourcing;

public class EventStoreEventConfiguration : IEntityTypeConfiguration<EventStoreEvent>
{
    public void Configure(EntityTypeBuilder<EventStoreEvent> builder)
    {
        builder.HasKey(storeEvent => new { storeEvent.AggregateId, storeEvent.Version });

        builder
            .Property(storeEvent => storeEvent.AggregateId)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.Event)
            .HasConversion<DomainEventConverter>()
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.Version)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.CreatedOn)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.CreatedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.LastModifiedOn)
            .IsRequired(false);

        builder
            .Property(storeEvent => storeEvent.LastModifiedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired(false);
    }
}
