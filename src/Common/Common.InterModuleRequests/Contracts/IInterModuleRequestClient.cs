namespace Common.InterModuleRequests.Contracts;

public interface IInterModuleRequestClient<TRequest, TResponse>
    where TRequest : class, IInterModuleRequest<TResponse>
    where TResponse : class
{
    Task<TResponse> SendAsync(TRequest request, CancellationToken cancellationToken);
}
