using Polly;

namespace Common.Infrastructure.Resiliency;

public static class HttpClientKeyedExtensions
{
    /// <summary>
    ///     Sends <paramref name="request" /> through <paramref name="pipeline" /> (see <see cref="KeyedResiliencePipelines" />).
    ///     The pipeline's own cancellation token (attempt timeout) is what reaches the send.
    /// </summary>
    public static async Task<HttpResponseMessage> SendWithPipelineAsync(
        this HttpClient httpClient,
        HttpRequestMessage request,
        ResiliencePipeline<HttpResponseMessage> pipeline,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(pipeline);

        return await pipeline.ExecuteAsync(
            static async (state, ct) => await state.Client.SendAsync(state.Request, ct),
            (Client: httpClient, Request: request),
            cancellationToken);
    }
}
