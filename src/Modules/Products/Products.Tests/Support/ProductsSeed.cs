using Common.Domain.StronglyTypedIds;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Tests.Support;

internal sealed record SeededStore(Store Store, ProductTemplate Template, IReadOnlyList<Product> Products);

internal static class ProductsSeed
{
    /// <summary>A store owned by <paramref name="owner" /> with one product per name, created the way the add-product
    /// endpoints do (the store adopts the product), all in one save.</summary>
    public static async Task<SeededStore> StoreWithProductsAsync(
        IProductsDbContext db, ApplicationUserId owner, params string[] productNames)
    {
        var store = Store.Create(owner, $"Store {Guid.NewGuid():N}", "Description", "Address");
        var template = ProductTemplate.Create("Brand", "Model", "Color");
        db.Stores.Add(store);
        db.ProductTemplates.Add(template);

        var products = new List<Product>();
        foreach (var name in productNames)
        {
            var product = Product.Create(store.Id, template.Id, name, "Description", 5, 10m);
            store.AddProduct(product);
            products.Add(product);
        }

        await db.SaveChangesAsync();
        return new SeededStore(store, template, products);
    }

    public static async Task<ProductTemplate> TemplateAsync(IProductsDbContext db)
    {
        var template = ProductTemplate.Create("Brand", "Model", "Color");
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();
        return template;
    }
}
