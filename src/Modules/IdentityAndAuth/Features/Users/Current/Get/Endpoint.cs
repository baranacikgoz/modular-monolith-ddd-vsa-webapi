using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.Current.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("", GetAsync)
            .WithDescription("Get current user.")
            .Produces<Response>(StatusCodes.Status200OK)
            .MustHavePermission(RfActions.ReadMy, RfResources.Users);
    }

    private static async Task<IResult> GetAsync(
        [FromServices] ISender mediator,
        [FromServices] IResultTranslator resultTranslator,
        [FromServices] IStringLocalizer<IErrorTranslator> localizer,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync<Request, Result<Response>>(new(), cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(result, localizer);
    }
}
