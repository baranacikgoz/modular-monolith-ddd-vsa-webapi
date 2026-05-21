using Common.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Infrastructure.Hubs;
using Notifications.Infrastructure.Sms;
using Notifications.Infrastructure.Telemetry;

namespace Notifications.Infrastructure;

public sealed class NotificationsModule : IModule
{
    public string Name => "Notifications";
    public int StartupPriority => 3;
    public IEnumerable<string> ActivitySourceNames => [NotificationsTelemetry.ActivitySourceName];
    public IEnumerable<string> MeterNames => [NotificationsTelemetry.MeterName];

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddNotificationServices();
        services.AddNotificationsSignalR(configuration);
    }

    public void UseModule(IApplicationBuilder app)
    {
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<NotificationsHub>("/hubs/notifications").RequireAuthorization();
    }
}
