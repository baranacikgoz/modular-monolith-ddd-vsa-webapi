using IAM.Endpoints.Captcha.VersionNeutral;
using IAM.Endpoints.Otp.VersionNeutral;
using IAM.Endpoints.Tokens.VersionNeutral;
using IAM.Endpoints.Users.VersionNeutral;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints;

public static class Setup
{
    public static WebApplication MapIAMModuleEndpoints(this WebApplication app, RouteGroupBuilder versionNeutralApiGroup)
    {
        versionNeutralApiGroup.MapUsersEndpoints();
        versionNeutralApiGroup.MapTokensEndpoints();
        versionNeutralApiGroup.MapOtpEndpoints();
        versionNeutralApiGroup.MapCaptchaEndpoints();

        return app;
    }
}
