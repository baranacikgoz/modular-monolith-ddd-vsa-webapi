using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using Common.Infrastructure.Persistence.ValueConverters;
using Inventory.Domain.Products;
using Inventory.Domain.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.EntityConfigurations;

internal class ProductConfiguration : AuditableEntityConfiguration<Product, ProductId>
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
