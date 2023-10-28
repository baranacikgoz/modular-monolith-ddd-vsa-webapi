using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using IdentityAndAuth;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Domain.Errors;
using IdentityAndAuth.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users;

public static class GetUser
{
    internal sealed record Request(Guid Id) : IRequest<Result<Response>>;
    internal sealed record Response(Guid Id, string FirstName, string LastName, string PhoneNumber, string NationalIdentityNumber, DateOnly BirthDate);

    internal sealed class RequestHandler(
        UserManager<ApplicationUser> userManager
    )
        : IRequestHandler<Request, Result<Response>>
    {
        public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            var response = await userManager
                            .Users
                            .Where(x => x.Id == request.Id)
                            .Select(x => new Response(x.Id, x.FirstName, x.LastName, x.PhoneNumber!, x.NationalIdentityNumber, x.BirthDate))
                            .SingleOrDefaultAsync(cancellationToken);

            if (response is null)
            {
                return UserErrors.NotFound;
            }

            return response;
        }
    }

    private static async Task<IResult> GetAsync(
        [FromRoute] Guid id,
        [FromServices] IMediator mediator,
        [FromKeyedServices(ModuleConstants.ModuleName)] IResultTranslator resultTranslator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync<Request, Result<Response>>(new(id), cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(result);
    }

    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("{id}", GetAsync)
            .WithDescription("Get a user by id.")
            .Produces<Response>(StatusCodes.Status200OK)
            .MustHavePermission(RfActions.Read, RfResources.Users);
    }
}
