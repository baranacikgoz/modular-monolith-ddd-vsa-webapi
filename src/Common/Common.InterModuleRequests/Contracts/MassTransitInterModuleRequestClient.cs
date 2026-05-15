using MassTransit;

namespace Common.InterModuleRequests.Contracts;

public class MassTransitInterModuleRequestClient<TRequest, TResponse>(IClientFactory clientFactory)
    : IInterModuleRequestClient<TRequest, TResponse>
    where TRequest : class, IInterModuleRequest<TResponse>
    where TResponse : class
{
    public async Task<TResponse> SendAsync(TRequest request, CancellationToken cancellationToken)
    {
        var requestClient = clientFactory.CreateRequestClient<TRequest>();
        var response = await requestClient.GetResponse<TResponse>(request, cancellationToken);
        return response.Message;
    }
}
