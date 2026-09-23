using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class InterModuleRequestOptions
{
    // Sync request/response inside HTTP request paths: fail fast rather than pinning
    // the caller for MassTransit's default 30s when the target module is down.
    public required int TimeoutSeconds { get; set; }

    // Per request type override, keyed by the request record's type name (e.g. "GetProductRequest"),
    // in seconds. Absent key: TimeoutSeconds.
    public Dictionary<string, int> Timeouts { get; } = [];

    // Receive endpoint concurrency for request handlers (InterModuleRequestHandlerDefinition), separate
    // from the bus-level event consumer defaults. Plain C# defaults equal to interModuleRequest.json on
    // purpose: added after the file was deployed, a required property would crash-loop a Vault value that
    // predates it (CLAUDE.md, options pattern).
    public int HandlerPrefetchCount { get; set; } = 32;
    public int HandlerConcurrentMessageLimit { get; set; } = 32;

    // Retry-After hint on the 503 the Host returns when a request to another module faults or times out.
    // Plain default for the same already-deployed-file reason as above.
    public int DependencyUnavailableRetryAfterSeconds { get; set; } = 2;

    public int TimeoutSecondsFor(Type requestType)
        => Timeouts.TryGetValue(requestType.Name, out var perRequest) ? perRequest : TimeoutSeconds;
}

public class InterModuleRequestOptionsValidator : CustomValidator<InterModuleRequestOptions>
{
    public InterModuleRequestOptionsValidator()
    {
        RuleFor(o => o.TimeoutSeconds)
            .GreaterThan(0)
            .WithMessage("TimeoutSeconds must be greater than 0.");

        RuleFor(o => o.Timeouts)
            .NotNull()
            .WithMessage("Timeouts must not be null.");

        RuleForEach(o => o.Timeouts)
            .Must(pair => pair.Value > 0)
            .WithMessage((_, pair) => $"Timeouts[{pair.Key}] must be greater than 0.");

        RuleFor(o => o.HandlerPrefetchCount)
            .GreaterThan(0)
            .WithMessage("HandlerPrefetchCount must be greater than 0.");

        RuleFor(o => o.HandlerConcurrentMessageLimit)
            .GreaterThan(0)
            .WithMessage("HandlerConcurrentMessageLimit must be greater than 0.");

        RuleFor(o => o.DependencyUnavailableRetryAfterSeconds)
            .GreaterThan(0)
            .WithMessage("DependencyUnavailableRetryAfterSeconds must be greater than 0.");
    }
}
