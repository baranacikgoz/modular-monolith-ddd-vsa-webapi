using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace IAM.Endpoints.Users.VersionNeutral.CheckRegistration;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("check-registration", IsRegisteredAsync)
            .WithDescription("Check if a user is registered by given phone number.")
            .AllowAnonymous()
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> IsRegisteredAsync(
        [AsParameters] Request request,
        IIAMDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Users
            .AsNoTracking()
            .TagWith(nameof(IsRegisteredAsync))
            .Where(u => u.PhoneNumber == request.PhoneNumber)
            .AnyAsResultAsync(cancellationToken)
            .MapAsync(any => new Response { IsRegistered = any });
    }
}
