using Common.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Infrastructure.Sms;

namespace Notifications.Infrastructure;

public sealed class NotificationsModule : IModule
{
    public string Name => "Notifications";
    public int StartupPriority => 3;
    public IEnumerable<string> ActivitySourceNames => [];
    public IEnumerable<string> MeterNames => [];

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddNotificationServices();
    }

    public void UseModule(IApplicationBuilder app)
    {
        // Will be filled when module reaches maturity
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
    }
}
