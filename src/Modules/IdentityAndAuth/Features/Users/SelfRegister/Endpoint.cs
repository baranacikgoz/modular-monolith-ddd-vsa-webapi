using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.SelfRegister;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("self-register", SelfRegisterAsync)
            .AllowAnonymous()
            .WithDescription("Self register a new user.")
            .Produces<Response>();
    }

    private static async Task<IResult> SelfRegisterAsync(
        [FromBody] Request request,
        [FromServices] ISender mediator,
        [FromServices] IResultTranslator resultTranslator,
        [FromServices] IStringLocalizer<IErrorTranslator> localizer,
        CancellationToken cancellationToken)
    {
        var response = await mediator.SendAsync<Request, Result<Response>>(request, cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(response, localizer);
    }
}
