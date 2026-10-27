using System.Globalization;
using Common.Application.EventBus;
using Common.Application.Localization.Resources;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notifications.Application.Persistence;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.Alerts;

/// <summary>
///     Turns a low stock crossing into a critical alert for <see cref="AlertRecipientsOptions.RoleName" />. The alert is
///     collected on the outbox, so it is committed with the inbox row and fanned out by
///     <see cref="NotifyRoleOnCriticalAlertRaisedHandler" />. The recovery event has no consumer here. A consumer has no
///     request, so the text is written in <see cref="ResxLocalizationOptions.DefaultCulture" />.
/// </summary>
public sealed class RaiseCriticalAlertOnStockLowThresholdCrossedHandler(
    IIntegrationEventOutbox outbox,
    IResxLocalizer localizer,
    IOptions<AlertRecipientsOptions> alertOptions,
    IOptions<ResxLocalizationOptions> localizationOptions,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<INotificationsDbContext> inbox,
    ILogger<RaiseCriticalAlertOnStockLowThresholdCrossedHandler> logger
) : IntegrationEventHandlerBase<StockLowThresholdCrossedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override Task ProcessAsync(StockLowThresholdCrossedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        var culture = CultureInfo.GetCultureInfo(localizationOptions.Value.DefaultCulture);
        CultureInfo.CurrentUICulture = culture;

        var message = string.Format(
            culture, localizer.Notifications_LowStockAlert_Message, @event.ProductId, @event.QuantityOnHand, @event.LowStockThreshold);
        outbox.Collect(new CriticalAlertRaisedIntegrationEvent(alertOptions.Value.RoleName, localizer.Notifications_LowStockAlert_Title, message));

        return Task.CompletedTask;
    }
}
