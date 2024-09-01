using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace IAM.Application.Identity.VersionNeutral.Users.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("{id}", GetAsync)
            .WithDescription("Get a user by id.")
            .Produces<Response>(StatusCodes.Status200OK)
            .MustHavePermission(CustomActions.Read, CustomResources.ApplicationUsers)
            .TransformResultTo<Response>();
    }
    private static async Task<Result<Response>> GetAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<ApplicationUserId>>] ApplicationUserId id,
        [FromServices] UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken)
        => await Result<Dto>
            .CreateAsync(taskToAwaitValue: async () => await userManager
                                                        .Users
                                                        .Where(x => x.Id == id)
                                                        .Select(x => new Dto(x.Id, x.Name, x.LastName, x.PhoneNumber!, x.NationalIdentityNumber, x.BirthDate))
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
        ApplicationUserId Id,
        string Name,
        string LastName,
        string PhoneNumber,
        string NationalIdentityNumber,
        DateOnly BirthDate
    );
}
