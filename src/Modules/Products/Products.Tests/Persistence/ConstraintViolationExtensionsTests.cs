using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.Products.Errors;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Infrastructure.Persistence;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Persistence;

public class ConstraintViolationExtensionsTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    /// <summary>One writer in a scope of its own, held at the start line until the test releases every writer at once.</summary>
    private async Task<Result> SaveNewProductAsync(StoreId storeId, ProductTemplateId templateId, string? sku, Task startLine)
    {
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        db.Products.Add(Product.Create(storeId, templateId, "Lamp", "Description", 1, 10m, sku));
        await startLine;
        return await db.SaveChangesDetectingConflictAsync(
            ProductErrors.SkuAlreadyExists, ProductErrors.ParentNoLongerExists, CancellationToken.None);
    }

    [Fact]
    public async Task SaveChangesDetectingConflict_TwoConcurrentSavesOfTheSameSku_OneWinsAndTheOtherGetsTheUniqueError()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser);
        var startLine = new TaskCompletionSource();

        var saves = new[]
        {
            SaveNewProductAsync(seeded.Store.Id, seeded.Template.Id, "SKU-1", startLine.Task),
            SaveNewProductAsync(seeded.Store.Id, seeded.Template.Id, "SKU-1", startLine.Task)
        };
        startLine.SetResult();
        var results = await Task.WhenAll(saves);

        Assert.Single(results, r => !r.IsFailure);
        Assert.Same(ProductErrors.SkuAlreadyExists, Assert.Single(results, r => r.IsFailure).Error);
        Assert.Equal(1, await Db.Products.CountAsync(p => p.Sku == "SKU-1"));
    }

    [Fact]
    public async Task SaveChangesDetectingConflict_ProductsWithoutASku_NeverConflict()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser);
        var startLine = new TaskCompletionSource();

        var saves = new[]
        {
            SaveNewProductAsync(seeded.Store.Id, seeded.Template.Id, null, startLine.Task),
            SaveNewProductAsync(seeded.Store.Id, seeded.Template.Id, null, startLine.Task)
        };
        startLine.SetResult();
        var results = await Task.WhenAll(saves);

        Assert.All(results, r => Assert.False(r.IsFailure));
    }

    [Fact]
    public async Task SaveChangesDetectingConflict_ProductOfAStoreThatIsGone_ReturnsTheForeignKeyError()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser);

        var result = await SaveNewProductAsync(StoreId.New(), seeded.Template.Id, "SKU-1", Task.CompletedTask);

        Assert.Same(ProductErrors.ParentNoLongerExists, result.Error);
    }
}
