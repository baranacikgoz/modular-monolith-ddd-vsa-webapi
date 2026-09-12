using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class OtpOptions
{
    public required int Length { get; set; }
    public required int ExpirationInMinutes { get; set; }

    /// <summary>Seconds a client must wait before a new OTP is sent for the same phone, purpose and context.</summary>
    public required int ResendIntervalSeconds { get; set; }

    /// <summary>
    /// Max real SMS sent to one phone number within <see cref="PhoneQuotaWindowMinutes"/>, independent
    /// of Purpose/ContextId. Backstops the resend guard, which is keyed per-context and can be bypassed
    /// by varying ContextId or the caller's IP.
    /// </summary>
    public required int MaxSendsPerPhonePerWindow { get; set; }

    public required int PhoneQuotaWindowMinutes { get; set; }
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

        RuleFor(o => o.ResendIntervalSeconds)
            .GreaterThan(0)
            .WithMessage("ResendIntervalSeconds must be greater than 0.");

        RuleFor(o => o.MaxSendsPerPhonePerWindow)
            .GreaterThan(0)
            .WithMessage("MaxSendsPerPhonePerWindow must be greater than 0.");

        RuleFor(o => o.PhoneQuotaWindowMinutes)
            .GreaterThan(0)
            .WithMessage("PhoneQuotaWindowMinutes must be greater than 0.");
    }
}
