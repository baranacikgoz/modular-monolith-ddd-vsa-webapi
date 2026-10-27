using System.Net;
using System.Net.Http.Json;

namespace Inventory.Tests.Support;

/// <summary>What the fake warehouse system received: enough to assert the wire shape without a mocking library.</summary>
public sealed record RecordedWarehouseRequest(
    HttpMethod Method, Uri? Uri, string? Authorization, string? ApiKey, string? ContentType, string Body);

/// <summary>
///     Stands in for the warehouse system's HTTP API: records every request that reaches it and answers with whatever
///     <see cref="Responder" /> returns. A transport failure is a responder that throws. Shared by unit tests (handed to
///     an <see cref="HttpClient" /> directly) and by integration tests (installed as the gateway client's primary handler).
/// </summary>
public sealed class FakeWarehouseHandler : HttpMessageHandler
{
    private readonly Lock _lock = new();
    private readonly List<RecordedWarehouseRequest> _requests = [];

    public Func<HttpRequestMessage, HttpResponseMessage> Responder { get; set; } = _ => new HttpResponseMessage(HttpStatusCode.OK);

    public IReadOnlyList<RecordedWarehouseRequest> Requests
    {
        get
        {
            lock (_lock)
            {
                return [.. _requests];
            }
        }
    }

    public void Reset()
    {
        lock (_lock)
        {
            _requests.Clear();
        }

        Responder = _ => new HttpResponseMessage(HttpStatusCode.OK);
    }

    public static HttpResponseMessage Json(HttpStatusCode status, object body)
    {
        return new HttpResponseMessage(status) { Content = JsonContent.Create(body) };
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var body = request.Content is null ? string.Empty : await request.Content.ReadAsStringAsync(cancellationToken);
        var apiKey = request.Headers.TryGetValues("X-Api-Key", out var values) ? values.Single() : null;

        lock (_lock)
        {
            _requests.Add(new RecordedWarehouseRequest(
                request.Method,
                request.RequestUri,
                request.Headers.Authorization?.ToString(),
                apiKey,
                request.Content?.Headers.ContentType?.MediaType,
                body));
        }

        return Responder(request);
    }
}
