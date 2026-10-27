using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Inventory.Application.Gateway;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Gateway;
using Inventory.Tests.Support;
using Polly.Timeout;
using Xunit;

namespace Inventory.Tests.Gateway;

/// <summary>
///     The JSON provider's wire shape and its response mapping, against a fake transport: no real HTTP call and no mocking
///     library, just a handler that records what it received and returns canned responses.
/// </summary>
public class JsonApiWarehouseGatewayTests : WarehouseTransportTestBase
{
    private const string Credential = "json-api-credential";

    private JsonApiWarehouseGateway NewGateway(Uri? baseUrl = null)
    {
        var connection = TestWarehouseResilience.Connection(WarehouseProvider.JsonApi);
        return new JsonApiWarehouseGateway(
            Client,
            baseUrl ?? new Uri("https://warehouse.example-partner.local/api/"),
            Credential,
            TestWarehouseResilience.Pipelines().For(connection));
    }

    [Fact]
    public async Task RequestReleaseAsync_SuccessResponse_ReturnsTheConfirmationReferenceAfterOneBearerAuthenticatedJsonPost()
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-123" });

        var result = await NewGateway().RequestReleaseAsync("product-1", 5, CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal("wh-ref-123", result.Value);
        var request = Assert.Single(Handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal(new Uri("https://warehouse.example-partner.local/api/releases"), request.Uri);
        Assert.Equal($"Bearer {Credential}", request.Authorization);
        Assert.Null(request.ApiKey);
        Assert.Equal("application/json", request.ContentType);
        using var body = JsonDocument.Parse(request.Body);
        Assert.Equal("product-1", body.RootElement.GetProperty("productReference").GetString());
        Assert.Equal(5, body.RootElement.GetProperty("quantity").GetInt32());
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task RequestReleaseAsync_BaseUrlWithOrWithoutTrailingSlash_ReachesTheSameRoute(bool trailingSlash)
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "ref" });
        var baseUrl = new Uri(trailingSlash ? "https://warehouse.example-partner.local/api/" : "https://warehouse.example-partner.local/api");

        await NewGateway(baseUrl).RequestReleaseAsync("product-1", 1, CancellationToken.None);

        Assert.Equal(new Uri("https://warehouse.example-partner.local/api/releases"), Assert.Single(Handler.Requests).Uri);
    }

    [Fact]
    public async Task ReconcileReleaseAsync_SuccessResponse_ReadsTheStatusRouteOfTheEscapedReference()
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-9" });

        var result = await NewGateway().ReconcileReleaseAsync("rel:1/2", CancellationToken.None);

        Assert.Equal("wh-ref-9", result.Value);
        var request = Assert.Single(Handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/api/releases/rel%3A1%2F2/status", request.Uri!.AbsolutePath);
        Assert.Equal($"Bearer {Credential}", request.Authorization);
    }

    // The release names its product in the body, so a 404 on the route means the base address does not lead to the API.
    [Fact]
    public async Task RequestReleaseAsync_NotFoundResponse_ReturnsEndpointNotFoundNotAMissingReference()
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.NotFound);

        var result = await NewGateway().RequestReleaseAsync("product-1", 5, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("WarehouseGatewayEndpointNotFound", result.Error!.Key);
        Assert.Equal(HttpStatusCode.BadGateway, result.Error.StatusCode);
    }

    // The reconcile route carries the reference, so there a 404 is the warehouse saying it knows no such release.
    [Fact]
    public async Task ReconcileReleaseAsync_NotFoundResponse_ReturnsTheReferenceNotFound()
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.NotFound);

        var result = await NewGateway().ReconcileReleaseAsync("rel-1", CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("NotFound", result.Error!.Key);
        Assert.Equal("WarehouseReference", result.Error.ParameterName);
        Assert.Equal(HttpStatusCode.NotFound, result.Error.StatusCode);
    }

    [Fact]
    public async Task PushStockAsync_NotFoundResponse_ReturnsEndpointNotFoundNotAMissingReference()
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.NotFound);

        var result = await NewGateway().PushStockAsync([new("product-1", 1)], CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("WarehouseGatewayEndpointNotFound", result.Error!.Key);
        Assert.Equal(HttpStatusCode.BadGateway, result.Error.StatusCode);
    }

    [Fact]
    public async Task RequestReleaseAsync_ServerErrorResponse_ReturnsBadGatewayAndIsSentOnce()
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.InternalServerError);

        var result = await NewGateway().RequestReleaseAsync("product-1", 5, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("WarehouseGatewayCallFailed", result.Error!.Key);
        Assert.Equal(HttpStatusCode.BadGateway, result.Error.StatusCode);
        Assert.Single(Handler.Requests);
    }

    [Fact]
    public async Task RequestReleaseAsync_MalformedBody_ReturnsMalformedResponseError()
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { unexpectedField = "oops" });

        var result = await NewGateway().RequestReleaseAsync("product-1", 5, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("WarehouseGatewayMalformedResponse", result.Error!.Key);
    }

    [Fact]
    public async Task RequestReleaseAsync_TooManyRequests_ReturnsThrottledWithTheWaitTheWarehouseNamed()
    {
        Handler.Responder = _ =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
            response.Headers.RetryAfter = new RetryConditionHeaderValue(TimeSpan.FromSeconds(30));
            return response;
        };

        var result = await NewGateway().RequestReleaseAsync("product-1", 5, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("WarehouseGatewayThrottled", result.Error!.Key);
        Assert.Equal(HttpStatusCode.TooManyRequests, result.Error.StatusCode);
        Assert.Equal(30, result.Error.Value);
    }

    // The release is not idempotent: a transport failure says nothing about whether the warehouse processed it, so the
    // gateway reports an unknown outcome and the caller keeps its attempt marker. The pipeline must not send it a second time.
    [Fact]
    public async Task RequestReleaseAsync_TransportFailure_IsAnUnknownOutcomeAndTheReleaseIsNotSentAgain()
    {
        Handler.Responder = _ => throw new HttpRequestException("Connection reset by peer.");

        await Assert.ThrowsAsync<WarehouseOutcomeUnknownException>(
            () => NewGateway().RequestReleaseAsync("product-1", 5, CancellationToken.None));

        Assert.Single(Handler.Requests);
    }

    [Fact]
    public async Task RequestReleaseAsync_AttemptTimeout_IsAnUnknownOutcomeAndTheReleaseIsNotSentAgain()
    {
        Handler.Responder = _ => throw new TimeoutRejectedException("The attempt timed out.");

        await Assert.ThrowsAsync<WarehouseOutcomeUnknownException>(
            () => NewGateway().RequestReleaseAsync("product-1", 5, CancellationToken.None));

        Assert.Single(Handler.Requests);
    }

    [Fact]
    public async Task RequestReleaseAsync_CallerCancels_PropagatesTheCancellationInsteadOfAnUnknownOutcome()
    {
        using var cancelled = new CancellationTokenSource();
        await cancelled.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => NewGateway().RequestReleaseAsync("product-1", 5, cancelled.Token));
    }

    [Fact]
    public async Task ReconcileReleaseAsync_ServerErrorResponse_IsNotRepeatedByThePipeline()
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);

        var result = await NewGateway().ReconcileReleaseAsync("rel-1", CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Single(Handler.Requests);
    }

    [Fact]
    public async Task PushStockAsync_SuccessResponse_ReturnsTheBatchReferenceAfterOneBearerAuthenticatedJsonPost()
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-batch-1" });
        WarehouseStockQuantity[] quantities = [new("product-1", 7), new("product-2", 0)];

        var result = await NewGateway().PushStockAsync(quantities, CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal("wh-batch-1", result.Value);
        var request = Assert.Single(Handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal(new Uri("https://warehouse.example-partner.local/api/stock/levels"), request.Uri);
        Assert.Equal($"Bearer {Credential}", request.Authorization);
        Assert.Equal("application/json", request.ContentType);
        using var body = JsonDocument.Parse(request.Body);
        var items = body.RootElement.GetProperty("items").EnumerateArray().ToList();
        Assert.Equal(2, items.Count);
        Assert.Equal("product-1", items[0].GetProperty("productReference").GetString());
        Assert.Equal(7, items[0].GetProperty("quantity").GetInt32());
        Assert.Equal("product-2", items[1].GetProperty("productReference").GetString());
        Assert.Equal(0, items[1].GetProperty("quantity").GetInt32());
    }

    [Fact]
    public async Task PushStockAsync_ServerErrorResponse_ReturnsBadGatewayAndIsSentOnce()
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.InternalServerError);

        var result = await NewGateway().PushStockAsync([new("product-1", 1)], CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("WarehouseGatewayCallFailed", result.Error!.Key);
        Assert.Single(Handler.Requests);
    }

    // The quantities are absolute, so the caller can safely send the current ones again; the pipeline itself still never does.
    [Fact]
    public async Task PushStockAsync_TransportFailure_IsAnUnknownOutcomeAndTheBatchIsNotSentAgain()
    {
        Handler.Responder = _ => throw new HttpRequestException("Connection reset by peer.");

        await Assert.ThrowsAsync<WarehouseOutcomeUnknownException>(
            () => NewGateway().PushStockAsync([new("product-1", 1)], CancellationToken.None));

        Assert.Single(Handler.Requests);
    }
}
