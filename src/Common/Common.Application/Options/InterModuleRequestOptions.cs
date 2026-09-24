using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class InterModuleRequestOptions
{
    // Sync request/response inside HTTP request paths: fail fast rather than pinning
    // the caller for MassTransit's default 30s when the target module is down.
    public required int TimeoutSeconds { get; set; }

    // Per request type override, keyed by the request record's type name (e.g. "GetProductRequest"),
    // in seconds, applied to the caller and the handler alike. Absent key: TimeoutSeconds. The initializer
    // stays although the property is required: an empty JSON object ("Timeouts": {}) binds to nothing.
    public required Dictionary<string, int> Timeouts { get; init; } = [];

    // Receive endpoint concurrency for request handlers (InterModuleRequestHandlerDefinition), separate
    // from the bus-level event consumer defaults.
    public required int HandlerPrefetchCount { get; set; }
    public required int HandlerConcurrentMessageLimit { get; set; }

    // Retry-After hint on the 503 the Host returns when a request to another module times out.
    public required int DependencyUnavailableRetryAfterSeconds { get; set; }

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
