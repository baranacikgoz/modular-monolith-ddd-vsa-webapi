using Common.Endpoints.Versioning;
using Common.Infrastructure.Modules;
using IAM.Endpoints.Captcha.VersionNeutral;
using IAM.Endpoints.Otp.VersionNeutral;
using IAM.Endpoints.Tokens.VersionNeutral;
using IAM.Endpoints.Users.VersionNeutral;
using IAM.Infrastructure.Auth;
using IAM.Infrastructure.Captcha;
using IAM.Infrastructure.Identity;
using IAM.Infrastructure.Persistence;
using IAM.Infrastructure.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace IAM.Endpoints;

public sealed class IamModule : IModule
{
    public string Name => "IAM";

    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence()
            .AddIdentityInfrastructure()
            .AddAuthInfrastructure(configuration)
            .AddCaptchaInfrastructure()
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

    public IEnumerable<Action<global::Microsoft.AspNetCore.RateLimiting.RateLimiterOptions, global::Common.Application.Options.CustomRateLimitingOptions>>? RateLimitingPolicies => global::IAM.Infrastructure.RateLimiting.Policies.Get();
}
