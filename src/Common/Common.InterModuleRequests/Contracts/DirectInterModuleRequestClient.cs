using Microsoft.Extensions.DependencyInjection;

namespace Common.InterModuleRequests.Contracts;

public class DirectInterModuleRequestClient<TRequest, TResponse>(IServiceProvider serviceProvider)
    : IInterModuleRequestClient<TRequest, TResponse>
    where TRequest : class, IInterModuleRequest<TResponse>
    where TResponse : class
{
    public Task<TResponse> SendAsync(TRequest request, CancellationToken cancellationToken)
    {
        var handler = serviceProvider.GetRequiredService<IInterModuleRequestHandler<TRequest, TResponse>>();
        return handler.HandleAsync(request, cancellationToken);
    }
}
