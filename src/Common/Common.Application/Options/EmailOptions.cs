using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public enum EmailProvider
{
    /// <summary>No-op, logs instead of sending. Non-production only.</summary>
    Dummy,
    Brevo
}

public class EmailOptions
{
    /// <summary>"Dummy" (non-production only, no-op) or "Brevo".</summary>
    public required EmailProvider Provider { get; set; }

    /// <summary>Brevo transactional API base URL, e.g. https://api.brevo.com. Required when Provider is Brevo.</summary>
    public string? BaseUrl { get; set; }

    /// <summary>Brevo API key, sent as the <c>api-key</c> header. Required when Provider is Brevo.</summary>
    public string? ApiKey { get; set; }

    /// <summary>Verified sender address in the Brevo account. Required when Provider is Brevo.</summary>
    public string? SenderEmail { get; set; }

    /// <summary>Sender display name shown to recipients.</summary>
    public string? SenderName { get; set; }

    /// <summary>Per-attempt timeout for the resilient HTTP client calling the email provider.</summary>
    public int AttemptTimeoutSeconds { get; set; }

    /// <summary>Total timeout across all retry attempts for a single send call.</summary>
    public int TotalRequestTimeoutSeconds { get; set; }

    /// <summary>
    /// Max retry attempts for the resilient HTTP client. Kept low (ideally 0-1): an email send is
    /// non-idempotent, so a retried request that actually delivered means a duplicate email.
    /// </summary>
    public int MaxRetryAttempts { get; set; }

    /// <summary>Max emails sent to one address per day, enforced by ThrottledEmailGateway.</summary>
    public int MaxPerAddressPerDay { get; set; }

    /// <summary>Max emails sent across all addresses per day, enforced by ThrottledEmailGateway.</summary>
    public int MaxPerDay { get; set; }

    /// <summary>
    /// TTL for ThrottledEmailGateway's per-day counters. The counter key buckets by UTC day, so this must
    /// stay above 24h or a bucket could expire before its day ends.
    /// </summary>
    public required int ThrottleCounterTtlHours { get; set; }

    public required EmailTemplatesOptions Templates { get; set; }
}

/// <summary>Future templates (e.g. OrderConfirmed) get added here as sibling language-code dictionaries.</summary>
public class EmailTemplatesOptions
{
    /// <summary>Language code (e.g. "en", "tr") -> OTP template. Must contain "{0}" as the OTP placeholder.</summary>
    public Dictionary<string, EmailTemplate> Otp { get; } = [];
}

public sealed class EmailTemplate
{
    public required string Subject { get; set; }
    public required string HtmlBody { get; set; }
    public string? TextBody { get; set; }
}

public class EmailOptionsValidator : CustomValidator<EmailOptions>
{
    public EmailOptionsValidator()
    {
        RuleFor(o => o.Templates.Otp)
            .NotEmpty()
            .WithMessage("Templates.Otp must contain at least one entry.");

        RuleForEach(o => o.Templates.Otp)
            .Must(kv => kv.Value.Subject.Contains("{0}", StringComparison.Ordinal)
                        || kv.Value.HtmlBody.Contains("{0}", StringComparison.Ordinal))
            .WithMessage("Each Templates.Otp entry must contain '{0}' as the OTP placeholder in Subject or HtmlBody.");

        RuleFor(o => o.ThrottleCounterTtlHours)
            .GreaterThanOrEqualTo(24)
            .WithMessage("ThrottleCounterTtlHours must be at least 24.");

        // DummyEmailGateway is a no-op: verification codes are generated but never reach the user.
        // In Production that silently bricks every email flow, so fail fast until Provider is switched
        // to Brevo (with real credentials) below.
        RuleFor(o => o.Provider)
            .Must((_, provider, context) => !context.IsProduction() || provider != EmailProvider.Dummy)
            .WithMessage(
                $"{nameof(EmailOptions)}.{nameof(EmailOptions.Provider)} is 'Dummy' in Production. Dummy email gateway is a " +
                "no-op, verification codes would never reach users. " +
                $"Set {nameof(EmailOptions.Provider)} to 'Brevo' (with real credentials) before deploying.");

        When(o => o.Provider == EmailProvider.Brevo, () =>
        {
            RuleFor(o => o.BaseUrl)
                .NotEmpty()
                .WithMessage("BaseUrl must not be empty when Provider is Brevo.")
                .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
                .WithMessage("BaseUrl must be a valid URL.");

            RuleFor(o => o.ApiKey)
                .NotEmpty()
                .WithMessage("ApiKey must not be empty when Provider is Brevo.");

            RuleFor(o => o.SenderEmail)
                .NotEmpty()
                .WithMessage("SenderEmail must not be empty when Provider is Brevo.")
                .EmailAddress()
                .WithMessage("SenderEmail must be a valid email address.");

            RuleFor(o => o.AttemptTimeoutSeconds)
                .GreaterThan(0)
                .WithMessage("AttemptTimeoutSeconds must be greater than 0.");

            RuleFor(o => o.TotalRequestTimeoutSeconds)
                .GreaterThanOrEqualTo(o => o.AttemptTimeoutSeconds)
                .WithMessage("TotalRequestTimeoutSeconds must be greater than or equal to AttemptTimeoutSeconds.");

            RuleFor(o => o.MaxRetryAttempts)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MaxRetryAttempts must be greater than or equal to 0.");

            RuleFor(o => o.MaxPerAddressPerDay)
                .GreaterThan(0)
                .WithMessage("MaxPerAddressPerDay must be greater than 0.");

            RuleFor(o => o.MaxPerDay)
                .GreaterThan(0)
                .WithMessage("MaxPerDay must be greater than 0.");
        });
    }
}
