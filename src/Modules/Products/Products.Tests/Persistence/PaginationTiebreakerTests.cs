using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.Extensions;
using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Xunit;

namespace Products.Tests.Persistence;

[Collection("IntegrationTestCollection")]
public class PaginationTiebreakerTests : BaseIntegrationTest
{
    public PaginationTiebreakerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task PaginateAsync_WithTiebreaker_ReturnsTiedRowsInKeyOrderWithoutRepeatingAcrossPages()
    {
        // Arrange: five products with the same price (the sort key) and unique names (the tiebreaker), inserted in
        // reverse name order, so the table's physical order is the opposite of the expected order and an undefined tie
        // order cannot pass by luck.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var store = Store.Create(new ApplicationUserId(Guid.NewGuid()), "Store", "Description", "Address");
        var template = ProductTemplate.Create("Brand", "Model", "Color");
        db.Stores.Add(store);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var products = Enumerable.Range(1, 5)
            .Select(i => Product.Create(store.Id, template.Id, $"P{i}", "Description", 10, 10m))
            .ToList();
        db.Products.AddRange(products.AsEnumerable().Reverse());
        await db.SaveChangesAsync();

        var query = db.Products.AsNoTracking().Where(p => p.StoreId == store.Id);

        // Act
        var first = await query.PaginateAsync(
            p => p.Name, new PageRequest { PageNumber = 1, PageSize = 3 }, orderBy: p => p.Price, tiebreaker: p => p.Name);
        var second = await query.PaginateAsync(
            p => p.Name, new PageRequest { PageNumber = 2, PageSize = 3 }, orderBy: p => p.Price, tiebreaker: p => p.Name);

        // Assert: each row exactly once, in tiebreaker order.
        Assert.Equal(["P1", "P2", "P3"], first.Value!.Data);
        Assert.Equal(["P4", "P5"], second.Value!.Data);
    }

    private sealed record PageRequest : PaginationRequest;
}
