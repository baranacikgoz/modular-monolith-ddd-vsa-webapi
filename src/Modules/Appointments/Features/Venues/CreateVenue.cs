using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace Appointments.Features.Venues;

public static class CreateVenue
{
    public sealed record Request(string Name) : IRequest<Result<Guid>>;

    internal sealed class RequestHandler : IRequestHandler<Request, Result<Guid>>
    {
        public ValueTask<Result<Guid>> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    private static async Task<IResult> CreateAsync(
        [FromBody] Request request,
        [FromServices] IMediator mediator,
        [FromServices] IResultTranslator resultTranslator,
        [FromServices] IStringLocalizer<IErrorTranslator> localizer,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync<Request, Result<Guid>>(request, cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(result, localizer);
    }

    internal static void MapEndpoint(RouteGroupBuilder venuesApiGroup)
    {
        venuesApiGroup
            .MapPost("", CreateAsync)
            .MustHavePermission(RfActions.Create, RfResources.Venues)
            .WithDescription("Create a new venue.");
    }
}
