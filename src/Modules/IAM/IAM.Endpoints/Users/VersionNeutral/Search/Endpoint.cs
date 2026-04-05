using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace IAM.Endpoints.Users.VersionNeutral.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("search", SearchUsersAsync)
            .WithDescription("Search users.")
            .MustHavePermission(CustomActions.Search, CustomResources.ApplicationUsers)
            .Produces<PaginationResponse<Response>>()
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> SearchUsersAsync(
        [AsParameters] Request request,
        IIAMDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Users
            .AsNoTracking()
            .TagWith(nameof(SearchUsersAsync))
            .WhereIf(
                u => EF.Property<NpgsqlTsVector>(u, "SearchVector")
                    .Matches(EF.Functions.WebSearchToTsQuery("english", request.SearchTerm!)),
                !string.IsNullOrWhiteSpace(request.SearchTerm))
            .WhereIf(u => EF.Functions.ILike(u.Name, $"%{request.Name}%"), !string.IsNullOrWhiteSpace(request.Name))
            .WhereIf(u => EF.Functions.ILike(u.LastName, $"%{request.LastName}%"), !string.IsNullOrWhiteSpace(request.LastName))
            .PaginateAsync(
                request: request,
                selector: u => new Response
                {
                    Id = u.Id,
                    Name = u.Name,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber!,
                    BirthDate = u.BirthDate,
                    CreatedBy = u.CreatedBy,
                    CreatedOn = u.CreatedOn,
                    LastModifiedBy = u.LastModifiedBy,
                    LastModifiedOn = u.LastModifiedOn
                },
                orderByDescending: u => u.CreatedOn,
                cancellationToken: cancellationToken);
    }
}
