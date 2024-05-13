using MassTransit;

namespace Common.InterModuleRequests.Contracts;

/// <summary>
/// This is a mechanism to force returning response directly using <see cref="HandleAsync"/> method providing a mediator-like experience,
/// rather than expecting developers explicitly call context.RespondAsync(response).
/// Since <see cref="Consume"> method returns merely a Task,
/// Some may forget to call context.RespondAsync(response) and this will cause the request to be stuck in the queue.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public abstract class InterModuleRequestHandler<TRequest, TResponse> : IConsumer<TRequest>
    where TRequest : class, IInterModuleRequest<TResponse>
    where TResponse : class
{
    public async Task Consume(ConsumeContext<TRequest> context)
    {
        var response = await HandleAsync(context.Message, context.CancellationToken);
        await context.RespondAsync(response);
    }

    protected abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
