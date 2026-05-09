namespace Common.InterModuleRequests.Contracts;

public interface IInterModuleRequestHandler<in TRequest, TResponse>
    where TRequest : class, IInterModuleRequest<TResponse>
    where TResponse : class
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
