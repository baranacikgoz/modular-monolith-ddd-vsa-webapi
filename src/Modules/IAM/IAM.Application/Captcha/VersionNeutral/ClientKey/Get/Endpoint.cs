using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Mvc;
using IAM.Application.Captcha.Services;

namespace IAM.Application.Captcha.VersionNeutral.ClientKey.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder captchaApiGroup)
    {
        captchaApiGroup
            .MapGet("client-key", GetClientKey)
            .WithDescription("Get the client key for captcha.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }

    private static Result<Response> GetClientKey(
        [FromServices] ICaptchaService captchaService)
        => Result<string>
            .Success(captchaService.GetClientKey())
            .Map(clientKey => new Response { ClientKey = clientKey });
}
