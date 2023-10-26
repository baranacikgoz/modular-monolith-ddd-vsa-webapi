using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Captcha;

public static class GetClientKey
{
    // We skipped the mediator pattern here because:
    // We just return the constant value from the options (from appsettings.json).
    // This api will be called frequently, if the operation is so simple, why mess with a lot of code and reduce performance?
    public record Response(string ClientKey);
    internal static void MapEndpoint(RouteGroupBuilder captchaApiGroup)
    {
        captchaApiGroup
            .MapGet("client-key", GetClientKeyAsync)
            .WithDescription("Get the client key for captcha.")
            .AllowAnonymous()
            .Produces<Response>(StatusCodes.Status200OK);
    }

    private static Response GetClientKeyAsync(IOptions<CaptchaOptions> captchaOptionsProvider)
    {
        var captchaOptions = captchaOptionsProvider.Value;
        return new Response(captchaOptions.ClientKey);
    }
}
