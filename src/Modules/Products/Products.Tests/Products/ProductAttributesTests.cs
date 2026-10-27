using Products.Domain.Products;
using Products.Domain.Products.DomainEvents.v1;
using Products.Domain.Products.Errors;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Xunit;

namespace Products.Tests.Products;

public class ProductAttributesTests
{
    private const int MaxAttributes = 3;

    private static Product NewProduct()
    {
        var product = Product.Create(StoreId.New(), ProductTemplateId.New(), "Sample", "Description", 5, 10m);
        product.ClearEvents();
        return product;
    }

    [Fact]
    public void SetAttributes_WithANewList_ReplacesTheWholeListAndRaisesOneEvent()
    {
        var product = NewProduct();
        product.SetAttributes([new ProductAttribute("Color", "Red")], MaxAttributes);
        product.ClearEvents();

        var result = product.SetAttributes([new ProductAttribute("Size", "L"), new ProductAttribute("Material", "Wool")], MaxAttributes);

        Assert.False(result.IsFailure);
        Assert.Equal([new ProductAttribute("Size", "L"), new ProductAttribute("Material", "Wool")], product.Attributes);
        var @event = Assert.IsType<V1ProductAttributesReplacedDomainEvent>(Assert.Single(product.Events));
        Assert.Equal(product.Id, @event.ProductId);
        Assert.Equal(
            [new V1ProductAttributesReplacedDomainEvent.AttributeSnapshot("Size", "L"), new V1ProductAttributesReplacedDomainEvent.AttributeSnapshot("Material", "Wool")],
            @event.Attributes);
    }

    [Fact]
    public void SetAttributes_WithTheSameContent_RaisesNothing()
    {
        var product = NewProduct();
        product.SetAttributes([new ProductAttribute("Color", "Red")], MaxAttributes);
        product.ClearEvents();

        var result = product.SetAttributes([new ProductAttribute("Color", "Red")], MaxAttributes);

        Assert.False(result.IsFailure);
        Assert.Empty(product.Events);
    }

    [Fact]
    public void SetAttributes_WithAnEmptyList_ClearsTheAttributes()
    {
        var product = NewProduct();
        product.SetAttributes([new ProductAttribute("Color", "Red")], MaxAttributes);

        var result = product.SetAttributes([], MaxAttributes);

        Assert.False(result.IsFailure);
        Assert.Empty(product.Attributes);
    }

    [Fact]
    public void SetAttributes_BeyondTheMaximum_FailsAndKeepsTheOldList()
    {
        var product = NewProduct();
        product.SetAttributes([new ProductAttribute("Color", "Red")], MaxAttributes);
        product.ClearEvents();

        var result = product.SetAttributes(
            [new ProductAttribute("A", "1"), new ProductAttribute("B", "2"), new ProductAttribute("C", "3"), new ProductAttribute("D", "4")],
            MaxAttributes);

        Assert.Equal(ProductErrors.TooManyAttributes, result.Error);
        Assert.Equal([new ProductAttribute("Color", "Red")], product.Attributes);
        Assert.Empty(product.Events);
    }

    [Fact]
    public void SetAttributes_WithNamesThatDifferOnlyInCase_FailsWithDuplicateAttributeName()
    {
        var product = NewProduct();

        var result = product.SetAttributes([new ProductAttribute("Color", "Red"), new ProductAttribute("color", "Blue")], MaxAttributes);

        Assert.Equal(ProductErrors.DuplicateAttributeName, result.Error);
        Assert.Empty(product.Attributes);
        Assert.Empty(product.Events);
    }
}
