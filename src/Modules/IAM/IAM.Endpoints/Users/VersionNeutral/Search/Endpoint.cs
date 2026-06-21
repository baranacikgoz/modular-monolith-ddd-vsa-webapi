using System.Linq.Expressions;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Application.Pagination;
using Common.Application.Search;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        ISearchLanguageResolver searchLanguageResolver,
        IOptions<FullTextSearchOptions> fullTextSearchOptions,
        CancellationToken cancellationToken)
    {
        var hasSearchTerm = !string.IsNullOrWhiteSpace(request.SearchTerm);
        var searchTerm = request.SearchTerm ?? string.Empty;
        var universalConfig = searchLanguageResolver.UniversalConfig;
        var rankWeights = fullTextSearchOptions.Value.RankWeights.ToArray();

        // Universal layer only — FullName is a proper noun, single language-neutral tsquery.
        Expression<Func<ApplicationUser, object>>? orderByRank = hasSearchTerm
            ? u => EF.Property<NpgsqlTsVector>(u, FullTextSearchOptions.SearchVectorColumn)
                .Rank(rankWeights, EF.Functions.WebSearchToTsQuery(universalConfig, searchTerm))
            : null;

        return await dbContext
            .Users
            .AsNoTracking()
            .TagWith(nameof(SearchUsersAsync))
            .WhereIf(
                u => EF.Property<NpgsqlTsVector>(u, FullTextSearchOptions.SearchVectorColumn)
                    .Matches(EF.Functions.WebSearchToTsQuery(universalConfig, searchTerm)),
                hasSearchTerm)
            .WhereIf(u => EF.Functions.ILike(u.FullName, $"%{request.FullName}%"), !string.IsNullOrWhiteSpace(request.FullName))
            .PaginateAsync(
                request: request,
                selector: u => new Response
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    PhoneNumber = u.PhoneNumber!,
                    BirthDate = u.BirthDate,
                    CreatedBy = u.CreatedBy,
                    CreatedOn = u.CreatedOn,
                    LastModifiedBy = u.LastModifiedBy,
                    LastModifiedOn = u.LastModifiedOn
                },
                orderByDescending: orderByRank,
                cancellationToken: cancellationToken);
    }
}
