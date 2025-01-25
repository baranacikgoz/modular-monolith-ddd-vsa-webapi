using Common.Application.Validation;
using FluentValidation;

namespace Common.Infrastructure.Options;

public class OtpOptions
{
    public int Length { get; set; }
    public int ExpirationInMinutes { get; set; }
}

public class OtpOptionsValidator : CustomValidator<OtpOptions>
{
    public OtpOptionsValidator()
    {
        RuleFor(o => o.Length)
            .GreaterThan(0)
            .WithMessage("Length must be greater than 0.");

        RuleFor(o => o.ExpirationInMinutes)
            .GreaterThan(0)
            .WithMessage("ExpirationInMinutes must be greater than 0.");
    }
}
