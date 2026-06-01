using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class SensitivePathRule
{
    public string Path { get; set; } = string.Empty;
    public IList<string> Methods { get; } = [];
}

public class RequestLoggingOptions
{
    public required bool LogRequestBody { get; set; }
    public required bool LogResponseBody { get; set; }
    public required int RequestBodyLogLimitBytes { get; set; }
    public required int ResponseBodyLogLimitBytes { get; set; }

    public required bool LogQueryString { get; set; }
    public required IList<string> ExcludedPathPrefixes { get; init; } = [];

    public IList<SensitivePathRule> SensitiveRequestBodyPaths { get; } = [];
    public IList<SensitivePathRule> SensitiveResponseBodyPaths { get; } = [];
    public IList<SensitivePathRule> SensitiveQueryParamPaths { get; } = [];
}

public class RequestLoggingOptionsValidator : CustomValidator<RequestLoggingOptions>
{
    private const int MaxBodyLogLimitBytes = 65536;

    public RequestLoggingOptionsValidator()
    {
        When(o => o.LogRequestBody, () =>
            RuleFor(o => o.RequestBodyLogLimitBytes)
                .GreaterThan(0).LessThanOrEqualTo(MaxBodyLogLimitBytes));

        When(o => o.LogResponseBody, () =>
            RuleFor(o => o.ResponseBodyLogLimitBytes)
                .GreaterThan(0).LessThanOrEqualTo(MaxBodyLogLimitBytes));
    }
}
