using Common.Application.Options;
using Common.Infrastructure.Modules;
using IAM.Endpoints.Captcha.VersionNeutral;
using IAM.Endpoints.Otp.VersionNeutral;
using IAM.Endpoints.Tokens.VersionNeutral;
using IAM.Endpoints.Users.VersionNeutral;
using IAM.Infrastructure.Auth;
using IAM.Infrastructure.Captcha;
using IAM.Infrastructure.Keycloak;
using IAM.Infrastructure.RateLimiting;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace IAM.Endpoints;

/// <summary>
///     Identity broker in front of Keycloak: verifies one-time codes, proxies logins and refreshes, exposes
///     user/session queries through the Admin REST API and wires token validation + permission decisions.
///     It owns no database.
/// </summary>
public sealed class IamModule : IModule
{
    public string Name => "IAM";
    public int StartupPriority => 2;

    public IEnumerable<string> ActivitySourceNames => [IamTelemetry.ActivitySourceName];

    public IEnumerable<string> MeterNames => [IamTelemetry.MeterName];

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddKeycloakInfrastructure()
            .AddAuthInfrastructure()
            .AddCaptchaInfrastructure(configuration);
    }

    public void UseModule(IApplicationBuilder app)
    {
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
