using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Common.Tests;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Email;
using Notifications.Application.Hubs;
using Notifications.Application.Persistence;
using Notifications.Domain.Alerts;
using Notifications.Infrastructure.Alerts;
using Notifications.Infrastructure.Hubs;
using NSubstitute;
using Xunit;

namespace Notifications.Tests.Alerts;

#pragma warning disable CA1707 // Remove the underscores from member name

/// <summary>A critical alert addressed to a role is pushed to the role's connected users and emailed to the projected recipients, once.</summary>
[Collection("IntegrationTestCollection")]
public sealed class NotifyRoleOnCriticalAlertRaisedHandlerTests(NotificationsTestFactory factory) : BaseIntegrationTest(factory)
{
    private static CriticalAlertRaisedIntegrationEvent Alert(string roleName = KeycloakRoles.Staff) =>
        new(roleName, "Disk almost full", "The data disk is 98% full.");

    private async Task SeedRecipientsAsync(params string[] emails)
    {
        var db = Scope.ServiceProvider.GetRequiredService<INotificationsDbContext>();
        foreach (var email in emails)
        {
            db.AlertRecipients.Add(AlertRecipient.Create(ApplicationUserId.New(), email, DateTimeOffset.UtcNow));
        }

        await db.SaveChangesAsync();
    }

    private async Task DeliverAsync(CriticalAlertRaisedIntegrationEvent @event, INotificationDispatcher dispatcher, IEmailGateway email)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var handler = ActivatorUtilities.CreateInstance<NotifyRoleOnCriticalAlertRaisedHandler>(scope.ServiceProvider, dispatcher, email);
        var context = Substitute.For<ConsumeContext<CriticalAlertRaisedIntegrationEvent>>();
        context.Message.Returns(@event);
        context.CancellationToken.Returns(CancellationToken.None);
        await handler.Consume(context);
    }

    [Fact]
    public void Handler_IsRegisteredAsAConsumerInTheContainer()
    {
        var handler = Scope.ServiceProvider.GetService<NotifyRoleOnCriticalAlertRaisedHandler>();

        Assert.NotNull(handler);
    }

    [Fact]
    public async Task Consume_PushesToTheRoleGroup_AndEmailsEveryRecipient()
    {
        await SeedRecipientsAsync("one@example.com", "two@example.com");
        var dispatcher = Substitute.For<INotificationDispatcher>();
        var email = Substitute.For<IEmailGateway>();
        email.SendAsync(Arg.Any<EmailMessage>(), Arg.Any<CancellationToken>()).Returns(Result.Success);
        var @event = Alert();

        await DeliverAsync(@event, dispatcher, email);

        await dispatcher.Received(1).SendToGroupAsync(
            NotificationGroupName.ForRole(KeycloakRoles.Staff),
            Arg.Is<NotificationPayload>(p => p.Type == NotifyRoleOnCriticalAlertRaisedHandler.PayloadType
                                             && p.ResourceId == @event.Id.ToString()
                                             && p.Title == @event.Title
                                             && p.Message == @event.Message),
            Arg.Any<CancellationToken>());
        await email.Received(1).SendAsync(
            Arg.Is<EmailMessage>(m => m.To == "one@example.com"
                                      && m.Subject == "[Critical alert] Disk almost full"
                                      && m.HtmlBody == "<h3>Disk almost full</h3><p>The data disk is 98% full.</p>"
                                      && m.TextBody == "Disk almost full\n\nThe data disk is 98% full."),
            Arg.Any<CancellationToken>());
        await email.Received(1).SendAsync(Arg.Is<EmailMessage>(m => m.To == "two@example.com"), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Consume_TitleAndMessageWithMarkup_AreEncodedInTheHtmlBodyAndFlattenedInTheSubject()
    {
        await SeedRecipientsAsync("one@example.com");
        var dispatcher = Substitute.For<INotificationDispatcher>();
        var email = Substitute.For<IEmailGateway>();
        email.SendAsync(Arg.Any<EmailMessage>(), Arg.Any<CancellationToken>()).Returns(Result.Success);
        var @event = new CriticalAlertRaisedIntegrationEvent(KeycloakRoles.Staff, "Queue {1}\r\n<b>stuck</b>", "Retry <script>x</script>");

        await DeliverAsync(@event, dispatcher, email);

        await email.Received(1).SendAsync(
            Arg.Is<EmailMessage>(m => m.Subject == "[Critical alert] Queue {1} <b>stuck</b>"
                                      && m.HtmlBody == "<h3>Queue {1}\r\n&lt;b&gt;stuck&lt;/b&gt;</h3><p>Retry &lt;script&gt;x&lt;/script&gt;</p>"
                                      && m.TextBody == "Queue {1}\r\n<b>stuck</b>\n\nRetry <script>x</script>"),
            Arg.Any<CancellationToken>());
    }

    /// <summary>A failing push or email must not throw: a retry of the consumer would mail the others twice.</summary>
    [Fact]
    public async Task Consume_PushAndOneEmailFail_StillTriesTheRestAndDoesNotThrow()
    {
        await SeedRecipientsAsync("bad@example.com", "good@example.com");
        var dispatcher = Substitute.For<INotificationDispatcher>();
        dispatcher.SendToGroupAsync(Arg.Any<string>(), Arg.Any<NotificationPayload>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromException(new InvalidOperationException("hub down")));
        var email = Substitute.For<IEmailGateway>();
        email.SendAsync(Arg.Is<EmailMessage>(m => m.To == "bad@example.com"), Arg.Any<CancellationToken>()).Returns(EmailErrors.Throttled);
        email.SendAsync(Arg.Is<EmailMessage>(m => m.To == "good@example.com"), Arg.Any<CancellationToken>()).Returns(Result.Success);

        await DeliverAsync(Alert(), dispatcher, email);

        await email.Received(1).SendAsync(Arg.Is<EmailMessage>(m => m.To == "good@example.com"), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Consume_NoRecipients_StillPushesAndSendsNoEmail()
    {
        var dispatcher = Substitute.For<INotificationDispatcher>();
        var email = Substitute.For<IEmailGateway>();

        await DeliverAsync(Alert(), dispatcher, email);

        await dispatcher.Received(1).SendToGroupAsync(Arg.Any<string>(), Arg.Any<NotificationPayload>(), Arg.Any<CancellationToken>());
        await email.DidNotReceiveWithAnyArgs().SendAsync(default!, default);
    }

    /// <summary>The projected list belongs to one role: an alert for another role is pushed to that role's group and mails nobody.</summary>
    [Fact]
    public async Task Consume_AlertForAnotherRole_PushesToThatRoleOnlyAndSendsNoEmail()
    {
        await SeedRecipientsAsync("one@example.com");
        var dispatcher = Substitute.For<INotificationDispatcher>();
        var email = Substitute.For<IEmailGateway>();

        await DeliverAsync(Alert(KeycloakRoles.StoreOwner), dispatcher, email);

        await dispatcher.Received(1).SendToGroupAsync(
            NotificationGroupName.ForRole(KeycloakRoles.StoreOwner), Arg.Any<NotificationPayload>(), Arg.Any<CancellationToken>());
        await email.DidNotReceiveWithAnyArgs().SendAsync(default!, default);
    }

    [Fact]
    public async Task Consume_SameEventTwice_AlertsOnce()
    {
        await SeedRecipientsAsync("one@example.com");
        var dispatcher = Substitute.For<INotificationDispatcher>();
        var email = Substitute.For<IEmailGateway>();
        email.SendAsync(Arg.Any<EmailMessage>(), Arg.Any<CancellationToken>()).Returns(Result.Success);
        var @event = Alert();

        await DeliverAsync(@event, dispatcher, email);
        await DeliverAsync(@event, dispatcher, email);

        await email.Received(1).SendAsync(Arg.Any<EmailMessage>(), Arg.Any<CancellationToken>());
    }
}

#pragma warning restore CA1707
