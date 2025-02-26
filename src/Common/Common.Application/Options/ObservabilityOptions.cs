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

    public bool LogGeneratedSqlQueries { get; set; }

    public int ResponseTimeThresholdInMs { get; set; }

    public required string OtlpLoggingEndpoint { get; set; }

    public required string OtlpLoggingProtocol { get; set; }

    public bool EnableMetrics { get; set; }

    public bool OtlpMetricsUsePrometheusDirectly { get; set; }

    public string? OtlpMetricsEndpoint { get; set; }

    public string? OtlpMetricsProtocol { get; set; }

    public bool EnableTracing { get; set; }

    public required string OtlpTracingEndpoint { get; set; }

    public required string OtlpTracingProtocol { get; set; }
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

        RuleFor(o => o.OtlpLoggingEndpoint)
            .NotEmpty()
            .WithMessage("OtlpLoggingEndpoint must not be empty.");

        RuleFor(o => o.OtlpLoggingProtocol)
            .NotEmpty()
            .WithMessage("OtlpLoggingProtocol must not be empty.")
            .Matches("HttpProtobuf|Grpc");

        RuleFor(o => o.OtlpMetricsEndpoint)
            .NotEmpty()
            .WithMessage("OtlpMetricsEndpoint must not be empty.")
            .When(o => o.EnableMetrics);

        RuleFor(o => o.OtlpMetricsProtocol)
            .NotEmpty()
            .WithMessage("OtlpMetricsProtocol must not be empty.")
            .Matches("HttpProtobuf|Grpc")
            .When(o => o.EnableMetrics);

        RuleFor(o => o.OtlpTracingEndpoint)
            .NotEmpty()
            .WithMessage("OtlpTracingEndpoint must not be empty.")
            .When(o => o.EnableTracing);

        RuleFor(o => o.OtlpTracingProtocol)
            .NotEmpty()
            .WithMessage("OtlpTracingProtocol must not be empty.")
            .Matches("HttpProtobuf|Grpc")
            .When(o => o.EnableTracing);
    }
}
