using Common.Infrastructure.Persistence.EntityConfigurations;
using Inventory.Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal sealed class ProductConfiguration : AuditableEntityConfiguration<Product, ProductId>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder
            .Property(a => a.Name)
            .HasMaxLength(Domain.Products.Constants.NameMaxLength)
            .IsRequired();

        builder
            .Property(a => a.Description)
            .HasMaxLength(Domain.Products.Constants.DescriptionMaxLength)
            .IsRequired();
    }
}
