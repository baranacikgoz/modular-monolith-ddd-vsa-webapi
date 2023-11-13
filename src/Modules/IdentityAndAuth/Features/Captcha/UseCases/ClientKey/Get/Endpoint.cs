using Common.Core.Contracts.Results;
using Common.Core.EndpointFilters;
using Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Features.Captcha.UseCases.ClientKey.Get;

internal static class Endpoint
{
    // We skipped the mediator pattern here because:
    // We just return the constant value from the options (from appsettings.json).
    // This api will be called frequently, if the operation is so simple, why mess with a lot of code and reduce performance?
    internal static void MapEndpoint(RouteGroupBuilder captchaApiGroup)
    {
        captchaApiGroup
            .MapGet("client-key", GetClientKeyAsync)
            .WithDescription("Get the client key for captcha.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .AddEndpointFilter<ResultToMinimalApiResponseFilter<Response>>();
    }

    private static Result<Response> GetClientKeyAsync(IOptions<CaptchaOptions> captchaOptionsProvider)
    {
        var captchaOptions = captchaOptionsProvider.Value;
        return new Response(captchaOptions.ClientKey);
    }
}
