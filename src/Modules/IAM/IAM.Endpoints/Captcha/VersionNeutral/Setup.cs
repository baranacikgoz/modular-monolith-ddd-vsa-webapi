using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Captcha.VersionNeutral;

public static class Setup
{
    public static void MapCaptchaEndpoints(this RouteGroupBuilder rootGroup)
    {
        var otpApiGroup = rootGroup
            .MapGroup("/captcha")
            .WithTags("Captcha");

        ClientKey.Get.Endpoint.MapEndpoint(otpApiGroup);
    }
}
