using Common.IntermoduleRequests.Contracts;
using MassTransit;

namespace Common.IntermoduleRequests;

public class MassTransitIntermoduleRequestClient<TRequest, TResponse>(
    IRequestClient<TRequest> requestClient
    ) : IIntermoduleRequestClient<TRequest, TResponse>
    where TRequest : class, IIntermoduleRequest<TResponse>
    where TResponse : class
{
    public async Task<TResponse> SendAsync(TRequest request, CancellationToken cancellationToken)
    {
        var response = await requestClient.GetResponse<TResponse>(request, cancellationToken);
        return response.Message;
    }
}
