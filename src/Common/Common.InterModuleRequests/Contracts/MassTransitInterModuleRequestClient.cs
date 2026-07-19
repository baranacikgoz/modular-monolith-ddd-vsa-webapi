using Common.Application.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Common.InterModuleRequests.Contracts;

public class MassTransitInterModuleRequestClient<TRequest, TResponse>(
    IClientFactory clientFactory,
    IOptions<InterModuleRequestOptions> options)
    : IInterModuleRequestClient<TRequest, TResponse>
    where TRequest : class, IInterModuleRequest<TResponse>
    where TResponse : class
{
    public async Task<TResponse> SendAsync(TRequest request, CancellationToken cancellationToken)
    {
        var timeout = RequestTimeout.After(s: options.Value.TimeoutSeconds);
        var requestClient = clientFactory.CreateRequestClient<TRequest>(timeout);
        var response = await requestClient.GetResponse<TResponse>(request, cancellationToken);
        return response.Message;
    }
}
