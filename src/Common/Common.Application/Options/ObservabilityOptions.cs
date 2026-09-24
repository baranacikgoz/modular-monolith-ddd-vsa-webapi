using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class ObservabilityOptions
{
    public required string AppName { get; set; }
    public required string AppVersion { get; set; }
    public required string MinimumLevel { get; set; }
    public Dictionary<string, string> MinimumLevelOverrides { get; } = [];
    public bool WriteToConsole { get; set; }
    public bool WriteToFile { get; set; }
    public int ResponseTimeThresholdInMs { get; set; }

    // Remote log sink: "Seq" | "Elasticsearch": omit to use console/file only
    public string? LogSink { get; set; }
    public string? SeqServerUrl { get; set; }
    public string? ElasticsearchUrl { get; set; }

    // OTEL traces + metrics → single collector endpoint
    public bool EnableMetrics { get; set; }
    public bool EnableTracing { get; set; }
    public string? OtlpEndpoint { get; set; }
    public string? OtlpProtocol { get; set; }

    // Fraction of new traces recorded (ParentBased: a sampled parent always wins). Nullable so a missing
    // key fails NotNull at boot instead of binding to 0 (tracing silently off).
    public required double? TraceSamplingRatio { get; set; }
}

public class ObservabilityOptionsValidator : CustomValidator<ObservabilityOptions>
{
    public ObservabilityOptionsValidator()
    {
        RuleFor(o => o.AppName)
            .NotEmpty()
            .WithMessage("AppName must not be empty.");

        RuleFor(o => o.AppVersion)
            .NotEmpty()
            .WithMessage("AppVersion must not be empty.");

        RuleFor(o => o.MinimumLevel)
            .NotEmpty()
            .WithMessage("MinimumLevel must not be empty.");

        RuleFor(o => o.MinimumLevelOverrides)
            .NotNull()
            .WithMessage("MinimumLevelOverrides must not be null.")
            .NotEmpty()
            .WithMessage("MinimumLevelOverrides must contain at least one override.");

        RuleFor(o => o.ResponseTimeThresholdInMs)
            .GreaterThan(0)
            .WithMessage("ResponseTimeThresholdInMs must be greater than 0.");

        RuleFor(o => o.LogSink)
            .Matches("^(Seq|Elasticsearch|Otlp)$")
            .WithMessage("LogSink must be 'Seq', 'Elasticsearch', or 'Otlp'.")
            .When(o => !string.IsNullOrEmpty(o.LogSink));

        RuleFor(o => o.SeqServerUrl)
            .NotEmpty()
            .WithMessage("SeqServerUrl must not be empty when LogSink is 'Seq'.")
            .When(o => o.LogSink == "Seq");

        RuleFor(o => o.ElasticsearchUrl)
            .NotEmpty()
            .WithMessage("ElasticsearchUrl must not be empty when LogSink is 'Elasticsearch'.")
            .When(o => o.LogSink == "Elasticsearch");

        RuleFor(o => o.OtlpEndpoint)
            .NotEmpty()
            .WithMessage("OtlpEndpoint must not be empty when tracing or metrics are enabled.")
            .When(o => o.EnableTracing || o.EnableMetrics);

        RuleFor(o => o.OtlpProtocol)
            .NotEmpty()
            .Matches("HttpProtobuf|Grpc")
            .WithMessage("OtlpProtocol must be 'HttpProtobuf' or 'Grpc'.")
            .When(o => o.EnableTracing || o.EnableMetrics);

        RuleFor(o => o.TraceSamplingRatio)
            .NotNull()
            .WithMessage("TraceSamplingRatio is required.");

        RuleFor(o => o.TraceSamplingRatio)
            .InclusiveBetween(0.0, 1.0)
            .WithMessage("TraceSamplingRatio must be between 0 and 1 inclusive.")
            .When(o => o.TraceSamplingRatio is not null);
    }
}
