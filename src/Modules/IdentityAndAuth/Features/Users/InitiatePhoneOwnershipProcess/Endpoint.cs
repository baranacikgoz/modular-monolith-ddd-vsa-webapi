using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;
using RateLimitingConstants = IdentityAndAuth.ModuleSetup.RateLimiting.Constants;

namespace IdentityAndAuth.Features.Users.InitiatePhoneOwnershipProcess;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("initiate-phone-ownership-process", InitiatePhoneOwnershipProcessAsync)
            .RequireRateLimiting(RateLimitingConstants.Sms)
            .AllowAnonymous()
            .WithDescription("Initiate phone ownership process by sending sms otp.")
            .Produces(StatusCodes.Status200OK);
    }

    private static async Task<IResult> InitiatePhoneOwnershipProcessAsync(
        [FromBody] Request request,
        [FromServices] ISender mediator,
        [FromServices] IResultTranslator resultTranslator,
        [FromServices] IStringLocalizer<IErrorTranslator> localizer,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync<Request, Result>(request, cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(result, localizer);
    }
}
