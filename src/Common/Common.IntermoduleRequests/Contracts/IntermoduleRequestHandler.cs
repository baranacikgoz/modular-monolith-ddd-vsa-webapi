﻿using MassTransit;

namespace Common.IntermoduleRequests.Contracts;

/// <summary>
/// This is a mechanism to force returning response directly using <see cref="HandleAsync"/> method providing a mediator-like experience,
/// rather than expecting developers explicitly call context.RespondAsync(response).
/// Since <see cref="Consume"> method returns merely a Task,
/// Some may forget to call context.RespondAsync(response) and this will cause the request to be stuck in the queue.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public abstract class IntermoduleRequestHandler<TRequest, TResponse> : IConsumer<TRequest>
    where TRequest : class, IIntermoduleRequest<TResponse>
    where TResponse : class
{
    public async Task Consume(ConsumeContext<TRequest> context)
    {
        var response = HandleAsync(context.Message, context.CancellationToken);
        await context.RespondAsync(response);
    }

    protected abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
