using Common.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.Products;

namespace Products.Infrastructure.Persistence.EntityConfigurations;

internal sealed class ProductConfiguration : AuditableEntityConfiguration<Product, ProductId>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder
            .Property(a => a.Name)
            .HasMaxLength(Constants.NameMaxLength)
            .IsRequired();

        builder
            .Property(a => a.Description)
            .HasMaxLength(Constants.DescriptionMaxLength)
            .IsRequired();
    }
}
