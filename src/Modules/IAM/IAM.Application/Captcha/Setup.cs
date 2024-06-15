using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Application.Captcha;

public static class Setup
{
    public static void MapCaptchaEndpoints(this RouteGroupBuilder rootGroup)
    {
        var captchaApiGroup = rootGroup
            .MapGroup("/captcha")
            .WithTags("Captcha");

        VersionNeutral.ClientKey.Get.Endpoint.MapEndpoint(captchaApiGroup);
    }
}
