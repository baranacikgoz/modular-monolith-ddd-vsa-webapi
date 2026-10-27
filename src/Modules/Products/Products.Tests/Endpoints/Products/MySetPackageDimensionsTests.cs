using System.Net;
using System.Net.Http.Json;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Products;
using Products.Domain.Products.DomainEvents.v1;
using Products.Tests.Support;
using Xunit;
using MyGetResponse = Products.Endpoints.Products.v1.My.Get.Response;

namespace Products.Tests.Endpoints.Products;

public class MySetPackageDimensionsTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static object Body(decimal length = 30.5m, decimal width = 20m, decimal height = 10m, decimal weight = 1.25m)
    {
        return new { lengthCm = length, widthCm = width, heightCm = height, weightKg = weight };
    }

    private async Task<Product> SeedProductAsync(ApplicationUserId? owner = null)
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, owner ?? DefaultUser, "Lamp");
        return seeded.Products[0];
    }

    private static Uri Route(Product product) => new($"/v1/products/my/{product.Id}/package-dimensions", UriKind.Relative);

    [Fact]
    public async Task MySetPackageDimensions_WithValidValues_StoresThemAndRecordsTheEvent()
    {
        var product = await SeedProductAsync();

        using var response = await AuthorizedClient().PutAsJsonAsync(Route(product), Body());

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var stored = await Db.Products.AsNoTracking().SingleAsync(p => p.Id == product.Id);
        Assert.Equal(PackageDimensions.Create(30.5m, 20m, 10m, 1.25m).Value, stored.PackageDimensions);
        var audited = await Db.AuditLog.AsNoTracking().Where(e => e.AggregateId == product.Id.Value && e.Version == stored.Version).SingleAsync();
        Assert.IsType<V1ProductPackageDimensionsChangedDomainEvent>(audited.Event);
    }

    [Fact]
    public async Task MySetPackageDimensions_WithAZeroSide_ReturnsBadRequest()
    {
        var product = await SeedProductAsync();

        using var response = await AuthorizedClient().PutAsJsonAsync(Route(product), Body(width: 0m));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var stored = await Db.Products.AsNoTracking().SingleAsync(p => p.Id == product.Id);
        Assert.Null(stored.PackageDimensions);
    }

    [Fact]
    public async Task MySetPackageDimensions_OnAnotherOwnersProduct_ReturnsNotFound()
    {
        var product = await SeedProductAsync(new ApplicationUserId(Guid.NewGuid()));

        using var response = await AuthorizedClient().PutAsJsonAsync(Route(product), Body());

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task MyGet_ReturnsThePackageDimensionsAndNullWhenNoneAreSet()
    {
        var withDimensions = await SeedProductAsync();
        var without = (await ProductsSeed.StoreWithProductsAsync(Db, new ApplicationUserId(Guid.NewGuid()), "Chair")).Products[0];
        using var set = await AuthorizedClient().PutAsJsonAsync(Route(withDimensions), Body());

        using var response = await AuthorizedClient().GetAsync(new Uri($"/v1/products/my/{withDimensions.Id}", UriKind.Relative));
        var body = await response.Content.ReadFromJsonAsync<MyGetResponse>(JsonSerializerOptions);

        Assert.Equal(30.5m, body!.PackageDimensions!.LengthCm);
        Assert.Equal(1.25m, body.PackageDimensions.WeightKg);
        Assert.Null((await Db.Products.AsNoTracking().SingleAsync(p => p.Id == without.Id)).PackageDimensions);
    }
}
