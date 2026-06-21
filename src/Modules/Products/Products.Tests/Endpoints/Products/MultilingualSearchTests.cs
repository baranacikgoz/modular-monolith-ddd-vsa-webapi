using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bogus;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Endpoints.Products.v1.Search;
using DomainProduct = Products.Domain.Products.Product;
using Xunit;

namespace Products.Tests.Endpoints.Products;

[Collection("IntegrationTestCollection")]
public class MultilingualSearchTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public MultilingualSearchTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    // Seeds a product directly via the DbContext under the given UI culture so the
    // ApplySearchLanguageInterceptor stamps the matching per-row search Language.
    private async Task<DomainProduct> SeedProductAsync(
        IProductsDbContext db, string name, string description, string culture)
    {
        var previous = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
        try
        {
            var ownerId = new ApplicationUserId(Guid.NewGuid());
            var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(),
                _faker.Address.FullAddress());
            var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(),
                _faker.Commerce.Color());
            var product = DomainProduct.Create(store.Id, template.Id, name, description, 10, 100m);
            store.AddProduct(product);

            db.Stores.Add(store);
            db.ProductTemplates.Add(template);
            await db.SaveChangesAsync();

            return product;
        }
        finally
        {
            CultureInfo.CurrentUICulture = previous;
        }
    }

    private HttpClient CreateClient(string? acceptLanguage = null)
    {
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        if (acceptLanguage is not null)
        {
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLanguage));
        }

        return client;
    }

    private async Task<PaginationResponse<Response>> SearchAsync(HttpClient client, string searchTerm)
    {
        var response = await client.GetAsync(
            new Uri($"/v1/products/search?PageNumber=1&PageSize=10&SearchTerm={Uri.EscapeDataString(searchTerm)}",
                UriKind.Relative));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);
        Assert.NotNull(result);
        return result;
    }

    [Fact]
    public async Task Search_AsciiQuery_FindsAccentedDataViaUniversalLayer()
    {
        var db = Scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        await SeedProductAsync(db, name: "Koşu Ayakkabısı", description: "spor", culture: "tr");

        var client = CreateClient("tr");

        // ASCII "kosu" must fold to the accented stored "Koşu" via simple_unaccent.
        var result = await SearchAsync(client, "kosu");

        Assert.Equal(1, result.TotalCount);
        Assert.Equal("Koşu Ayakkabısı", result.Data.First().Name);
    }

    [Fact]
    public async Task Search_AccentedQuery_FindsAsciiData()
    {
        var db = Scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        await SeedProductAsync(db, name: "Cetin Marka", description: "urun", culture: "tr");

        var client = CreateClient("tr");

        // Accented "Çetin" must fold to the ASCII stored "Cetin".
        var result = await SearchAsync(client, "Çetin");

        Assert.Equal(1, result.TotalCount);
        Assert.Equal("Cetin Marka", result.Data.First().Name);
    }

    [Fact]
    public async Task Search_BrandInName_FoundAcrossLocales()
    {
        var db = Scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        // Authored in Turkish; the proper-noun brand lives in the language-neutral universal layer.
        await SeedProductAsync(db, name: "Zphqx Brand", description: "açıklama", culture: "tr");

        // Searched by an English-locale user.
        var client = CreateClient("en");
        var result = await SearchAsync(client, "Zphqx");

        Assert.Equal(1, result.TotalCount);
        Assert.Equal("Zphqx Brand", result.Data.First().Name);
    }

    [Fact]
    public async Task Search_InflectedQuery_MatchesProseViaLanguageStemming()
    {
        var db = Scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        // English prose: "running" is stored stemmed to "run".
        await SeedProductAsync(db, name: "Qwsxedc Item", description: "lightweight running gear", culture: "en");
        await SeedProductAsync(db, name: "Plnkmju Item", description: "static indoor furniture", culture: "en");

        var client = CreateClient("en");

        // "runs" stems to "run" only through english_unaccent — the prose layer, not the simple universal layer.
        var result = await SearchAsync(client, "runs");

        Assert.Equal(1, result.TotalCount);
        Assert.Equal("Qwsxedc Item", result.Data.First().Name);
    }

    [Fact]
    public async Task Search_NameMatch_RanksAboveDescriptionOnlyMatch()
    {
        var db = Scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        // Term in the name → universal layer (weight A).
        await SeedProductAsync(db, name: "Zenithify Edition", description: "ordinary goods", culture: "en");
        // Same term only in the description → prose layer (weight B).
        await SeedProductAsync(db, name: "Ordinary Edition", description: "a zenithify finish", culture: "en");

        var client = CreateClient("en");
        var result = await SearchAsync(client, "zenithify");

        Assert.Equal(2, result.TotalCount);
        Assert.Equal("Zenithify Edition", result.Data.First().Name);
    }

    [Fact]
    public async Task Create_UnderTurkishCulture_StampsTurkishUnaccentLanguage()
    {
        var db = Scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var product = await SeedProductAsync(db, name: "Ürün", description: "açıklama", culture: "tr");

        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var stored = await verifyDb.Products.AsNoTracking().FirstAsync(p => p.Id == product.Id);

        Assert.Equal("turkish_unaccent", stored.Language);
    }

    [Fact]
    public async Task Create_UnderEnglishCulture_StampsEnglishUnaccentLanguage()
    {
        var db = Scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var product = await SeedProductAsync(db, name: "Item", description: "a description", culture: "en");

        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var stored = await verifyDb.Products.AsNoTracking().FirstAsync(p => p.Id == product.Id);

        Assert.Equal("english_unaccent", stored.Language);
    }
}
