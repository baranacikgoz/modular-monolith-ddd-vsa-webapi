using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.ProvePhoneOwnership;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("prove-phone-ownership", ProvePhoneOwnershipAsync)
            .WithDescription("Prove phone ownership by validating otp.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous();
    }
    private static async Task<IResult> ProvePhoneOwnershipAsync(
        [FromBody] Request request,
        [FromServices] ISender mediator,
        [FromServices] IResultTranslator resultTranslator,
        [FromServices] IStringLocalizer<IErrorTranslator> localizer,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync<Request, Result<Response>>(request, cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(result, localizer);
    }
}
