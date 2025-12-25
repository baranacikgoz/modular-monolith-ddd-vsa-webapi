using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Endpoint = IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get.Endpoint;

namespace IAM.Endpoints.Captcha.VersionNeutral;

public static class Setup
{
    public static void MapCaptchaEndpoints(this RouteGroupBuilder rootGroup)
    {
        var otpApiGroup = rootGroup
            .MapGroup("/captcha")
            .WithTags("Captcha");

        Endpoint.MapEndpoint(otpApiGroup);
    }
}
