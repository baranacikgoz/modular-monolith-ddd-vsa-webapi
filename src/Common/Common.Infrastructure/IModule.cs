using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure;

public interface IModule
{
    int RegistrationPriority { get; } // Lower value means higher priority
    IEnumerable<Assembly> GetAssemblies();
    void Register(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env);
    IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> RateLimitingPolicies();
    IEnumerable<Func<string?, IDbCommand, bool>> EfCoreInstrumentationFilters();
    void Use(WebApplication app, RouteGroupBuilder routeGroupBuilder);
}
