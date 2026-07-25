using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public enum SmsProvider
{
    /// <summary>No-op, logs instead of sending. Non-production only.</summary>
    Dummy,
    NetGsm
}

public class SmsOptions
{
    /// <summary>"Dummy" (non-production only, no-op) or "NetGsm".</summary>
    public required SmsProvider Provider { get; set; }

    /// <summary>NetGSM REST API base URL, e.g. https://api.netgsm.com.tr. Required when Provider is NetGsm.</summary>
    public string? BaseUrl { get; set; }

    /// <summary>NetGSM account user code. Required when Provider is NetGsm.</summary>
    public string? UserCode { get; set; }

    /// <summary>NetGSM account password. Required when Provider is NetGsm.</summary>
    public string? Password { get; set; }

    /// <summary>Approved NetGSM sender header (msgheader). Required when Provider is NetGsm.</summary>
    public string? MsgHeader { get; set; }

    /// <summary>Optional NetGSM "appname" parameter, surfaced in their delivery reports.</summary>
    public string? AppName { get; set; }

    /// <summary>Per-attempt timeout for the resilient HTTP client calling the SMS provider.</summary>
    public int AttemptTimeoutSeconds { get; set; }

    /// <summary>Total timeout across all retry attempts for a single send call.</summary>
    public int TotalRequestTimeoutSeconds { get; set; }

    /// <summary>
    /// Max retry attempts for the resilient HTTP client. Kept low (ideally 0-1): an SMS send is
    /// non-idempotent, so a retried request that actually delivered means a duplicate SMS.
    /// </summary>
    public int MaxRetryAttempts { get; set; }

    /// <summary>Max SMS sends per phone number per day, enforced by ThrottledSmsGateway.</summary>
    public int MaxPerPhoneNumberPerDay { get; set; }

    /// <summary>Max SMS sends across all phone numbers per day, enforced by ThrottledSmsGateway.</summary>
    public int MaxPerDay { get; set; }

    public required SmsTemplatesOptions Templates { get; set; }
}

/// <summary>Future templates (e.g. OrderConfirmed) get added here as sibling language-code dictionaries.</summary>
public class SmsTemplatesOptions
{
    /// <summary>Language code (e.g. "en", "tr") -> template. Must contain "{0}" as the OTP placeholder.</summary>
    public Dictionary<string, string> Otp { get; } = [];
}

public class SmsOptionsValidator : CustomValidator<SmsOptions>
{
    public SmsOptionsValidator()
    {
        RuleFor(o => o.Templates.Otp)
            .NotEmpty()
            .WithMessage("Templates.Otp must contain at least one entry.");

        RuleForEach(o => o.Templates.Otp)
            .Must(kv => kv.Value.Contains("{0}", StringComparison.Ordinal))
            .WithMessage("Each Templates.Otp value must contain '{0}' as the OTP placeholder.");

        // DummySmsGateway is a no-op: OTPs and notifications are generated but never reach the user.
        // In Production that silently bricks every SMS flow, so fail fast until Provider is switched
        // to NetGsm (with real credentials) below.
        RuleFor(o => o.Provider)
            .Must((_, provider, context) => !context.IsProduction() || provider != SmsProvider.Dummy)
            .WithMessage(
                $"{nameof(SmsOptions)}.{nameof(SmsOptions.Provider)} is 'Dummy' in Production. Dummy SMS gateway is a no-op " +
                "— OTPs and notifications would never reach users. " +
                $"Set {nameof(SmsOptions.Provider)} to 'NetGsm' (with real credentials) before deploying.");

        When(o => o.Provider == SmsProvider.NetGsm, () =>
        {
            RuleFor(o => o.BaseUrl)
                .NotEmpty()
                .WithMessage("BaseUrl must not be empty when Provider is NetGsm.")
                .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
                .WithMessage("BaseUrl must be a valid URL.");

            RuleFor(o => o.UserCode)
                .NotEmpty()
                .WithMessage("UserCode must not be empty when Provider is NetGsm.");

            RuleFor(o => o.Password)
                .NotEmpty()
                .WithMessage("Password must not be empty when Provider is NetGsm.");

            RuleFor(o => o.MsgHeader)
                .NotEmpty()
                .WithMessage("MsgHeader must not be empty when Provider is NetGsm.");

            RuleFor(o => o.AttemptTimeoutSeconds)
                .GreaterThan(0)
                .WithMessage("AttemptTimeoutSeconds must be greater than 0.");

            RuleFor(o => o.TotalRequestTimeoutSeconds)
                .GreaterThanOrEqualTo(o => o.AttemptTimeoutSeconds)
                .WithMessage("TotalRequestTimeoutSeconds must be greater than or equal to AttemptTimeoutSeconds.");

            RuleFor(o => o.MaxRetryAttempts)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MaxRetryAttempts must be greater than or equal to 0.");

            RuleFor(o => o.MaxPerPhoneNumberPerDay)
                .GreaterThan(0)
                .WithMessage("MaxPerPhoneNumberPerDay must be greater than 0.");

            RuleFor(o => o.MaxPerDay)
                .GreaterThan(0)
                .WithMessage("MaxPerDay must be greater than 0.");
        });
    }
}
