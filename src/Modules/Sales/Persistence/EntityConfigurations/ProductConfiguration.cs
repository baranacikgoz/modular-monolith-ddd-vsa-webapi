using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Features.Products.Domain;

namespace Sales.Persistence.EntityConfigurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => new ProductId(value));

        builder
            .Property(a => a.StoreId)
            .IsRequired();

        builder
            .HasOne(a => a.Store)
            .WithMany(a => a.Products)
            .HasForeignKey(a => a.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(Sales.Features.Products.Domain.Constants.ProductNameMaxLength);
    }
}
