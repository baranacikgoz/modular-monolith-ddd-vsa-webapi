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

    /// <summary>
    /// When set, every generated OTP is this fixed code instead of a random one, regardless of whether
    /// the OTP store is Redis or in-memory. Lets non-production environments be exercised without reading
    /// SMS. Forbidden in Production.
    /// </summary>
    public string? DummyCode { get; set; }
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

        RuleFor(o => o.DummyCode)
            .Must((o, code) => code is null || (code.Length == o.Length && code.All(char.IsAsciiDigit)))
            .WithMessage("DummyCode must be exactly Length digits when set.");

        // A fixed OTP is a publicly known constant: anyone can pass verification for any phone number.
        RuleFor(o => o.DummyCode)
            .Must((_, code, context) => !context.IsProduction() || code is null)
            .WithMessage(
                $"{nameof(OtpOptions)}.{nameof(OtpOptions.DummyCode)} is set in Production. Every OTP would be a fixed, " +
                "publicly known code. Remove it before deploying.");
    }
}
