using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using IAM.Application.Keycloak;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Users.VersionNeutral.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("search", SearchUsersAsync)
            .WithDescription("Search users (substring over username, first name, last name and email).")
            .RequireScope(KeycloakScopes.Users.Search)
            .Produces<PaginationResponse<Response>>()
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> SearchUsersAsync(
        [AsParameters] Request request,
        IKeycloakAdminClient adminClient,
        CancellationToken cancellationToken)
    {
        var page = await adminClient.SearchUsersAsync(request.SearchTerm, request.Skip, request.Take, cancellationToken);

        var data = page.Users
            .Select(user => new Response
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                Enabled = user.Enabled,
                CreatedOn = user.CreatedOn
            })
            .ToList();

        return new PaginationResponse<Response>(data, page.TotalCount, request.PageNumber, request.PageSize);
    }
}
