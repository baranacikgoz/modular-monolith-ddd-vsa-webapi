using System.Net;
using System.Net.Http.Json;
using Inventory.Infrastructure.Gateway;
using Xunit;

namespace Inventory.Tests.Gateway;

/// <summary>
///     Tests <see cref="HttpWarehouseGateway"/>'s response-mapping logic via a stub transport instead of a
///     mocking library - mirrors Payments' <c>IyzicoPaymentsGatewayTests</c> strategy: no real HTTP call, no
///     NSubstitute, just a fake <see cref="HttpMessageHandler"/> returning canned responses.
/// </summary>
public class HttpWarehouseGatewayTests
{
    [Fact]
    public async Task RequestReleaseAsync_SuccessResponse_ReturnsConfirmationReference()
    {
        using var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new { confirmationReference = "wh-ref-123" })
        };
        using var handler = new StubHandler(response);
        using var httpClient = CreateHttpClient(handler);
        var gateway = new HttpWarehouseGateway(httpClient);

        var result = await gateway.RequestReleaseAsync("product-1", 5, CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal("wh-ref-123", result.Value);
    }

    [Fact]
    public async Task RequestReleaseAsync_NotFoundResponse_ReturnsNotFoundError()
    {
        using var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        using var handler = new StubHandler(response);
        using var httpClient = CreateHttpClient(handler);
        var gateway = new HttpWarehouseGateway(httpClient);

        var result = await gateway.RequestReleaseAsync("product-1", 5, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("NotFound", result.Error!.Key);
    }

    [Fact]
    public async Task RequestReleaseAsync_ServerErrorResponse_ReturnsBadGatewayError()
    {
        using var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        using var handler = new StubHandler(response);
        using var httpClient = CreateHttpClient(handler);
        var gateway = new HttpWarehouseGateway(httpClient);

        var result = await gateway.RequestReleaseAsync("product-1", 5, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(HttpStatusCode.BadGateway, result.Error!.StatusCode);
    }

    [Fact]
    public async Task RequestReleaseAsync_MalformedBody_ReturnsMalformedResponseError()
    {
        using var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new { unexpectedField = "oops" })
        };
        using var handler = new StubHandler(response);
        using var httpClient = CreateHttpClient(handler);
        var gateway = new HttpWarehouseGateway(httpClient);

        var result = await gateway.RequestReleaseAsync("product-1", 5, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("WarehouseGatewayMalformedResponse", result.Error!.Key);
    }

    [Fact]
    public async Task RequestReleaseAsync_TransportThrows_PropagatesException()
    {
        using var handler = new ThrowingHandler();
        using var httpClient = CreateHttpClient(handler);
        var gateway = new HttpWarehouseGateway(httpClient);

        await Assert.ThrowsAsync<HttpRequestException>(
            () => gateway.RequestReleaseAsync("product-1", 5, CancellationToken.None));
    }

    private static HttpClient CreateHttpClient(HttpMessageHandler handler)
    {
        return new HttpClient(handler) { BaseAddress = new Uri("https://warehouse.example-partner.local/") };
    }

    private sealed class StubHandler(HttpResponseMessage response) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(response);
        }
    }

    private sealed class ThrowingHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new HttpRequestException("Connection reset by peer.");
        }
    }
}
