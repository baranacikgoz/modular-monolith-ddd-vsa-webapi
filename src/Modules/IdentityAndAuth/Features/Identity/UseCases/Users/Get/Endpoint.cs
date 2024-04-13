using Common.Core.Auth;
using Common.Core.Contracts.Results;
using Common.Core.Extensions;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("{id}", GetAsync)
            .WithDescription("Get a user by id.")
            .Produces<Response>(StatusCodes.Status200OK)
            .MustHavePermission(CustomActions.Read, CustomResources.Users)
            .TransformResultTo<Response>();
    }
    private static async Task<Result<Response>> GetAsync(
        [FromRoute] Guid id,
        [FromServices] UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
        => await Result<Dto>
            .CreateAsync(taskToAwaitValue: async () => await userManager
                                                        .Users
                                                        .Where(x => x.Id.Value == id)
                                                        .Select(x => new Dto(x.Id.Value, x.Name, x.LastName, x.PhoneNumber!, x.NationalIdentityNumber, x.BirthDate))
                                                        .SingleOrDefaultAsync(cancellationToken),
                    errorIfValueNull: Error.NotFound(nameof(ApplicationUser), id))
            .MapAsync(dto => new Response(
                                    dto.Id,
                                    dto.Name,
                                    dto.LastName,
                                    dto.PhoneNumber,
                                    dto.NationalIdentityNumber,
                                    dto.BirthDate));

    private sealed record Dto(
        Guid Id,
        string Name,
        string LastName,
        string PhoneNumber,
        string NationalIdentityNumber,
        DateOnly BirthDate
    );
}
