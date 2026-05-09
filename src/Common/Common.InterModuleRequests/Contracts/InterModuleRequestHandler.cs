#pragma warning disable S1694 // Abstract class has a single abstract member: intentional — provides a named base type for DI scanning and implementors
namespace Common.InterModuleRequests.Contracts;

public abstract class InterModuleRequestHandler<TRequest, TResponse> : IInterModuleRequestHandler<TRequest, TResponse>
    where TRequest : class, IInterModuleRequest<TResponse>
    where TResponse : class
{
    public abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
#pragma warning restore S1694
