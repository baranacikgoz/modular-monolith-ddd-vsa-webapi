using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public sealed class SecurityHeadersOptions
{
    public Dictionary<string, string> Headers { get; init; } = [];
}

public sealed class SecurityHeadersOptionsValidator : CustomValidator<SecurityHeadersOptions>
{
    public SecurityHeadersOptionsValidator()
    {
        RuleForEach(o => o.Headers)
            .Must(kvp => !string.IsNullOrWhiteSpace(kvp.Key) && !string.IsNullOrWhiteSpace(kvp.Value))
            .WithMessage("Each security header entry must have a non-empty name and value.");
    }
}
