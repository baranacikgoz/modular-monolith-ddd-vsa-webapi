using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using Products.Domain.Products;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Endpoints.Products;

/// <summary>
///     The lowest price is <see cref="Constants.PriceGreaterThanOrEqualTo" /> itself, as its name, the validation messages and
///     the import parser all say: a price equal to it is accepted, one just below it is not.
/// </summary>
public class PriceRuleTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static decimal Minimum => Constants.PriceGreaterThanOrEqualTo;

    private static decimal BelowMinimum => Constants.PriceGreaterThanOrEqualTo - 0.01m;

    // The query string is invariant: "0.99" must not be written as "0,99" by the host's culture.
    private static string Query(decimal value) => value.ToString(CultureInfo.InvariantCulture);

    private async Task<SeededStore> SeedAsync()
    {
        return await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "Lamp");
    }

    private static object NewProductBody(SeededStore seeded, decimal price)
    {
        return new { productTemplateId = seeded.Template.Id.Value, name = "Desk", description = "Description", quantity = 1, price };
    }

    [Fact]
    public async Task AddProduct_PriceEqualToTheMinimum_Succeeds()
    {
        var seeded = await SeedAsync();

        using var response = await AuthorizedClient().PostAsJsonAsync(
            new Uri($"/v1/stores/{seeded.Store.Id}/products", UriKind.Relative), NewProductBody(seeded, Minimum));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AddProduct_PriceBelowTheMinimum_ReturnsBadRequest()
    {
        var seeded = await SeedAsync();

        using var response = await AuthorizedClient().PostAsJsonAsync(
            new Uri($"/v1/stores/{seeded.Store.Id}/products", UriKind.Relative), NewProductBody(seeded, BelowMinimum));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task MyAddProduct_PriceEqualToTheMinimum_Succeeds()
    {
        var seeded = await SeedAsync();

        using var response = await AuthorizedClient().PostAsJsonAsync(
            new Uri("/v1/stores/my/products", UriKind.Relative), NewProductBody(seeded, Minimum));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Update_PriceEqualToTheMinimum_Succeeds()
    {
        var seeded = await SeedAsync();

        using var response = await AuthorizedClient().PutAsJsonAsync(
            new Uri($"/v1/products/{seeded.Products[0].Id}", UriKind.Relative), new { price = Minimum });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task MyUpdate_PriceEqualToTheMinimum_Succeeds()
    {
        var seeded = await SeedAsync();

        using var response = await AuthorizedClient().PutAsJsonAsync(
            new Uri($"/v1/products/my/{seeded.Products[0].Id}", UriKind.Relative), new { price = Minimum });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Search_PriceFilterEqualToTheMinimum_IsAccepted()
    {
        using var response = await AuthorizedClient().GetAsync(new Uri(
            $"/v1/products/search?PageNumber=1&PageSize=10&MinPrice={Query(Minimum)}&MaxPrice={Query(Minimum)}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Search_PriceFilterBelowTheMinimum_ReturnsBadRequest()
    {
        using var response = await AuthorizedClient().GetAsync(new Uri(
            $"/v1/products/search?PageNumber=1&PageSize=10&MinPrice={Query(BelowMinimum)}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task MySearch_PriceFilterEqualToTheMinimum_IsAccepted()
    {
        using var response = await AuthorizedClient().GetAsync(new Uri(
            $"/v1/products/my/search?PageNumber=1&PageSize=10&MinPrice={Query(Minimum)}&MaxPrice={Query(Minimum)}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
