using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Endpoints.Products.v1.Search;
using DomainProduct = Products.Domain.Products.Product;
using Xunit;

namespace Products.Tests.Endpoints.Products;

// Human-readable end-to-end demonstration: seeds a known Turkish + English catalog through the real
// write pipeline (interceptor stamps per-row Language), dumps what Postgres actually stored (Language +
// tsvector lexemes), then runs real HTTP searches and prints the ranked results. Run with:
//   dotnet test --filter FullyQualifiedName~MultilingualSearchDemo -l "console;verbosity=detailed"
[Collection("IntegrationTestCollection")]
public class MultilingualSearchDemo : BaseIntegrationTest
{
    private readonly ITestOutputHelper _out;

    public MultilingualSearchDemo(IntegrationTestWebAppFactory factory, ITestOutputHelper output) : base(factory)
    {
        _out = output;
    }

    private static async Task<DomainProduct> SeedAsync(IProductsDbContext db, string name, string description, string culture)
    {
        var previous = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
        try
        {
            var ownerId = new ApplicationUserId(Guid.NewGuid());
            var store = Store.Create(ownerId, $"Store-{name}", "demo store", "demo address");
            var template = ProductTemplate.Create($"Brand-{name}", "Model-X", "Black");
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

    private HttpClient Client(string acceptLanguage)
    {
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLanguage));
        return client;
    }

    private async Task RunSearchAsync(string label, string acceptLanguage, string term)
    {
        var client = Client(acceptLanguage);
        var response = await client.GetAsync(
            new Uri($"/v1/products/search?PageNumber=1&PageSize=10&SearchTerm={Uri.EscapeDataString(term)}",
                UriKind.Relative));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        var names = result!.Data.Select(p => p.Name).ToList();
        _out.WriteLine($"  [{label}] Accept-Language:{acceptLanguage,-3} term='{term}'  →  {result.TotalCount} hit(s)");
        for (var i = 0; i < names.Count; i++)
        {
            _out.WriteLine($"        #{i + 1} (rank order): {names[i]}");
        }
    }

    private async Task DumpStorageAsync()
    {
        await using var conn = new NpgsqlConnection(Factory.ConnectionString);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(
            """SELECT "Name", "Language", "SearchVector"::text FROM "Products"."Products" ORDER BY "Name";""", conn);
        await using var reader = await cmd.ExecuteReaderAsync();
        _out.WriteLine("── What Postgres actually stored (per-row Language + generated tsvector) ──");
        while (await reader.ReadAsync())
        {
            _out.WriteLine($"  Name='{reader.GetString(0)}'  Language='{reader.GetString(1)}'");
            _out.WriteLine($"      tsvector: {reader.GetString(2)}");
        }
    }

    [Fact]
    public async Task Demonstrate_Multilingual_Search_EndToEnd()
    {
        var db = Scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        // Two rows authored in different locales — exactly the worked example from the design doc.
        await SeedAsync(db, name: "Koşu Ayakkabısı", description: "Hafif ve dayanıklı koşu ayakkabısı", culture: "tr");
        await SeedAsync(db, name: "Running Shoes", description: "Lightweight durable running shoes", culture: "en");
        // A proper-noun brand authored in Turkish, to prove cross-locale discovery via the universal layer.
        await SeedAsync(db, name: "Nikex Sneaker", description: "özel üretim", culture: "tr");
        // Ranking pair: same term in name (weight A) vs only in description (weight B).
        await SeedAsync(db, name: "Zenithify Boot", description: "ordinary", culture: "en");
        await SeedAsync(db, name: "Plain Boot", description: "a zenithify finish", culture: "en");

        _out.WriteLine("");
        await DumpStorageAsync();

        _out.WriteLine("");
        _out.WriteLine("── Real HTTP searches against the running app ──");

        // Accent fold: ASCII query finds accented Turkish data via simple_unaccent universal layer.
        await RunSearchAsync("accent-fold", "tr", "kosu");
        // Turkish stemming on prose: inflected form matches via turkish_unaccent.
        await RunSearchAsync("tr-stem", "tr", "dayanıklı");
        // English stemming on prose: "running" stored as "run", query "runs" → "run".
        await RunSearchAsync("en-stem", "en", "runs");
        // Cross-locale brand: TR-authored "Nikex" found by an English user via universal layer.
        await RunSearchAsync("cross-locale", "en", "Nikex");
        // Isolation: an English prose word must NOT match the Turkish row.
        await RunSearchAsync("isolation", "en", "lightweight");
        // Ranking: name-layer (A) row outranks description-only (B) row.
        await RunSearchAsync("ranking", "en", "zenithify");

        // Hard assertions so this is a real test, not just a print.
        var ranked = await SearchNamesAsync("en", "zenithify");
        Assert.Equal(["Zenithify Boot", "Plain Boot"], ranked);

        var isolation = await SearchNamesAsync("en", "lightweight");
        Assert.Equal(["Running Shoes"], isolation);

        var accent = await SearchNamesAsync("tr", "kosu");
        Assert.Contains("Koşu Ayakkabısı", accent);

        var crossLocale = await SearchNamesAsync("en", "Nikex");
        Assert.Equal(["Nikex Sneaker"], crossLocale);
    }

    private async Task<List<string>> SearchNamesAsync(string acceptLanguage, string term)
    {
        var client = Client(acceptLanguage);
        var response = await client.GetAsync(
            new Uri($"/v1/products/search?PageNumber=1&PageSize=10&SearchTerm={Uri.EscapeDataString(term)}",
                UriKind.Relative));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);
        return result!.Data.Select(p => p.Name).ToList();
    }
}
