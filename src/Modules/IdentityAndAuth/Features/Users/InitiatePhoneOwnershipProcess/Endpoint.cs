using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.InitiatePhoneOwnershipProcess;

public static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("initiate-phone-ownership-process", InitiatePhoneOwnershipProcessAsync)
            .WithDescription("Initiate phone ownership process by sending sms otp.")
            .Produces(StatusCodes.Status200OK)
            .AllowAnonymous();
    }
    private static async Task<IResult> InitiatePhoneOwnershipProcessAsync(
        [FromBody] Request request,
        [FromServices] IMediator mediator,
        [FromServices] IResultTranslator resultTranslator,
        [FromServices] IStringLocalizer<IErrorTranslator> localizer,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync<Request, Result>(request, cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(result, localizer);
    }
}
