using Common.Application.EventBus;
using Common.Application.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Common.InterModuleRequests.Contracts;

/// <summary>
///     Receive endpoint policy for synchronous request/response handlers. The bus-level defaults in the Host
///     (exponential retry, event consumer concurrency) are meant for integration event consumers: a failing
///     read handler retried for minutes only pins the caller, who gave up after
///     <see cref="InterModuleRequestOptions.TimeoutSeconds"/>. So: no retry (the caller sees the fault at once
///     and owns any retry), its own concurrency, and a handler timeout equal to the caller's request timeout
///     (<see cref="InterModuleRequestOptions.TimeoutSecondsFor"/>, so a per-request override applies to both sides).
///     Registered per handler type by the Host's MassTransit setup.
/// </summary>
public class InterModuleRequestHandlerDefinition<THandler>(IOptions<InterModuleRequestOptions> options)
    : ConsumerDefinition<THandler>
    where THandler : class, IConsumer
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<THandler> consumerConfigurator,
        IRegistrationContext context)
    {
        var opts = options.Value;

        endpointConfigurator.UseMessageRetry(r => r.None());
        endpointConfigurator.SetConcurrency(opts.HandlerPrefetchCount, opts.HandlerConcurrentMessageLimit);
        endpointConfigurator.UseTimeout(t => t.Timeout = TimeSpan.FromSeconds(opts.TimeoutSecondsFor(RequestType)));
    }

    /// <summary>The TRequest of the <see cref="InterModuleRequestHandler{TRequest,TResponse}"/> the handler derives from.</summary>
    private static Type RequestType { get; } = ResolveRequestType();

    private static Type ResolveRequestType()
    {
        for (var current = typeof(THandler).BaseType; current is not null; current = current.BaseType)
        {
            if (current.IsGenericType && current.GetGenericTypeDefinition() == typeof(InterModuleRequestHandler<,>))
            {
                return current.GetGenericArguments()[0];
            }
        }

        throw new InvalidOperationException(
            $"{typeof(THandler).Name} does not derive from {typeof(InterModuleRequestHandler<,>).Name}.");
    }
}
