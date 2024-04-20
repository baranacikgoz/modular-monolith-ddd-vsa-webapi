using Common.Core.Contracts.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Persistence.TransactionalOutbox;

public class OutboxMessageConfig : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.CreatedOn);

        builder.Property(x => x.Event)
            .HasConversion<DomainEventConverter>()
            .IsRequired();

        builder.Property(x => x.IsProcessed).IsRequired();
        builder.HasIndex(x => x.IsProcessed);

        builder.Property(x => x.ProcessedOn).IsRequired(false);

        builder.Property(x => x.FailedCount).IsRequired();

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

public class DeadLetterMessageConfig : IEntityTypeConfiguration<DeadLetterMessage>
{
    public void Configure(EntityTypeBuilder<DeadLetterMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Event)
            .HasConversion<DomainEventConverter>()
            .IsRequired();

        builder.Property(x => x.FailedCount).IsRequired();

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

