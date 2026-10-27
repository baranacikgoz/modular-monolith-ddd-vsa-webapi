using Products.Domain.Products;
using Products.Domain.Products.DomainEvents.v1;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Xunit;

namespace Products.Tests.Products;

public class ProductSkuTests
{
    private readonly StoreId _storeId = StoreId.New();
    private readonly ProductTemplateId _templateId = ProductTemplateId.New();

    [Fact]
    public void Create_WithoutSku_LeavesItEmptyAndRaisesOnlyTheCreatedEvent()
    {
        var product = Product.Create(_storeId, _templateId, "Name", "Description", 1, 10m);

        Assert.Null(product.Sku);
        Assert.IsType<V1ProductCreatedDomainEvent>(Assert.Single(product.Events));
    }

    [Fact]
    public void Create_WithSku_KeepsItAndRaisesTheSkuAssignedEventAfterTheCreatedEvent()
    {
        var product = Product.Create(_storeId, _templateId, "Name", "Description", 1, 10m, "SKU-1");

        Assert.Equal("SKU-1", product.Sku);
        Assert.Collection(
            product.Events,
            first => Assert.IsType<V1ProductCreatedDomainEvent>(first),
            second =>
            {
                var assigned = Assert.IsType<V1ProductSkuAssignedDomainEvent>(second);
                Assert.Equal(product.Id, assigned.ProductId);
                Assert.Equal("SKU-1", assigned.Sku);
                Assert.Equal(2, assigned.Version);
            });
    }
}
