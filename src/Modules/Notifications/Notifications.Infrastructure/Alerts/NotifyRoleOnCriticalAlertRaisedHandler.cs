using System.Net;
using System.Text.RegularExpressions;
using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.Application.Email;
using Notifications.Application.Hubs;
using Notifications.Application.Persistence;
using Notifications.Infrastructure.Hubs;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.Alerts;

/// <summary>
///     Delivers a critical alert addressed to a role: pushes it to every user connected right now who holds the role, and
///     emails the projected recipients when the role is the one <see cref="AlertRecipientsOptions.RoleName" /> keeps a list
///     for. There is no persisted feed, so the push reaches only users connected now; the email is the durable channel.
///     Best effort per channel and per address: a failure is logged and never thrown, because a retry of this consumer would
///     send the same email again to the addresses that already got it.
/// </summary>
public sealed partial class NotifyRoleOnCriticalAlertRaisedHandler(
    INotificationDispatcher dispatcher,
    IEmailGateway emailGateway,
    INotificationsDbContext dbContext,
    IOptions<AlertRecipientsOptions> alertOptions,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<INotificationsDbContext> inbox,
    ILogger<NotifyRoleOnCriticalAlertRaisedHandler> logger
) : IntegrationEventHandlerBase<CriticalAlertRaisedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    public const string PayloadType = "notifications.critical-alert";

    protected override async Task ProcessAsync(CriticalAlertRaisedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        await PushAsync(@event, cancellationToken);

        var options = alertOptions.Value;
        if (!string.Equals(@event.RoleName, options.RoleName, StringComparison.Ordinal))
        {
            LogNoEmailListForRole(logger, @event.Id, @event.RoleName);
            return;
        }

        var recipients = await dbContext.AlertRecipients
            .AsNoTracking()
            .TagWith(nameof(ProcessAsync))
            .Select(r => r.Email)
            .ToListAsync(cancellationToken);

        if (recipients.Count == 0)
        {
            LogNoRecipients(logger, @event.Id, @event.RoleName);
            return;
        }

        var subject = Fill(options.EmailSubjectTemplate, @event.Title.ReplaceLineEndings(" "), @event.Message.ReplaceLineEndings(" "));
        var html = Fill(options.EmailHtmlBodyTemplate, WebUtility.HtmlEncode(@event.Title), WebUtility.HtmlEncode(@event.Message));
        var text = Fill(options.EmailTextBodyTemplate, @event.Title, @event.Message);

        foreach (var email in recipients)
        {
            await SendEmailAsync(@event, new EmailMessage(email, subject, html, text), cancellationToken);
        }
    }

    private async Task PushAsync(CriticalAlertRaisedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            await dispatcher.SendToGroupAsync(
                NotificationGroupName.ForRole(@event.RoleName),
                new NotificationPayload(PayloadType, @event.Id.ToString(), @event.Title, @event.Message),
                cancellationToken);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            LogPushFailed(logger, @event.Id, @event.RoleName, ex);
        }
    }

    private async Task SendEmailAsync(CriticalAlertRaisedIntegrationEvent @event, EmailMessage message, CancellationToken cancellationToken)
    {
        try
        {
            var sent = await emailGateway.SendAsync(message, cancellationToken);
            if (sent.Error is { } error)
            {
                LogEmailFailed(logger, @event.Id, error.Key);
            }
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            LogEmailThrew(logger, @event.Id, ex);
        }
    }

    // One pass over the template, so a placeholder-looking text inside the title is never substituted again. Plain
    // substitution rather than string.Format: an HTML body legitimately contains braces.
    private static string Fill(string template, string title, string message)
    {
        return PlaceholderPattern().Replace(template, match => match.Value == "{0}" ? title : message);
    }

    [GeneratedRegex(@"\{[01]\}")]
    private static partial Regex PlaceholderPattern();

    [LoggerMessage(Level = LogLevel.Warning, Message = "Pushing the critical alert {AlertId} to the {RoleName} role group failed.")]
    private static partial void LogPushFailed(ILogger logger, Guid alertId, string roleName, Exception exception);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Critical alert {AlertId} is addressed to role {RoleName}, which has no email list; it was pushed only.")]
    private static partial void LogNoEmailListForRole(ILogger logger, Guid alertId, string roleName);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Critical alert {AlertId} for role {RoleName} has no email recipient (the alert recipients projection is empty).")]
    private static partial void LogNoRecipients(ILogger logger, Guid alertId, string roleName);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Critical alert {AlertId} email was not sent: {ErrorKey}.")]
    private static partial void LogEmailFailed(ILogger logger, Guid alertId, string errorKey);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Sending the critical alert {AlertId} email threw.")]
    private static partial void LogEmailThrew(ILogger logger, Guid alertId, Exception exception);
}
