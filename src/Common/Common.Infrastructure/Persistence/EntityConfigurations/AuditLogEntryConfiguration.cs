using Common.Infrastructure.Persistence.ValueConverters;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Persistence.EntityConfigurations;

public class AuditLogEntryConfiguration : AuditableEntityConfiguration<AuditLogEntry>
{
    public override void Configure(EntityTypeBuilder<AuditLogEntry> builder)
    {
        base.Configure(builder);

        builder.ToTable("AuditLog");

        builder.HasKey(entry => new { entry.AggregateId, entry.Version });

        builder
            .Property(entry => entry.AggregateType)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .HasIndex(entry => new { entry.AggregateId, entry.AggregateType, entry.CreatedOn })
            .IsDescending(false, false, true)
            .HasDatabaseName("IX_AuditLog_AggregateId_AggregateType_CreatedOn");

        builder
            .Property(entry => entry.AggregateId)
            .IsRequired();

        builder
            .Property(entry => entry.EventType)
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(entry => entry.Event)
            .HasColumnType("jsonb")
            .HasConversion<DomainEventConverter>()
            .IsRequired();

        builder
            .Property(entry => entry.Version)
            .IsRequired();
    }
}
