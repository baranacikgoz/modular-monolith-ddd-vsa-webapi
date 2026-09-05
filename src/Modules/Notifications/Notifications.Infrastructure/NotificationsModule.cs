using Common.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Infrastructure.Devices;
using Notifications.Infrastructure.Hubs;
using Notifications.Infrastructure.Otp;
using Notifications.Infrastructure.Persistence;
using Notifications.Infrastructure.Push;
using Notifications.Infrastructure.Sms;
using Notifications.Infrastructure.Telemetry;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace Notifications.Infrastructure;

public sealed class NotificationsModule : IModule
{
    public string Name => "Notifications";
    public int StartupPriority => 3;
    public IEnumerable<string> ActivitySourceNames => [NotificationsTelemetry.ActivitySourceName];
    public IEnumerable<string> MeterNames => [NotificationsTelemetry.MeterName];

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence();
        services.AddNotificationServices(configuration);
        services.AddNotificationsSignalR(configuration);
        services.AddOtpServices(configuration);
        services.AddPushServices(configuration);
    }

    public void UseModule(IApplicationBuilder app)
    {
        app.UsePersistence();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<NotificationsHub>("/hubs/notifications").RequireAuthorization();

        var notificationsApiGroup = endpoints
            .MapGroup("/notifications")
            .AddFluentValidationAutoValidation()
            .RequireAuthorization();

        notificationsApiGroup.MapDevicesEndpoints();
    }
}
