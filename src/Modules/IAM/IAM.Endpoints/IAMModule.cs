using Common.Application.Options;
using Common.Infrastructure.Modules;
using IAM.Endpoints.Captcha.VersionNeutral;
using IAM.Endpoints.Otp.VersionNeutral;
using IAM.Endpoints.Tokens.VersionNeutral;
using IAM.Endpoints.Users.VersionNeutral;
using IAM.Infrastructure.Auth;
using IAM.Infrastructure.Captcha;
using IAM.Infrastructure.Identity;
using IAM.Infrastructure.Persistence;
using IAM.Infrastructure.RateLimiting;
using IAM.Infrastructure.Telemetry;
using IAM.Infrastructure.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace IAM.Endpoints;

public sealed class IamModule : IModule
{
    public string Name => "IAM";
    public int StartupPriority => 2;

    public IEnumerable<string> ActivitySourceNames => [IamTelemetry.ActivitySourceName];

    public IEnumerable<string> MeterNames => [IamTelemetry.MeterName];

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence()
            .AddIdentityInfrastructure()
            .AddAuthInfrastructure(configuration)
            .AddCaptchaInfrastructure(configuration)
            .AddTokensInfrastructure();
    }

    public void UseModule(IApplicationBuilder app)
    {
        app.UsePersistence();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var versionNeutralApiGroup = endpoints
            .MapGroup("/")
            .AddFluentValidationAutoValidation()
            .RequireAuthorization();

        versionNeutralApiGroup.MapUsersEndpoints();
        versionNeutralApiGroup.MapTokensEndpoints();
        versionNeutralApiGroup.MapOtpEndpoints();
        versionNeutralApiGroup.MapCaptchaEndpoints();
    }

    public IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>>? RateLimitingPolicies => Policies.Get();
}
