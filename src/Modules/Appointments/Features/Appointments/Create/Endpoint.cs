using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace Appointments.Features.Appointments.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder venuesApiGroup)
    {
        venuesApiGroup
            .MapPost("", CreateAsync)
            .MustHavePermission(RfActions.Create, RfResources.Appointments)
            .WithDescription("Create an appointment.");
    }
    private static async Task<IResult> CreateAsync(
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
