using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Application.Exports;
using Products.Domain.Stores;

namespace Products.Infrastructure.Persistence.EntityConfigurations;

internal sealed class ProductExportJobConfiguration : JobRowConfiguration<ProductExportJob, ProductExportJobId>
{
    public override void Configure(EntityTypeBuilder<ProductExportJob> builder)
    {
        base.Configure(builder);

        builder.ToTable("ExportJobs");

        builder
            .Property(j => j.StoreId)
            .HasConversion<StronglyTypedIdValueConverter<StoreId>>()
            .IsRequired(false);

        builder.Property(j => j.FileName).HasMaxLength(ProductExportJob.FileNameMaxLength);
        builder.Property(j => j.ObjectKey).HasMaxLength(ProductExportJob.ObjectKeyMaxLength);

        // At most one Queued (0) or Running (1) export per user: a double click collides here and the loser gets the
        // winner's job back. JobStatus is stored as an integer, hence the literals.
        builder
            .HasIndex(j => j.RequestedBy, "UX_ExportJobs_RequestedBy_Active")
            .IsUnique()
            .HasFilter("\"Status\" IN (0, 1)");

        // The poll endpoints: my jobs, newest first.
        builder.HasIndex(j => new { j.RequestedBy, j.QueuedOn });
    }
}
