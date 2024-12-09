using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;
using FluentValidation;

namespace Common.Infrastructure.Options;

public class OtpOptions
{
    public int ExpirationInMinutes { get; set; }
}

public class OtpOptionsValidator : CustomValidator<OtpOptions>
{
    public OtpOptionsValidator()
    {
        RuleFor(o => o.ExpirationInMinutes)
            .GreaterThan(0)
            .WithMessage("ExpirationInMinutes must be greater than 0.");
    }
}
