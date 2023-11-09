using IdentityAndAuth.Features.Captcha.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Captcha;

internal static class Setup
{
    public static IServiceCollection AddCaptchaFeatures(this IServiceCollection services)
        => services
            .AddCaptchaServices();
    public static RouteGroupBuilder MapCaptchaEndpoints(this RouteGroupBuilder rootGroup)
    {
        var captchaApiGroup = rootGroup
            .MapGroup("/captcha")
            .WithTags("Captcha");

        ClientKey.Get.Endpoint.MapEndpoint(captchaApiGroup);

        return rootGroup;
    }
}
