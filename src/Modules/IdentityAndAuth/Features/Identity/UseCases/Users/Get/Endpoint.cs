using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.EndpointFilters;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Identity.Domain.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("{id}", GetAsync)
            .WithDescription("Get a user by id.")
            .Produces<Response>(StatusCodes.Status200OK)
            .MustHavePermission(RfActions.Read, RfResources.Users)
            .TransformResultTo<Response>();
    }
    private static async Task<Result<Response>> GetAsync(
        [FromRoute] Guid id,
        [FromServices] UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
        => await Result<Dto>
            .Create(taskToAwaitValue: async () => await userManager
                                                        .Users
                                                        .Where(x => x.Id == id)
                                                        .Select(x => new Dto(x.Id, x.FirstName, x.LastName, x.PhoneNumber!, x.NationalIdentityNumber, x.BirthDate))
                                                        .SingleOrDefaultAsync(cancellationToken),
                    ifTaskReturnsNull: UserErrors.UserNotFound)
            .MapAsync(dto => new Response(
                                    dto.Id,
                                    dto.FirstName,
                                    dto.LastName,
                                    dto.PhoneNumber,
                                    dto.NationalIdentityNumber,
                                    dto.BirthDate));

    private sealed record Dto(
        Guid Id,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string NationalIdentityNumber,
        DateOnly BirthDate
    );
}
