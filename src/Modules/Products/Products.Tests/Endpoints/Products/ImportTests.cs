using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Common.Application.Jobs;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Products.Application.Imports;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Tests.Support;
using Xunit;
using ImportResponse = Products.Endpoints.Products.v1.Import.Response;

namespace Products.Tests.Endpoints.Products;

public class ImportTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static readonly byte[] SmallWorkbook = Workbooks.Build(["Desk lamp", "LED", 5, 49.9m]);

    private async Task<(ProductTemplate Template, ApplicationUserId Owner)> SeedStoreAndTemplateAsync(ApplicationUserId? owner = null)
    {
        var ownerId = owner ?? DefaultUser;
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, ownerId);
        return (seeded.Template, ownerId);
    }

    private static async Task<HttpResponseMessage> UploadAsync(
        HttpClient client, byte[] workbook, string clientRequestId, Guid templateId, string fileName = "products.xlsx")
    {
        using var content = new MultipartFormDataContent();
        using var file = new ByteArrayContent(workbook);
        file.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        content.Add(file, "File", fileName);
        using var requestId = new StringContent(clientRequestId);
        using var template = new StringContent(templateId.ToString());
        content.Add(requestId, "ClientRequestId");
        content.Add(template, "ProductTemplateId");
        return await client.PostAsync(new Uri("/v1/products/imports", UriKind.Relative), content);
    }

    private async Task<ImportResponse> ReadJobAsync(HttpResponseMessage response)
    {
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        var job = await response.Content.ReadFromJsonAsync<ImportResponse>(JsonSerializerOptions);
        Assert.NotNull(job);
        return job;
    }

    [Fact]
    public async Task Import_WithAWorkbook_Returns202AndQueuesTheJobWithItsUploadAndOneMessage()
    {
        var (template, owner) = await SeedStoreAndTemplateAsync();

        using var response = await UploadAsync(AuthorizedClient(), SmallWorkbook, "request-1", template.Id.Value);

        var job = await ReadJobAsync(response);
        Assert.Equal("Queued", job.Status);
        Assert.Equal("products.xlsx", job.FileName);

        var row = await Db.ImportJobs.AsNoTracking().SingleAsync(j => j.Id == job.JobId);
        Assert.Equal(owner, row.RequestedBy);
        Assert.Equal(template.Id, row.ProductTemplateId);
        Assert.Equal("request-1", row.ClientRequestId);
        Assert.Equal(JobStatus.Queued, row.Status);
        Assert.Equal(SmallWorkbook, PrivateStore.Read(row.ObjectKey));

        var queued = (await OutboxEventsAsync()).OfType<ProductImportRequestedIntegrationEvent>().Single();
        Assert.Equal(job.JobId.Value, queued.JobId);
    }

    [Fact]
    public async Task Import_WithAnAcceptLanguage_RecordsTheUploadersCultureOnTheJob()
    {
        var (template, _) = await SeedStoreAndTemplateAsync();
        var client = AuthorizedClient();
        client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("tr"));

        using var response = await UploadAsync(client, SmallWorkbook, "request-1", template.Id.Value);

        var job = await ReadJobAsync(response);
        var row = await Db.ImportJobs.AsNoTracking().SingleAsync(j => j.Id == job.JobId);
        Assert.Equal("tr", row.UiCulture);
    }

    [Fact]
    public async Task Import_RepeatedClientRequestId_ReturnsTheFirstJobAndQueuesNothingNew()
    {
        var (template, _) = await SeedStoreAndTemplateAsync();
        var client = AuthorizedClient();

        using var first = await UploadAsync(client, SmallWorkbook, "request-1", template.Id.Value);
        using var second = await UploadAsync(client, Workbooks.Build(["Other", "x", 1, 5m]), "request-1", template.Id.Value);

        Assert.Equal((await ReadJobAsync(first)).JobId, (await ReadJobAsync(second)).JobId);
        Assert.Equal(1, await Db.ImportJobs.CountAsync());
        Assert.Single(PrivateStore.Keys);
        Assert.Single((await OutboxEventsAsync()).OfType<ProductImportRequestedIntegrationEvent>());
    }

    [Fact]
    public async Task Import_WhileAnotherImportIsActive_ReturnsTheActiveJob()
    {
        var (template, _) = await SeedStoreAndTemplateAsync();
        var client = AuthorizedClient();

        using var first = await UploadAsync(client, SmallWorkbook, "request-1", template.Id.Value);
        using var doubleClick = await UploadAsync(client, SmallWorkbook, "request-2", template.Id.Value);

        Assert.Equal((await ReadJobAsync(first)).JobId, (await ReadJobAsync(doubleClick)).JobId);
        Assert.Equal(1, await Db.ImportJobs.CountAsync());
    }

    [Fact]
    public async Task Import_WithoutAStore_Returns404()
    {
        var template = await ProductsSeed.TemplateAsync(Db);

        using var response = await UploadAsync(AuthorizedClient(), SmallWorkbook, "request-1", template.Id.Value);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Empty(PrivateStore.Keys);
    }

    [Fact]
    public async Task Import_FileOverTheSizeLimit_Returns413AndStoresNothing()
    {
        var (template, _) = await SeedStoreAndTemplateAsync();
        var limit = TestOptions.BulkImport().Value.MaxFileBytes;

        using var response = await UploadAsync(AuthorizedClient(), new byte[limit + 1], "request-1", template.Id.Value);

        Assert.Equal(HttpStatusCode.RequestEntityTooLarge, response.StatusCode);
        Assert.Empty(PrivateStore.Keys);
        Assert.Equal(0, await Db.ImportJobs.CountAsync());
    }

    [Fact]
    public async Task Import_FileThatIsNotAWorkbookByName_Returns400()
    {
        var (template, _) = await SeedStoreAndTemplateAsync();

        using var response = await UploadAsync(AuthorizedClient(), SmallWorkbook, "request-1", template.Id.Value, "products.txt");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(0, await Db.ImportJobs.CountAsync());
    }

    [Fact]
    public async Task Import_WithoutAuthentication_Returns401()
    {
        using var response = await UploadAsync(Factory.CreateClient(), SmallWorkbook, "request-1", Guid.NewGuid());

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
