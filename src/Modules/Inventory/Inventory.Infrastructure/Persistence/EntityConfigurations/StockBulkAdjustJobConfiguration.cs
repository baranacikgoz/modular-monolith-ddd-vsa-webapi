using System.Text.Json;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Inventory.Application.BulkAdjustments;
using Inventory.Domain.StockLevels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal sealed class StockBulkAdjustJobConfiguration : JobRowConfiguration<StockBulkAdjustJob, StockBulkAdjustJobId>
{
    private static readonly JsonSerializerOptions RowErrorsJsonOptions = new(JsonSerializerDefaults.Web);

    public override void Configure(EntityTypeBuilder<StockBulkAdjustJob> builder)
    {
        base.Configure(builder);

        builder.ToTable("StockBulkAdjustJobs");

        builder.Property(j => j.ClientRequestId).HasMaxLength(StockBulkAdjustJob.ClientRequestIdMaxLength).IsRequired();
        builder.Property(j => j.FileName).HasMaxLength(StockBulkAdjustJob.FileNameMaxLength).IsRequired();
        builder.Property(j => j.ObjectKey).HasMaxLength(StockBulkAdjustJob.ObjectKeyMaxLength).IsRequired();
        builder.Property(j => j.Reason).HasMaxLength(Constants.ReasonMaxLength).IsRequired();
        builder.Property(j => j.RequeueCount).IsRequired();

        // Only ever read back whole by the poll endpoints, never queried: jsonb with a plain System.Text.Json converter.
        builder
            .Property(j => j.RowErrors)
            .HasConversion(
                errors => JsonSerializer.Serialize(errors, RowErrorsJsonOptions),
                json => JsonSerializer.Deserialize<List<StockBulkAdjustRowError>>(json, RowErrorsJsonOptions) ?? new List<StockBulkAdjustRowError>(),
                new ValueComparer<IReadOnlyList<StockBulkAdjustRowError>>(
                    (a, b) => a!.SequenceEqual(b!),
                    errors => errors.Aggregate(0, (hash, e) => HashCode.Combine(hash, e.GetHashCode())),
                    errors => errors.ToList()))
            .HasColumnType("jsonb")
            .HasDefaultValueSql("'[]'::jsonb")
            .IsRequired();

        // The client's own retry token: a repeat of the same id by the same user is the first job.
        builder
            .HasIndex(j => new { j.RequestedBy, j.ClientRequestId }, "UX_StockBulkAdjustJobs_RequestedBy_ClientRequestId")
            .IsUnique();

        // At most one Queued (0) or Running (1) bulk adjustment per user: a double click with a new client request id
        // collides here and the loser gets the winner's job back. JobStatus is stored as an integer, hence the literals.
        builder
            .HasIndex(j => j.RequestedBy, "UX_StockBulkAdjustJobs_RequestedBy_Active")
            .IsUnique()
            .HasFilter("\"Status\" IN (0, 1)");

        // The list endpoint: my jobs, newest first.
        builder.HasIndex(j => new { j.RequestedBy, j.QueuedOn });
    }
}
