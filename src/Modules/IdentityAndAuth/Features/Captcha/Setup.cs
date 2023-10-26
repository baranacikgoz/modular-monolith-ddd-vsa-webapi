using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IdentityAndAuth.Features.Captcha;

public static class Setup
{
    public static RouteGroupBuilder MapCaptchaEndpoints(this RouteGroupBuilder rootGroup)
    {
        var captchaApiGroup = rootGroup
            .MapGroup("/captcha")
            .WithTags("Captcha");

        GetClientKey.MapEndpoint(captchaApiGroup);

        return rootGroup;
    }
}
