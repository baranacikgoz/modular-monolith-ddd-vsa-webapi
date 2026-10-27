using System.Globalization;
using Common.Application.Options;
using Common.IntegrationEvents;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notifications.Infrastructure.Alerts;
using Notifications.Tests.Support;
using Xunit;

namespace Notifications.Tests.Alerts;

#pragma warning disable CA1707 // Remove the underscores from member name

/// <summary>
///     A low stock crossing becomes a critical alert for the alert role: the consumer collects a
///     <see cref="CriticalAlertRaisedIntegrationEvent" /> on the outbox, in the same transaction as its inbox row.
/// </summary>
[Collection("IntegrationTestCollection")]
public sealed class RaiseCriticalAlertOnStockLowThresholdCrossedHandlerTests(NotificationsTestFactory factory) : BaseIntegrationTest(factory)
{
    private IServiceProvider Root => Factory.Services;

    private static StockLowThresholdCrossedIntegrationEvent Crossed(int quantityOnHand = 3, int lowStockThreshold = 10) =>
        new(DefaultIdType.CreateVersion7(), quantityOnHand, lowStockThreshold);

    private Task DeliverAsync(StockLowThresholdCrossedIntegrationEvent @event) =>
        ConsumerHarness.DeliverAsync<RaiseCriticalAlertOnStockLowThresholdCrossedHandler, StockLowThresholdCrossedIntegrationEvent>(Root, @event);

    [Fact]
    public void Handler_IsRegisteredAsAConsumerInTheContainer()
    {
        var handler = Scope.ServiceProvider.GetService<RaiseCriticalAlertOnStockLowThresholdCrossedHandler>();

        Assert.NotNull(handler);
    }

    [Fact]
    public async Task Consume_ThresholdCrossed_QueuesACriticalAlertForTheAlertRoleWithTheStockFacts()
    {
        var @event = Crossed(quantityOnHand: 3, lowStockThreshold: 10);

        await DeliverAsync(@event);

        var alert = Assert.Single(await ConsumerHarness.QueuedEventsAsync<CriticalAlertRaisedIntegrationEvent>(Root));
        Assert.Equal(Root.GetRequiredService<IOptions<AlertRecipientsOptions>>().Value.RoleName, alert.RoleName);
        Assert.False(string.IsNullOrWhiteSpace(alert.Title));
        Assert.Contains(@event.ProductId.ToString(), alert.Message, StringComparison.Ordinal);
        Assert.Contains("3", alert.Message, StringComparison.Ordinal);
        Assert.Contains("10", alert.Message, StringComparison.Ordinal);
    }

    /// <summary>A consumer has no request, so the text follows the configured default culture, not the thread's.</summary>
    [Fact]
    public async Task Consume_ThreadCultureDiffersFromTheDefaultCulture_TextUsesTheDefaultCulture()
    {
        var defaultCulture = CultureInfo.GetCultureInfo(Root.GetRequiredService<IOptions<ResxLocalizationOptions>>().Value.DefaultCulture);
        var otherCulture = CultureInfo.GetCultureInfo(defaultCulture.TwoLetterISOLanguageName == "en" ? "tr" : "en");

        CultureInfo.CurrentUICulture = defaultCulture;
        await DeliverAsync(Crossed());
        var inDefaultCulture = Assert.Single(await ConsumerHarness.QueuedEventsAsync<CriticalAlertRaisedIntegrationEvent>(Root));

        CultureInfo.CurrentUICulture = otherCulture;
        await DeliverAsync(Crossed());
        var queued = await ConsumerHarness.QueuedEventsAsync<CriticalAlertRaisedIntegrationEvent>(Root);

        Assert.Equal(2, queued.Count);
        Assert.All(queued, alert => Assert.Equal(inDefaultCulture.Title, alert.Title));
    }

    [Fact]
    public async Task Consume_SameEventTwiceWithCacheEvicted_QueuesOneAlert()
    {
        var @event = Crossed();

        await DeliverAsync(@event);
        await ConsumerHarness.ForgetAsync<RaiseCriticalAlertOnStockLowThresholdCrossedHandler>(Root, @event);
        await DeliverAsync(@event);

        Assert.Single(await ConsumerHarness.QueuedEventsAsync<CriticalAlertRaisedIntegrationEvent>(Root));
    }
}

#pragma warning restore CA1707
