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
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.Current;

public static class GetCurrentUser
{
    public sealed record Request() : IRequest<Result<Response>>;
    public sealed record Response(string FirstName, string LastName, string PhoneNumber);

    internal sealed class RequestHandler(ICurrentUser currentUser, UserManager<ApplicationUser> userManager) : IRequestHandler<Request, Result<Response>>
    {
        public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            var id = currentUser.Id;
            if (id == Guid.Empty)
            {
                return UserErrors.NotFound;
            }

            var user = await userManager
                            .Users
                            .Where(x => x.Id == id)
                            .Select(x => new Response(x.FirstName, x.LastName, x.PhoneNumber!))
                            .SingleOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                return UserErrors.NotFound;
            }

            return user;
        }
    }

    private static async Task<IResult> GetAsync(
        [FromServices] IMediator mediator,
        [FromServices] IResultTranslator resultTranslator,
        [FromServices] IStringLocalizer<IErrorTranslator> localizer,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync<Request, Result<Response>>(new(), cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(result, localizer);
    }

    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("", GetAsync)
            .WithDescription("Get current user.")
            .Produces<Response>(StatusCodes.Status200OK)
            .MustHavePermission(RfActions.ReadMy, RfResources.Users);
    }
}
