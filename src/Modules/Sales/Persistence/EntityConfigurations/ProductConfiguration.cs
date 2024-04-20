using Common.Core.Contracts.Identity;
using Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Features.Products.Domain;
using Sales.Features.Stores.Domain;

namespace Sales.Persistence.EntityConfigurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .Property(a => a.Id)
            .HasConversion<StronglyTypedIdValueConverter<ProductId>>()
            .IsRequired();

        builder
            .Property(a => a.StoreId)
            .HasConversion<StronglyTypedIdValueConverter<StoreId>>()
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

        builder
            .OwnsOne(m => m.Price, p =>
            {
                p.Property(x => x.Amount).IsRequired();
                p.Property(x => x.Currency).IsRequired();
            });

        builder
            .Property(storeEvent => storeEvent.CreatedOn)
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.CreatedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired();

        builder
            .Property(storeEvent => storeEvent.LastModifiedOn)
            .IsRequired(false);

        builder
            .Property(storeEvent => storeEvent.LastModifiedBy)
            .HasConversion<StronglyTypedIdValueConverter<ApplicationUserId>>()
            .IsRequired(false);
    }
}
