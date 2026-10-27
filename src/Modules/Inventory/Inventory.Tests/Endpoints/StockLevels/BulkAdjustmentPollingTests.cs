using System.Net;
using System.Net.Http.Json;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Inventory.Application.BulkAdjustments;
using Inventory.Tests.Support;
using Xunit;
using BulkAdjustResponse = Inventory.Endpoints.StockLevels.v1.BulkAdjust.Response;

namespace Inventory.Tests.Endpoints.StockLevels;

public class BulkAdjustmentPollingTests(IntegrationTestWebAppFactory factory) : BulkAdjustIntegrationTest(factory)
{
    private static readonly DateTimeOffset Now = new(2026, 10, 1, 12, 0, 0, TimeSpan.Zero);

    private async Task<StockBulkAdjustJob> AddJobAsync(ApplicationUserId owner, DateTimeOffset queuedOn, string clientRequestId)
    {
        var job = StockBulkAdjustJob.Queue(StockBulkAdjustJobId.New(), owner, queuedOn, clientRequestId, "adjustments.xlsx", "key", "Cycle count");
        job.MarkSucceeded(queuedOn.AddMinutes(1));
        Db.StockBulkAdjustJobs.Add(job);
        await Db.SaveChangesAsync();
        return job;
    }

    [Fact]
    public async Task GetBulkAdjustment_OwnJob_ReturnsItsStatusAndCounters()
    {
        var job = await AddJobAsync(DefaultUser, Now, "request-1");

        var response = await AuthorizedClient().GetFromJsonAsync<BulkAdjustResponse>(
            new Uri($"/v1/stock-levels/bulk-adjustments/{job.Id}", UriKind.Relative), JsonSerializerOptions);

        Assert.NotNull(response);
        Assert.Equal(job.Id, response.JobId);
        Assert.Equal("Succeeded", response.Status);
        Assert.Equal("adjustments.xlsx", response.FileName);
        Assert.Equal("Cycle count", response.Reason);
        Assert.Empty(response.RowErrors);
    }

    [Fact]
    public async Task GetBulkAdjustment_JobOfAnotherUser_Returns404()
    {
        var job = await AddJobAsync(new ApplicationUserId(Guid.NewGuid()), Now, "request-1");

        using var response = await AuthorizedClient().GetAsync(new Uri($"/v1/stock-levels/bulk-adjustments/{job.Id}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ListBulkAdjustments_WalkingTheCursor_ReturnsMyJobsNewestFirstAndNeverAnotherUsers()
    {
        var oldest = await AddJobAsync(DefaultUser, Now, "request-1");
        var middle = await AddJobAsync(DefaultUser, Now.AddHours(1), "request-2");
        var newest = await AddJobAsync(DefaultUser, Now.AddHours(2), "request-3");
        await AddJobAsync(new ApplicationUserId(Guid.NewGuid()), Now.AddHours(3), "request-4");
        var client = AuthorizedClient();

        var first = await client.GetFromJsonAsync<PaginationResponse<BulkAdjustResponse>>(
            new Uri("/v1/stock-levels/bulk-adjustments?PageNumber=1&PageSize=2&IncludeTotal=false", UriKind.Relative), JsonSerializerOptions);
        Assert.NotNull(first);
        Assert.NotNull(first.NextCursor);

        var second = await client.GetFromJsonAsync<PaginationResponse<BulkAdjustResponse>>(
            new Uri($"/v1/stock-levels/bulk-adjustments?PageNumber=1&PageSize=2&IncludeTotal=false&After={first.NextCursor}", UriKind.Relative),
            JsonSerializerOptions);
        Assert.NotNull(second);

        Assert.Equal([newest.Id, middle.Id], first.Data.Select(j => j.JobId));
        Assert.Equal([oldest.Id], second.Data.Select(j => j.JobId));
        Assert.Null(second.NextCursor);
    }
}
