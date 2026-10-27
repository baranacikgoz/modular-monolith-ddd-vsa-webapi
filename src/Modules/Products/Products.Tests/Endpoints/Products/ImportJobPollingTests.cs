using System.Net;
using System.Net.Http.Json;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Products.Application.Imports;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Tests.Support;
using Xunit;
using ImportResponse = Products.Endpoints.Products.v1.Import.Response;

namespace Products.Tests.Endpoints.Products;

public class ImportJobPollingTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static readonly DateTimeOffset Now = new(2026, 10, 1, 12, 0, 0, TimeSpan.Zero);

    private async Task<ProductImportJob> AddJobAsync(ApplicationUserId owner, DateTimeOffset queuedOn, string clientRequestId)
    {
        var job = ProductImportJob.Queue(
            ProductImportJobId.New(), owner, queuedOn, StoreId.New(), ProductTemplateId.New(), clientRequestId, "products.xlsx", "key", string.Empty);
        job.MarkSucceeded(queuedOn.AddMinutes(1));
        Db.ImportJobs.Add(job);
        await Db.SaveChangesAsync();
        return job;
    }

    [Fact]
    public async Task GetImport_OwnJob_ReturnsItsStatusAndCounters()
    {
        var job = await AddJobAsync(DefaultUser, Now, "request-1");

        var response = await AuthorizedClient().GetFromJsonAsync<ImportResponse>(
            new Uri($"/v1/products/imports/{job.Id}", UriKind.Relative), JsonSerializerOptions);

        Assert.NotNull(response);
        Assert.Equal(job.Id, response.JobId);
        Assert.Equal("Succeeded", response.Status);
        Assert.Equal("products.xlsx", response.FileName);
        Assert.Empty(response.RowErrors);
    }

    [Fact]
    public async Task GetImport_JobOfAnotherUser_Returns404()
    {
        var job = await AddJobAsync(new ApplicationUserId(Guid.NewGuid()), Now, "request-1");

        using var response = await AuthorizedClient().GetAsync(new Uri($"/v1/products/imports/{job.Id}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ListImports_WalkingTheCursor_ReturnsMyJobsNewestFirstAndNeverAnotherUsers()
    {
        var oldest = await AddJobAsync(DefaultUser, Now, "request-1");
        var middle = await AddJobAsync(DefaultUser, Now.AddHours(1), "request-2");
        var newest = await AddJobAsync(DefaultUser, Now.AddHours(2), "request-3");
        await AddJobAsync(new ApplicationUserId(Guid.NewGuid()), Now.AddHours(3), "request-4");
        var client = AuthorizedClient();

        var first = await client.GetFromJsonAsync<PaginationResponse<ImportResponse>>(
            new Uri("/v1/products/imports?PageNumber=1&PageSize=2&IncludeTotal=false", UriKind.Relative), JsonSerializerOptions);
        Assert.NotNull(first);
        Assert.NotNull(first.NextCursor);

        var second = await client.GetFromJsonAsync<PaginationResponse<ImportResponse>>(
            new Uri($"/v1/products/imports?PageNumber=1&PageSize=2&IncludeTotal=false&After={first.NextCursor}", UriKind.Relative), JsonSerializerOptions);
        Assert.NotNull(second);

        Assert.Equal([newest.Id, middle.Id], first.Data.Select(j => j.JobId));
        Assert.Equal([oldest.Id], second.Data.Select(j => j.JobId));
        Assert.Null(second.NextCursor);
    }
}
