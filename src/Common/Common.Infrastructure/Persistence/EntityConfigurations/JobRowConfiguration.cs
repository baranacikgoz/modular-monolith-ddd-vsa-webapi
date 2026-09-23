using Common.Application.Jobs;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
///     Shared mapping for every <see cref="JobRow{TId}" /> table: key and audit columns, <c>RequestedBy</c> conversion,
///     <c>FailureKey</c> length, and the indexes housekeeping and claims rely on. A module configuration derives from it
///     and adds the job's own columns.
/// </summary>
public abstract class JobRowConfiguration<TJob, TId> : AuditableEntityConfiguration<TJob, TId>
    where TJob : JobRow<TId>
    where TId : IStronglyTypedId, new()
{
    public override void Configure(EntityTypeBuilder<TJob> builder)
    {
        base.Configure(builder);

        builder
            .Property(j => j.RequestedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired(false);

        builder
            .Property(j => j.Status)
            .IsRequired();

        builder
            .Property(j => j.QueuedOn)
            .IsRequired();

        builder
            .Property(j => j.FailureKey)
            .HasMaxLength(JobRow<TId>.FailureKeyMaxLength);

        // Stale sweep: Running rows by start time.
        builder.HasIndex(j => new { j.Status, j.StartedOn });

        // Retention sweep: finished rows by finish time.
        builder.HasIndex(j => j.FinishedOn);
    }
}
