using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using IAM.Application.Captcha.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get;

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

    private static Result<Response> GetClientKey([FromServices] ICaptchaService captchaService)
        => Result<string>
            .Success(captchaService.GetClientKey())
            .Map(clientKey => new Response { ClientKey = clientKey });
}
