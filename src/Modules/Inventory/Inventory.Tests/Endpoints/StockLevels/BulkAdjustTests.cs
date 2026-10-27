using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Common.Application.Jobs;
using Common.IntegrationEvents;
using Inventory.Application.BulkAdjustments;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Xunit;
using BulkAdjustResponse = Inventory.Endpoints.StockLevels.v1.BulkAdjust.Response;

namespace Inventory.Tests.Endpoints.StockLevels;

public class BulkAdjustTests(IntegrationTestWebAppFactory factory) : BulkAdjustIntegrationTest(factory)
{
    private static readonly Uri Route = new("/v1/stock-levels/bulk-adjustments", UriKind.Relative);

    private static readonly byte[] SmallWorkbook = Workbooks.Build([Guid.NewGuid(), 5]);

    private static async Task<HttpResponseMessage> UploadAsync(
        HttpClient client, byte[] workbook, string clientRequestId, string reason = "Cycle count", string fileName = "adjustments.xlsx")
    {
        using var content = new MultipartFormDataContent();
        using var file = new ByteArrayContent(workbook);
        file.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        content.Add(file, "File", fileName);
        using var requestId = new StringContent(clientRequestId);
        using var reasonContent = new StringContent(reason);
        content.Add(requestId, "ClientRequestId");
        content.Add(reasonContent, "Reason");
        return await client.PostAsync(Route, content);
    }

    private async Task<BulkAdjustResponse> ReadJobAsync(HttpResponseMessage response)
    {
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        var job = await response.Content.ReadFromJsonAsync<BulkAdjustResponse>(JsonSerializerOptions);
        Assert.NotNull(job);
        return job;
    }

    [Fact]
    public async Task BulkAdjust_WithAWorkbook_Returns202AndQueuesTheJobWithItsUploadAndOneMessage()
    {
        using var response = await UploadAsync(AuthorizedClient(), SmallWorkbook, "request-1");

        var job = await ReadJobAsync(response);
        Assert.Equal("Queued", job.Status);
        Assert.Equal("adjustments.xlsx", job.FileName);
        Assert.Equal("Cycle count", job.Reason);

        var row = await Db.StockBulkAdjustJobs.AsNoTracking().SingleAsync(j => j.Id == job.JobId);
        Assert.Equal(DefaultUser, row.RequestedBy);
        Assert.Equal("request-1", row.ClientRequestId);
        Assert.Equal(JobStatus.Queued, row.Status);
        Assert.Equal(SmallWorkbook, PrivateStore.Read(row.ObjectKey));

        var queued = (await OutboxEventsAsync()).OfType<StockBulkAdjustRequestedIntegrationEvent>().Single();
        Assert.Equal(job.JobId.Value, queued.JobId);
    }

    [Fact]
    public async Task BulkAdjust_RepeatedClientRequestId_ReturnsTheFirstJobAndQueuesNothingNew()
    {
        var client = AuthorizedClient();

        using var first = await UploadAsync(client, SmallWorkbook, "request-1");
        using var second = await UploadAsync(client, Workbooks.Build([Guid.NewGuid(), 1]), "request-1");

        Assert.Equal((await ReadJobAsync(first)).JobId, (await ReadJobAsync(second)).JobId);
        Assert.Equal(1, await Db.StockBulkAdjustJobs.CountAsync());
        Assert.Single(PrivateStore.Keys);
        Assert.Single((await OutboxEventsAsync()).OfType<StockBulkAdjustRequestedIntegrationEvent>());
    }

    [Fact]
    public async Task BulkAdjust_WhileAnotherAdjustmentIsActive_ReturnsTheActiveJob()
    {
        var client = AuthorizedClient();

        using var first = await UploadAsync(client, SmallWorkbook, "request-1");
        using var doubleClick = await UploadAsync(client, SmallWorkbook, "request-2");

        Assert.Equal((await ReadJobAsync(first)).JobId, (await ReadJobAsync(doubleClick)).JobId);
        Assert.Equal(1, await Db.StockBulkAdjustJobs.CountAsync());
    }

    [Fact]
    public async Task BulkAdjust_FileOverTheSizeLimit_Returns413AndStoresNothing()
    {
        var limit = TestInventoryOptions.BulkImport().Value.MaxFileBytes;

        using var response = await UploadAsync(AuthorizedClient(), new byte[limit + 1], "request-1");

        Assert.Equal(HttpStatusCode.RequestEntityTooLarge, response.StatusCode);
        Assert.Empty(PrivateStore.Keys);
        Assert.Equal(0, await Db.StockBulkAdjustJobs.CountAsync());
    }

    [Fact]
    public async Task BulkAdjust_FileThatIsNotAWorkbookByName_Returns400()
    {
        using var response = await UploadAsync(AuthorizedClient(), SmallWorkbook, "request-1", fileName: "adjustments.txt");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(0, await Db.StockBulkAdjustJobs.CountAsync());
    }

    [Fact]
    public async Task BulkAdjust_WithoutAReason_Returns400()
    {
        using var response = await UploadAsync(AuthorizedClient(), SmallWorkbook, "request-1", reason: "");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(0, await Db.StockBulkAdjustJobs.CountAsync());
    }

    [Fact]
    public async Task BulkAdjust_WithoutAuthentication_Returns401()
    {
        using var response = await UploadAsync(Factory.CreateClient(), SmallWorkbook, "request-1");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
