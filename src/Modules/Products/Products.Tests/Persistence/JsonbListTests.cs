using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Persistence;

/// <summary>A jsonb list column is read and written whole. The value comparer compares by content, so a list rebuilt with the
/// same items is no change and the row is not written again.</summary>
public class JsonbListTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private async Task<ProductId> SeedProductWithAttributesAsync()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "Lamp");
        var product = seeded.Products[0];
        product.SetAttributes([new ProductAttribute("Color", "Red"), new ProductAttribute("Size", "L")], maxAttributes: 10);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
        return product.Id;
    }

    [Fact]
    public async Task Attributes_WrittenWhole_AreReadBackInTheSameOrder()
    {
        var id = await SeedProductWithAttributesAsync();

        using var scope = Factory.Services.CreateScope();
        var product = await scope.ServiceProvider.GetRequiredService<IProductsDbContext>().Products.AsNoTracking().SingleAsync(p => p.Id == id);

        Assert.Equal([new ProductAttribute("Color", "Red"), new ProductAttribute("Size", "L")], product.Attributes);
    }

    [Fact]
    public async Task Attributes_AListRebuiltWithTheSameItems_DoesNotMarkTheRowModified()
    {
        var id = await SeedProductWithAttributesAsync();
        var product = await Db.Products.SingleAsync(p => p.Id == id);
        var attributes = Db.Entry(product).Property(p => p.Attributes);

        attributes.CurrentValue = product.Attributes.ToList();
        Db.ChangeTracker.DetectChanges();

        Assert.False(attributes.IsModified);
        Assert.Equal(EntityState.Unchanged, Db.Entry(product).State);
    }

    [Fact]
    public async Task Attributes_AListWithDifferentItems_MarksTheColumnModified()
    {
        var id = await SeedProductWithAttributesAsync();
        var product = await Db.Products.SingleAsync(p => p.Id == id);
        var attributes = Db.Entry(product).Property(p => p.Attributes);

        attributes.CurrentValue = new List<ProductAttribute> { new("Color", "Blue"), new("Size", "L") };
        Db.ChangeTracker.DetectChanges();

        Assert.True(attributes.IsModified);
    }

    [Fact]
    public async Task Attributes_OfAProductCreatedWithoutAny_AreEmptyNotNull()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "Lamp");
        Db.ChangeTracker.Clear();

        var product = await Db.Products.AsNoTracking().SingleAsync(p => p.Id == seeded.Products[0].Id);

        Assert.Empty(product.Attributes);
    }
}
