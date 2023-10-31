using Common.Caching;
using Common.Options;
using IdentityAndAuth.Features.Captcha.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Features.Captcha;

public static class Setup
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
