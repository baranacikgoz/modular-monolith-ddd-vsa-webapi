using MassTransit;

namespace Common.InterModuleRequests.Contracts;

public class MassTransitInterModuleRequestClient<TRequest, TResponse>(IClientFactory clientFactory)
    : IInterModuleRequestClient<TRequest, TResponse>
    where TRequest : class, IInterModuleRequest<TResponse>
    where TResponse : class
{
    // Sync request/response inside HTTP request paths — fail fast rather than pinning
    // the caller for MassTransit's default 30s when the target module is down.
    private static readonly RequestTimeout Timeout = RequestTimeout.After(s: 10);

    public async Task<TResponse> SendAsync(TRequest request, CancellationToken cancellationToken)
    {
        var requestClient = clientFactory.CreateRequestClient<TRequest>(Timeout);
        var response = await requestClient.GetResponse<TResponse>(request, cancellationToken);
        return response.Message;
    }
}
