﻿using Common.Application.Persistence.EntityConfigurations;
using Common.Application.ValueConverters;
using Common.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Outbox.Persistence;

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

