using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class CorsOptions
{
    public IReadOnlyList<string> AllowedOrigins { get; init; } = [];
    public IReadOnlyList<string> AllowedMethods { get; init; } = [];
    public IReadOnlyList<string> AllowedHeaders { get; init; } = [];
    public bool AllowCredentials { get; init; }
    public int MaxAgeInSeconds { get; init; } = 600;
}

public class CorsOptionsValidator : CustomValidator<CorsOptions>
{
    public CorsOptionsValidator()
    {
        RuleFor(o => o.MaxAgeInSeconds)
            .GreaterThan(0)
            .WithMessage("MaxAgeInSeconds must be greater than 0.");

        RuleFor(o => o)
            .Must(o => !o.AllowCredentials || !o.AllowedOrigins.Contains("*"))
            .WithMessage("AllowCredentials cannot be true when AllowedOrigins contains a wildcard ('*').");
    }
}
