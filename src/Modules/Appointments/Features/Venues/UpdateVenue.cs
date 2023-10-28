using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NimbleMediator.Contracts;
using Common.Core.Auth;
using Microsoft.Extensions.Localization;

namespace Appointments.Features.Venues;

public static class UpdateVenue
{
    public sealed record Request(Guid Id, string Name) : IRequest<Result>;

    internal sealed class RequestHandler : IRequestHandler<Request, Result>
    {
        public ValueTask<Result> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    private static async Task<IResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] Request request,
        [FromServices] IMediator mediator,
        [FromServices] IResultTranslator resultTranslator,
        [FromServices] IStringLocalizer<IErrorTranslator> localizer,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return Results.BadRequest();
        }

        var result = await mediator.SendAsync<Request, Result>(request, cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(result, localizer);
    }

    internal static void MapEndpoint(RouteGroupBuilder venuesApiGroup)
    {
        venuesApiGroup
            .MapPut("{id}", UpdateAsync)
            .MustHavePermission(RfActions.Update, RfResources.Venues)
            .WithDescription("Update a venue.");
    }
}
