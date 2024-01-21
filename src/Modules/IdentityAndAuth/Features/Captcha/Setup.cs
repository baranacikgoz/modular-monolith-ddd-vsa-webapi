using IdentityAndAuth.Features.Captcha.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Captcha;

internal static class Setup
{
    public static IServiceCollection AddCaptchaFeature(this IServiceCollection services)
        => services
            .AddCaptchaInfrastructure();
    public static void MapCaptchaEndpoints(this RouteGroupBuilder rootGroup)
    {
        var captchaApiGroup = rootGroup
            .MapGroup("/captcha")
            .WithTags("Captcha");

        UseCases.ClientKey.Get.Endpoint.MapEndpoint(captchaApiGroup);
    }
}
