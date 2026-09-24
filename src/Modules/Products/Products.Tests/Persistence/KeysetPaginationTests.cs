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
public class KeysetPaginationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task PaginateAsync_SecondPageViaCursor_EqualsSecondPageViaOffset_StringTiebreaker()
    {
        var (db, storeId) = await SeedAsync(count: 7, price: _ => 10m);
        var query = db.Products.AsNoTracking().Where(p => p.StoreId == storeId);

        var first = await query.PaginateAsync(p => p.Name, new PageRequest { PageNumber = 1, PageSize = 3 },
            orderBy: p => p.Price, tiebreaker: p => p.Name);
        var secondByOffset = await query.PaginateAsync(p => p.Name, new PageRequest { PageNumber = 2, PageSize = 3 },
            orderBy: p => p.Price, tiebreaker: p => p.Name);
        var secondByCursor = await query.PaginateAsync(p => p.Name,
            new PageRequest { PageNumber = 1, PageSize = 3, After = first.Value!.NextCursor },
            orderBy: p => p.Price, tiebreaker: p => p.Name);

        Assert.NotNull(first.Value.NextCursor);
        Assert.Equal(["P1", "P2", "P3"], first.Value.Data);
        Assert.Equal(secondByOffset.Value!.Data, secondByCursor.Value!.Data);
        Assert.Equal(["P4", "P5", "P6"], secondByCursor.Value.Data);

        var third = await query.PaginateAsync(p => p.Name,
            new PageRequest { PageNumber = 1, PageSize = 3, After = secondByCursor.Value.NextCursor },
            orderBy: p => p.Price, tiebreaker: p => p.Name);
        Assert.Equal(["P7"], third.Value!.Data);
        Assert.Null(third.Value.NextCursor);
    }

    [Fact]
    public async Task PaginateAsync_DescendingSortWithStronglyTypedIdTiebreaker_WalksEveryRowOnce()
    {
        // Prices tie in pairs so the cursor must fall back to the id to split a page inside a tie.
        var (db, storeId) = await SeedAsync(count: 6, price: i => 100m - (i / 2 * 10));
        var query = db.Products.AsNoTracking().Where(p => p.StoreId == storeId);

        var seen = new List<ProductId>();
        string? cursor = null;
        var pages = 0;
        do
        {
            var page = await query.PaginateAsync(p => p.Id,
                new PageRequest { PageNumber = 1, PageSize = 4, After = cursor },
                orderByDescending: p => p.Price, tiebreaker: p => p.Id);
            seen.AddRange(page.Value!.Data);
            cursor = page.Value.NextCursor;
            pages++;
        } while (cursor is not null);

        var expected = await query.OrderByDescending(p => p.Price).ThenBy(p => p.Id).Select(p => p.Id).ToListAsync();
        Assert.Equal(2, pages);
        Assert.Equal(expected, seen);
    }

    [Fact]
    public async Task PaginateAsync_DefaultOrdering_CursorUsesCreatedOnDescending()
    {
        var (db, storeId) = await SeedAsync(count: 5, price: _ => 1m);
        var query = db.Products.AsNoTracking().Where(p => p.StoreId == storeId);

        var first = await query.PaginateAsync(p => p.Id, new PageRequest { PageNumber = 1, PageSize = 2 }, tiebreaker: p => p.Id);
        var second = await query.PaginateAsync(p => p.Id,
            new PageRequest { PageNumber = 1, PageSize = 2, After = first.Value!.NextCursor }, tiebreaker: p => p.Id);

        var expected = await query.OrderByDescending(p => p.CreatedOn).ThenBy(p => p.Id).Select(p => p.Id).Take(4).ToListAsync();
        Assert.NotNull(second.Value);
        Assert.Equal(expected, [.. first.Value.Data, .. second.Value.Data]);
    }

    [Fact]
    public async Task PaginateAsync_InvalidCursor_FailsValidationOnAfter()
    {
        var (db, storeId) = await SeedAsync(count: 1, price: _ => 1m);
        var query = db.Products.AsNoTracking().Where(p => p.StoreId == storeId);

        var result = await query.PaginateAsync(p => p.Name,
            new PageRequest { PageNumber = 1, PageSize = 3, After = "definitely-not-a-cursor" },
            orderBy: p => p.Price, tiebreaker: p => p.Name);

        Assert.True(result.IsFailure);
        Assert.Equal("Validation", result.Error!.Key);
        Assert.Contains(PaginationCursor.ParameterName, result.Error.SubErrors!);
    }

    [Fact]
    public async Task PaginateAsync_AfterWithoutTiebreaker_ThrowsArgumentException()
    {
        var (db, storeId) = await SeedAsync(count: 1, price: _ => 1m);
        var query = db.Products.AsNoTracking().Where(p => p.StoreId == storeId);
        var cursor = PaginationCursor.Encode(1m, "P1");

        await Assert.ThrowsAsync<ArgumentException>(() => query.PaginateAsync(p => p.Name,
            new PageRequest { PageNumber = 1, PageSize = 3, After = cursor }, orderBy: p => p.Price));
    }

    [Fact]
    public async Task PaginateAsync_IncludeTotalFalse_ReportsMinusOneAndStillPages()
    {
        var (db, storeId) = await SeedAsync(count: 4, price: _ => 1m);
        var query = db.Products.AsNoTracking().Where(p => p.StoreId == storeId);

        var page = await query.PaginateAsync(p => p.Name,
            new PageRequest { PageNumber = 1, PageSize = 3, IncludeTotal = false }, orderBy: p => p.Name, tiebreaker: p => p.Id);

        Assert.Equal(-1, page.Value!.TotalCount);
        Assert.False(page.Value.HasTotal);
        Assert.Equal(3, page.Value.Data.Count);
        Assert.True(page.Value.HasNext);
        Assert.NotNull(page.Value.NextCursor);
    }

    private async Task<(IProductsDbContext Db, StoreId StoreId)> SeedAsync(int count, Func<int, decimal> price)
    {
        var db = Scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var store = Store.Create(new ApplicationUserId(Guid.NewGuid()), "Store", "Description", "Address");
        var template = ProductTemplate.Create("Brand", "Model", "Color");
        db.Stores.Add(store);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        // Inserted in reverse so physical order never matches the expected order by accident.
        var products = Enumerable.Range(1, count)
            .Select(i => Product.Create(store.Id, template.Id, $"P{i}", "Description", 10, price(i)))
            .Reverse()
            .ToList();
        db.Products.AddRange(products);
        await db.SaveChangesAsync();
        db.ChangeTracker.Clear();

        return (db, store.Id);
    }

    private sealed record PageRequest : PaginationRequest;
}
