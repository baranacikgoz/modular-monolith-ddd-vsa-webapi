using System.Data;
using System.Reflection;
using Common.Infrastructure;
using Common.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Infrastructure.Sms;

namespace Notifications.Infrastructure;

public class NotificationsModule : IModule
{
    public int RegistrationPriority => 2;

    public IEnumerable<Assembly> GetAssemblies()
    {
        yield return typeof(IAssemblyReference).Assembly;
    }

    public void Register(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        => services.AddNotificationServices();

    public IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> RateLimitingPolicies() => [];

    public IEnumerable<Func<string?, IDbCommand, bool>> EfCoreInstrumentationFilters() => [];

    public void Use(WebApplication app, RouteGroupBuilder routeGroupBuilder)
    {
        // Will be filled when module reaches maturity

    }
}
