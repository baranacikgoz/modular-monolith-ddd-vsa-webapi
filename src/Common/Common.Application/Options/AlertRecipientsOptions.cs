using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

/// <summary>
///     Tunables of the critical alert fan-out: which realm role's members receive the alert email, how often the local
///     list of their addresses is re-read from IAM, and the text of the alert email. In the templates {0} is the alert
///     title and {1} its message.
/// </summary>
public class AlertRecipientsOptions
{
    /// <summary>
    ///     Realm role whose members Notifications keeps an email list for. A critical alert addressed to this role is
    ///     emailed to that list; an alert addressed to any other role is only pushed to the role's connected users.
    /// </summary>
    public required string RoleName { get; set; }

    /// <summary>
    ///     Cron for the recurring heal of the AlertRecipients projection from IAM's role members. There is no event for
    ///     a user gaining or losing a role, so this schedule is the only way the list follows.
    /// </summary>
    public required string ReconcileCron { get; set; }

    public required string EmailSubjectTemplate { get; set; }

    /// <summary>HTML body. The title and message are HTML-encoded before they are substituted.</summary>
    public required string EmailHtmlBodyTemplate { get; set; }

    public required string EmailTextBodyTemplate { get; set; }
}

public class AlertRecipientsOptionsValidator : CustomValidator<AlertRecipientsOptions>
{
    public AlertRecipientsOptionsValidator()
    {
        RuleFor(x => x.RoleName)
            .NotEmpty()
            .WithMessage("RoleName must not be empty.");

        RuleFor(x => x.ReconcileCron)
            .NotEmpty()
            .WithMessage("ReconcileCron must not be empty.");

        RuleFor(x => x.EmailSubjectTemplate)
            .NotEmpty()
            .WithMessage("EmailSubjectTemplate must not be empty.")
            .Must(template => template.Contains("{0}", StringComparison.Ordinal))
            .WithMessage("EmailSubjectTemplate must contain the {0} title placeholder.");

        RuleFor(x => x.EmailHtmlBodyTemplate)
            .NotEmpty()
            .WithMessage("EmailHtmlBodyTemplate must not be empty.")
            .Must(template => template.Contains("{1}", StringComparison.Ordinal))
            .WithMessage("EmailHtmlBodyTemplate must contain the {1} message placeholder.");

        RuleFor(x => x.EmailTextBodyTemplate)
            .NotEmpty()
            .WithMessage("EmailTextBodyTemplate must not be empty.")
            .Must(template => template.Contains("{1}", StringComparison.Ordinal))
            .WithMessage("EmailTextBodyTemplate must contain the {1} message placeholder.");
    }
}
