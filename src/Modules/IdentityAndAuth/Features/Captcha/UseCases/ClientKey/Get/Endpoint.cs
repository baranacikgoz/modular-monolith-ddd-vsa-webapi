using Common.Core.Contracts.Results;
using Common.Core.Extensions;
using Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Features.Captcha.UseCases.ClientKey.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder captchaApiGroup)
    {
        captchaApiGroup
            .MapGet("client-key", GetClientKeyAsync)
            .WithDescription("Get the client key for captcha.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }

    private static Result<Response> GetClientKeyAsync(IOptions<CaptchaOptions> captchaOptionsProvider)
        => Result<CaptchaOptions>
            .Success(captchaOptionsProvider.Value)
            .Map(captchaOptions => new Response(captchaOptions.ClientKey));
}
