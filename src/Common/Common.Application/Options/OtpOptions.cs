using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class OtpOptions
{
    public required int Length { get; set; }
    public required int ExpirationInMinutes { get; set; }
    public Dictionary<string, string> SmsTemplates { get; } = [];
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

        RuleFor(o => o.SmsTemplates)
            .NotEmpty()
            .WithMessage("SmsTemplates must contain at least one entry.");

        RuleForEach(o => o.SmsTemplates)
            .Must(kv => kv.Value.Contains("{0}", StringComparison.Ordinal))
            .WithMessage("Each SmsTemplate value must contain '{0}' as the OTP placeholder.");
    }
}
