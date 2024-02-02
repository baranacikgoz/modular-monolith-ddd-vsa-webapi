namespace Common.IntermoduleRequests.Contracts;

public interface IIntermoduleRequestClient<TRequest, TResponse>
    where TRequest : class, IIntermoduleRequest<TResponse>
    where TResponse : class
{
    Task<TResponse> SendAsync(TRequest request, CancellationToken cancellationToken);
}
