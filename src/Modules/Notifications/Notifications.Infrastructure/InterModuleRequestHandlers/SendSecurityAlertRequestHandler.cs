using System.Globalization;
using Common.Application.Options;
using Common.Infrastructure.Persistence.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Notifications.Application.Hubs;
using Notifications.Application.Persistence;
using Notifications.Application.Push;

namespace Notifications.Infrastructure.InterModuleRequestHandlers;

public class SendSecurityAlertRequestHandler(
    INotificationsDbContext dbContext,
    INotificationDispatcher dispatcher,
    IPushGateway pushGateway,
    IOptions<PushOptions> pushOptionsProvider,
    IOptions<ResxLocalizationOptions> localizationOptionsProvider
) : InterModuleRequestHandler<SendSecurityAlertRequest, SendSecurityAlertResponse>
{
    public override async Task<SendSecurityAlertResponse> HandleAsync(
        SendSecurityAlertRequest request, CancellationToken cancellationToken)
    {
        await dispatcher.SendToUserAsync(
            request.UserId,
            new NotificationPayload($"security.{request.Type}"),
            cancellationToken);

        var pushTokens = await dbContext.DeviceRegistrations
            .AsNoTracking()
            .TagWith(nameof(SendSecurityAlertRequestHandler), request.UserId)
            .Where(r => r.UserId == request.UserId && r.IsActive && r.PushToken != null)
#pragma warning disable S8969 // EF translates this to SQL; the null-forgiving operator is not redundant here,
                              // the preceding Where clause runs in a separate expression tree the compiler cannot see through.
            .Select(r => r.PushToken!)
#pragma warning restore S8969
            .ToListAsync(cancellationToken);

        if (pushTokens.Count > 0)
        {
            var templates = pushOptionsProvider.Value.Templates.SecurityAlert;
            // The application's configured default, not RequestLocalizationOptions: nothing configures that
            // DI-registered instance, so its default culture is the process culture (invariant in a container).
            var language = new CultureInfo(localizationOptionsProvider.Value.DefaultCulture).TwoLetterISOLanguageName;
            if (!templates.TryGetValue(language, out var template))
            {
                template = templates.Values.First();
            }

            await pushGateway.SendAsync(
                new PushMessage(pushTokens, template.Title, template.Body),
                cancellationToken);
        }

        return new SendSecurityAlertResponse();
    }
}
