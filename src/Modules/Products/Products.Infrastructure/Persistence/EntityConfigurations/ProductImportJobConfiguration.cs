using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Application.Imports;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Infrastructure.Persistence.EntityConfigurations;

internal sealed class ProductImportJobConfiguration : JobRowConfiguration<ProductImportJob, ProductImportJobId>
{
    public override void Configure(EntityTypeBuilder<ProductImportJob> builder)
    {
        base.Configure(builder);

        builder.ToTable("ImportJobs");

        builder
            .Property(j => j.StoreId)
            .HasConversion<StronglyTypedIdValueConverter<StoreId>>()
            .IsRequired();

        builder
            .Property(j => j.ProductTemplateId)
            .HasConversion<StronglyTypedIdValueConverter<ProductTemplateId>>()
            .IsRequired();

        builder.Property(j => j.ClientRequestId).HasMaxLength(ProductImportJob.ClientRequestIdMaxLength).IsRequired();
        builder.Property(j => j.FileName).HasMaxLength(ProductImportJob.FileNameMaxLength).IsRequired();
        builder.Property(j => j.ObjectKey).HasMaxLength(ProductImportJob.ObjectKeyMaxLength).IsRequired();
        builder.Property(j => j.UiCulture).HasMaxLength(ProductImportJob.UiCultureMaxLength).IsRequired();

        // Only ever read back whole by the poll endpoints, never queried.
        builder
            .Property(j => j.RowErrors)
            .HasJsonbList()
            .HasDefaultValueSql("'[]'::jsonb");

        // The client's own retry token: a repeat of the same id by the same user is the first job.
        builder
            .HasIndex(j => new { j.RequestedBy, j.ClientRequestId }, "UX_ImportJobs_RequestedBy_ClientRequestId")
            .IsUnique();

        // At most one Queued (0) or Running (1) import per user: a double click with a new client request id collides
        // here and the loser gets the winner's job back. JobStatus is stored as an integer, hence the literals.
        builder
            .HasIndex(j => j.RequestedBy, "UX_ImportJobs_RequestedBy_Active")
            .IsUnique()
            .HasFilter("\"Status\" IN (0, 1)");

        // The list endpoint: my jobs, newest first.
        builder.HasIndex(j => new { j.RequestedBy, j.QueuedOn });
    }
}
