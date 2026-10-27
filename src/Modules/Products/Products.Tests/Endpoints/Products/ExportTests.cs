using System.Net;
using System.Net.Http.Json;
using System.Text;
using Common.Application.Jobs;
using Common.Application.Storage;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Products.Application.Exports;
using Products.Domain.Stores;
using Products.Tests.Support;
using Xunit;
using ExportResponse = Products.Endpoints.Products.v1.Export.Response;

namespace Products.Tests.Endpoints.Products;

public class ExportTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static readonly byte[] CsvBytes = Encoding.UTF8.GetBytes("Id,Name\r\n1,Lamp\r\n");

    private async Task<ProductExportJob> AddJobAsync(ApplicationUserId owner, JobStatus status = JobStatus.Queued)
    {
        var job = ProductExportJob.Queue(owner, DateTimeOffset.UtcNow, null);
        Db.ExportJobs.Add(job);
        await Db.SaveChangesAsync();

        if (status == JobStatus.Succeeded)
        {
            var objectKey = $"products/exports/{job.Id}.csv";
            using var content = new MemoryStream(CsvBytes);
            await PrivateStore.UploadAsync(new UploadObjectRequest(objectKey, content, "text/csv"), CancellationToken.None);
            await Db.ExportJobs.Where(j => j.Id == job.Id).ExecuteUpdateAsync(s => s
                .SetProperty(j => j.Status, JobStatus.Succeeded)
                .SetProperty(j => j.FileName, "products-20261001120000.csv")
                .SetProperty(j => j.ObjectKey, objectKey)
                .SetProperty(j => j.FinishedOn, DateTimeOffset.UtcNow));
        }

        return job;
    }

    [Fact]
    public async Task Export_Returns202AndQueuesTheJobForTheStoreWithOneMessage()
    {
        var storeId = StoreId.New();

        using var response = await AuthorizedClient().PostAsync(new Uri($"/v1/products/exports?StoreId={storeId}", UriKind.Relative), null);

        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        var job = await response.Content.ReadFromJsonAsync<ExportResponse>(JsonSerializerOptions);
        Assert.NotNull(job);
        Assert.Equal("Queued", job.Status);
        Assert.Equal(storeId, job.StoreId);

        var row = await Db.ExportJobs.AsNoTracking().SingleAsync(j => j.Id == job.JobId);
        Assert.Equal(DefaultUser, row.RequestedBy);
        Assert.Equal(storeId, row.StoreId);
        var queued = (await OutboxEventsAsync()).OfType<ProductExportRequestedIntegrationEvent>().Single();
        Assert.Equal(job.JobId.Value, queued.JobId);
    }

    [Fact]
    public async Task Export_WhileAnotherExportIsActive_ReturnsTheActiveJob()
    {
        var client = AuthorizedClient();

        using var first = await client.PostAsync(new Uri("/v1/products/exports", UriKind.Relative), null);
        using var second = await client.PostAsync(new Uri("/v1/products/exports", UriKind.Relative), null);

        var firstJob = await first.Content.ReadFromJsonAsync<ExportResponse>(JsonSerializerOptions);
        var secondJob = await second.Content.ReadFromJsonAsync<ExportResponse>(JsonSerializerOptions);
        Assert.Equal(HttpStatusCode.Accepted, second.StatusCode);
        Assert.Equal(firstJob!.JobId, secondJob!.JobId);
        Assert.Equal(1, await Db.ExportJobs.CountAsync());
        Assert.Single((await OutboxEventsAsync()).OfType<ProductExportRequestedIntegrationEvent>());
    }

    [Fact]
    public async Task GetExport_OwnJob_ReturnsItsStatus()
    {
        var job = await AddJobAsync(DefaultUser, JobStatus.Succeeded);

        var response = await AuthorizedClient().GetFromJsonAsync<ExportResponse>(
            new Uri($"/v1/products/exports/{job.Id}", UriKind.Relative), JsonSerializerOptions);

        Assert.NotNull(response);
        Assert.Equal("Succeeded", response.Status);
        Assert.Equal("products-20261001120000.csv", response.FileName);
    }

    [Fact]
    public async Task GetExport_JobOfAnotherUser_Returns404()
    {
        var job = await AddJobAsync(new ApplicationUserId(Guid.NewGuid()));

        using var response = await AuthorizedClient().GetAsync(new Uri($"/v1/products/exports/{job.Id}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Download_SucceededJob_ReturnsTheCsvAsAFile()
    {
        var job = await AddJobAsync(DefaultUser, JobStatus.Succeeded);

        using var response = await AuthorizedClient().GetAsync(new Uri($"/v1/products/exports/{job.Id}/download", UriKind.Relative));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("text/csv", response.Content.Headers.ContentType?.MediaType);
        Assert.Equal("products-20261001120000.csv", response.Content.Headers.ContentDisposition?.FileName);
        Assert.Equal(CsvBytes, await response.Content.ReadAsByteArrayAsync());
    }

    [Fact]
    public async Task Download_JobThatHasNotSucceeded_Returns409()
    {
        var job = await AddJobAsync(DefaultUser);

        using var response = await AuthorizedClient().GetAsync(new Uri($"/v1/products/exports/{job.Id}/download", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Contains(ProductExportErrors.NotReady.Key, await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
    }

    [Fact]
    public async Task Download_JobOfAnotherUser_Returns404()
    {
        var job = await AddJobAsync(new ApplicationUserId(Guid.NewGuid()), JobStatus.Succeeded);

        using var response = await AuthorizedClient().GetAsync(new Uri($"/v1/products/exports/{job.Id}/download", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
