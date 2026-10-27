using System.Net;
using Inventory.Application.Gateway;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Gateway;
using Inventory.Tests.Support;
using Xunit;

namespace Inventory.Tests.Gateway;

/// <summary>
///     The second provider behind the same abstraction: form encoded bodies, an API key header and another response
///     envelope. Same outcomes for the caller, different wire.
/// </summary>
public class FormApiWarehouseGatewayTests : WarehouseTransportTestBase
{
    private const string Credential = "form-api-credential";

    private static readonly StockReservationId Reservation = new(Guid.Parse("0197c2d4-1f3a-7b5e-9c60-3a8e5d7f1b24"));

    private FormApiWarehouseGateway NewGateway()
    {
        var connection = TestWarehouseResilience.Connection(WarehouseProvider.FormApi);
        return new FormApiWarehouseGateway(
            Client,
            new Uri("https://warehouse.example-other.local/gateway"),
            Credential,
            TestWarehouseResilience.Pipelines().For(connection));
    }

    [Fact]
    public async Task RequestReleaseAsync_SuccessResponse_ReturnsTheReferenceAfterOneApiKeyAuthenticatedFormPost()
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { result = new { reference = "form-ref-9" } });

        var result = await NewGateway().RequestReleaseAsync(Reservation, "product-1", 5, CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal("form-ref-9", result.Value);
        var request = Assert.Single(Handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal(new Uri("https://warehouse.example-other.local/gateway/v2/holds/release"), request.Uri);
        Assert.Equal(Credential, request.ApiKey);
        Assert.Null(request.Authorization);
        Assert.Equal("application/x-www-form-urlencoded", request.ContentType);
        Assert.Equal($"sku=product-1&units=5&ref={Reservation.Value}", request.Body);
    }

    [Fact]
    public async Task ReconcileReleaseAsync_SuccessResponse_ReadsTheHoldRouteWithTheReservationInTheQuery()
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { result = new { reference = "form-ref-3" } });

        var result = await NewGateway().ReconcileReleaseAsync(Reservation, CancellationToken.None);

        Assert.Equal("form-ref-3", result.Value);
        var request = Assert.Single(Handler.Requests);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("/gateway/v2/holds/release", request.Uri!.AbsolutePath);
        Assert.Equal($"?ref={Reservation.Value}", request.Uri.Query);
        Assert.Equal(Credential, request.ApiKey);
    }

    [Fact]
    public async Task RequestReleaseAsync_NotFoundResponse_ReturnsEndpointNotFoundNotAMissingReference()
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.NotFound);

        var result = await NewGateway().RequestReleaseAsync(StockReservationId.New(), "product-1", 5, CancellationToken.None);

        Assert.Equal("WarehouseGatewayEndpointNotFound", result.Error!.Key);
        Assert.Equal(HttpStatusCode.BadGateway, result.Error.StatusCode);
    }

    // The warehouse says it knows no such release in the body of the 404. Nothing else is taken for that.
    [Fact]
    public async Task ReconcileReleaseAsync_NotFoundWithTheDocumentedBody_ReturnsTheReferenceNotFound()
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.NotFound, new { result = new { error = "hold_not_found" } });

        var result = await NewGateway().ReconcileReleaseAsync(Reservation, CancellationToken.None);

        Assert.Equal("NotFound", result.Error!.Key);
        Assert.Equal("WarehouseReference", result.Error.ParameterName);
        Assert.Equal(HttpStatusCode.NotFound, result.Error.StatusCode);
    }

    // A wrong base address answers 404 from some other server: that must never read as "the release was not made".
    [Fact]
    public async Task ReconcileReleaseAsync_BareNotFound_IsTheRouteMissingNotAMissingRelease()
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.NotFound);

        var result = await NewGateway().ReconcileReleaseAsync(Reservation, CancellationToken.None);

        Assert.Equal("WarehouseGatewayEndpointNotFound", result.Error!.Key);
        Assert.Equal(HttpStatusCode.BadGateway, result.Error.StatusCode);
    }

    [Theory]
    [InlineData("{\"result\":{\"error\":\"some_other_word\"}}")]
    [InlineData("{\"result\":{}}")]
    [InlineData("{\"error\":\"hold_not_found\"}")]
    [InlineData("<html>Not Found</html>")]
    [InlineData("null")]
    public async Task ReconcileReleaseAsync_NotFoundWithAnyOtherBody_IsTheRouteMissing(string body)
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json")
        };

        var result = await NewGateway().ReconcileReleaseAsync(Reservation, CancellationToken.None);

        Assert.Equal("WarehouseGatewayEndpointNotFound", result.Error!.Key);
    }

    [Fact]
    public async Task PushStockAsync_NotFoundResponse_ReturnsEndpointNotFoundNotAMissingReference()
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.NotFound);

        var result = await NewGateway().PushStockAsync([new("product-1", 1)], CancellationToken.None);

        Assert.Equal("WarehouseGatewayEndpointNotFound", result.Error!.Key);
        Assert.Equal(HttpStatusCode.BadGateway, result.Error.StatusCode);
    }

    [Fact]
    public async Task RequestReleaseAsync_ServerErrorResponse_ReturnsBadGatewayAndIsSentOnce()
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.BadGateway);

        var result = await NewGateway().RequestReleaseAsync(StockReservationId.New(), "product-1", 5, CancellationToken.None);

        Assert.Equal("WarehouseGatewayCallFailed", result.Error!.Key);
        Assert.Equal(HttpStatusCode.BadGateway, result.Error.StatusCode);
        Assert.Single(Handler.Requests);
    }

    [Fact]
    public async Task RequestReleaseAsync_MalformedBody_ReturnsMalformedResponseError()
    {
        // The other provider's envelope is not this provider's: the reference is missing, so the answer is unusable.
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "wh-ref-123" });

        var result = await NewGateway().RequestReleaseAsync(StockReservationId.New(), "product-1", 5, CancellationToken.None);

        Assert.Equal("WarehouseGatewayMalformedResponse", result.Error!.Key);
    }

    [Fact]
    public async Task RequestReleaseAsync_TransportFailure_IsAnUnknownOutcomeAndTheReleaseIsNotSentAgain()
    {
        Handler.Responder = _ => throw new HttpRequestException("Connection reset by peer.");

        await Assert.ThrowsAsync<WarehouseOutcomeUnknownException>(
            () => NewGateway().RequestReleaseAsync(StockReservationId.New(), "product-1", 5, CancellationToken.None));

        Assert.Single(Handler.Requests);
    }

    [Fact]
    public async Task PushStockAsync_SuccessResponse_ReturnsTheReferenceAfterOneApiKeyAuthenticatedFormPost()
    {
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { result = new { reference = "form-batch-4" } });
        WarehouseStockQuantity[] quantities = [new("product-1", 7), new("product-2", 0)];

        var result = await NewGateway().PushStockAsync(quantities, CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal("form-batch-4", result.Value);
        var request = Assert.Single(Handler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal(new Uri("https://warehouse.example-other.local/gateway/v2/stock/sync"), request.Uri);
        Assert.Equal(Credential, request.ApiKey);
        Assert.Null(request.Authorization);
        Assert.Equal("application/x-www-form-urlencoded", request.ContentType);
        Assert.Equal("sku%5B0%5D=product-1&units%5B0%5D=7&sku%5B1%5D=product-2&units%5B1%5D=0", request.Body);
    }

    [Fact]
    public async Task PushStockAsync_TooManyRequests_ReturnsThrottled()
    {
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.TooManyRequests);

        var result = await NewGateway().PushStockAsync([new("product-1", 1)], CancellationToken.None);

        Assert.Equal("WarehouseGatewayThrottled", result.Error!.Key);
        Assert.Single(Handler.Requests);
    }
}
