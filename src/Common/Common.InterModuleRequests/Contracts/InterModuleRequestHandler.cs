using MassTransit;

namespace Common.InterModuleRequests.Contracts;

public abstract class InterModuleRequestHandler<TRequest, TResponse>
    : IInterModuleRequestHandler<TRequest, TResponse>, IConsumer<TRequest>
    where TRequest : class, IInterModuleRequest<TResponse>
    where TResponse : class
{
    public async Task Consume(ConsumeContext<TRequest> context)
    {
        var response = await HandleAsync(context.Message, context.CancellationToken);
        await context.RespondAsync(response);
    }

    public abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
