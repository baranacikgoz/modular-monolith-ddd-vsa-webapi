using Common.Application.Persistence.Inbox;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Persistence.Inbox;

/// <summary>
///     Applied by <see cref="BaseDbContext"/> for every module DbContext: the table takes the module's
///     default schema, so each module owns its own inbox.
/// </summary>
public class ProcessedMessageConfiguration : AuditableEntityConfiguration<ProcessedMessage>
{
    public override void Configure(EntityTypeBuilder<ProcessedMessage> builder)
    {
        base.Configure(builder);

        builder.ToTable(ProcessedMessage.TableName);

        builder
            .HasKey(m => new { m.ConsumerName, m.MessageId })
            .HasName(ProcessedMessage.PrimaryKeyName);

        builder
            .Property(m => m.ConsumerName)
            .HasMaxLength(ProcessedMessage.ConsumerNameMaxLength)
            .IsRequired();

        builder
            .Property(m => m.MessageId)
            .IsRequired();

        builder
            .Property(m => m.ProcessedOn)
            .IsRequired();

        // Cleanup deletes by age in batches.
        builder.HasIndex(m => m.ProcessedOn);
    }
}
