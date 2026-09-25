using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Bogus;
using Common.Application.AuditLog;
using Common.Application.Pagination;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Domain.Products;
using Products.Domain.Products.DomainEvents.v1;
using Products.Domain.Stores;
using Xunit;

namespace Products.Tests.Endpoints.Products;

[Collection("IntegrationTestCollection")]
public class AuditLogTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public AuditLogTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task AuditLog_WithExistingProduct_ReturnsOkWithPaginationResponse()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var store = Store.Create(new ApplicationUserId(Guid.NewGuid()),
            _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        var product = Product.Create(store.Id, template.Id, "Product", "Desc", 10, 10m);
        store.AddProduct(product);
        db.Stores.Add(store);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/products/{product.Id}/audit-log?PageNumber=1&PageSize=10", UriKind.Relative));

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<AuditLogDto>>(JsonSerializerOptions);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task AuditLog_WhenEventWasWrittenWithoutUser_ReturnsNullCreatedBy()
    {
        // Arrange: written outside an HTTP request, so there is no current user and the row's CreatedBy is null.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var store = Store.Create(new ApplicationUserId(Guid.NewGuid()),
            _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        var product = Product.Create(store.Id, template.Id, "Product", "Desc", 10, 10m);
        store.AddProduct(product);
        db.Stores.Add(store);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/products/{product.Id}/audit-log?PageNumber=1&PageSize=10", UriKind.Relative));

        // Assert: a system-written row must read as null, not as the empty uuid.
        response.EnsureSuccessStatusCode();
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement.GetProperty("data").EnumerateArray().ToList();
        Assert.NotEmpty(items);
        Assert.All(items, item => Assert.Equal(JsonValueKind.Null, item.GetProperty("createdBy").ValueKind));
    }

    [Fact]
    public async Task AuditLog_WhenRowsShareCreatedOn_OrdersByVersionDescendingAcrossPages()
    {
        // Arrange: one SaveChanges stamps every row with the same CreatedOn, like one command that raises several events.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var productId = ProductId.New();
        for (var version = 1; version <= 5; version++)
        {
            db.AuditLog.Add(AuditLogEntry.Create(nameof(Product), productId.Value, version,
                new V1ProductNameUpdatedDomainEvent(productId, $"name-{version}")));
        }

        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act: two pages, so an unstable tie order would show up as a repeated or skipped row.
        var first = await GetVersionsAsync(client, productId, pageNumber: 1);
        var second = await GetVersionsAsync(client, productId, pageNumber: 2);

        // Assert
        Assert.Equal([5L, 4L, 3L], first);
        Assert.Equal([2L, 1L], second);
    }

    [Fact]
    public async Task AuditLog_FullPageCarriesACursorThatContinuesInTheSameOrder()
    {
        // Arrange: same instant for every row, the case where the order inside a tie matters most.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var productId = ProductId.New();
        for (var version = 1; version <= 5; version++)
        {
            db.AuditLog.Add(AuditLogEntry.Create(nameof(Product), productId.Value, version,
                new V1ProductNameUpdatedDomainEvent(productId, $"name-{version}")));
        }

        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var first = await GetPageAsync(client, productId, "PageNumber=1&PageSize=3");
        var second = await GetPageAsync(client, productId, $"PageNumber=1&PageSize=3&IncludeTotal=false&After={Uri.EscapeDataString(first.NextCursor!)}");

        // Assert: the cursor page picks up exactly where the offset page ended, and skips the count.
        Assert.Equal([5L, 4L, 3L], first.Versions);
        Assert.NotNull(first.NextCursor);
        Assert.Equal(5, first.TotalCount);
        Assert.Equal([2L, 1L], second.Versions);
        Assert.Null(second.NextCursor);
        Assert.Equal(-1, second.TotalCount);
    }

    [Fact]
    public async Task AuditLog_MalformedCursor_ReturnsBadRequest()
    {
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        var response = await client.GetAsync(new Uri($"/v1/products/{ProductId.New()}/audit-log?PageNumber=1&PageSize=3&After=not-a-cursor", UriKind.Relative));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private static async Task<(List<long> Versions, string? NextCursor, int TotalCount)> GetPageAsync(HttpClient client, ProductId productId, string query)
    {
        var response = await client.GetAsync(new Uri($"/v1/products/{productId}/audit-log?{query}", UriKind.Relative));
        response.EnsureSuccessStatusCode();
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;
        var versions = root.GetProperty("data").EnumerateArray().Select(item => item.GetProperty("version").GetInt64()).ToList();
        var cursor = root.TryGetProperty("nextCursor", out var next) && next.ValueKind == JsonValueKind.String ? next.GetString() : null;
        return (versions, cursor, root.GetProperty("totalCount").GetInt32());
    }

    private static async Task<List<long>> GetVersionsAsync(HttpClient client, ProductId productId, int pageNumber)
    {
        var response = await client.GetAsync(new Uri($"/v1/products/{productId}/audit-log?PageNumber={pageNumber}&PageSize=3", UriKind.Relative));
        response.EnsureSuccessStatusCode();
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("data").EnumerateArray().Select(item => item.GetProperty("version").GetInt64()).ToList();
    }

    [Fact]
    public async Task AuditLog_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var response = await client.GetAsync(new Uri($"/v1/products/{ProductId.New()}/audit-log?PageNumber=1&PageSize=10", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
