using System.Linq.Expressions;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Application.Pagination;
using Common.Application.Search;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NpgsqlTypes;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapGet("search", SearchStoresAsync)
            .WithDescription("Search stores.")
            .MustHavePermission(CustomActions.Search, CustomResources.Stores)
            .Produces<PaginationResponse<Response>>()
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> SearchStoresAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        ISearchLanguageResolver searchLanguageResolver,
        IOptions<FullTextSearchOptions> fullTextSearchOptions,
        CancellationToken cancellationToken)
    {
        var hasSearchTerm = !string.IsNullOrWhiteSpace(request.SearchTerm);
        var searchTerm = request.SearchTerm ?? string.Empty;
        var universalConfig = searchLanguageResolver.UniversalConfig;
        var proseConfig = searchLanguageResolver.ResolveConfig();
        var rankWeights = fullTextSearchOptions.Value.RankWeights.ToArray();

        Expression<Func<Store, object>>? orderByRank = hasSearchTerm
            ? s => EF.Property<NpgsqlTsVector>(s, FullTextSearchOptions.SearchVectorColumn)
                .Rank(
                    rankWeights,
                    EF.Functions.WebSearchToTsQuery(universalConfig, searchTerm)
                        .Or(EF.Functions.WebSearchToTsQuery(proseConfig, searchTerm)))
            : null;

        return await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(SearchStoresAsync))
            .WhereIf(
                s => EF.Property<NpgsqlTsVector>(s, FullTextSearchOptions.SearchVectorColumn)
                         .Matches(EF.Functions.WebSearchToTsQuery(universalConfig, searchTerm))
                     || EF.Property<NpgsqlTsVector>(s, FullTextSearchOptions.SearchVectorColumn)
                         .Matches(EF.Functions.WebSearchToTsQuery(proseConfig, searchTerm)),
                hasSearchTerm)
            .WhereIf(s => EF.Functions.ILike(s.Name, $"%{request.Name}%"), !string.IsNullOrWhiteSpace(request.Name))
            .WhereIf(s => EF.Functions.ILike(s.Description, $"%{request.Description}%"),
                !string.IsNullOrWhiteSpace(request.Description))
            .WhereIf(s => EF.Functions.ILike(s.Address, $"%{request.Address}%"),
                !string.IsNullOrWhiteSpace(request.Address))
            .PaginateAsync(
                request: request,
                selector: s => new Response
                {
                    Id = s.Id,
                    OwnerId = s.OwnerId,
                    Name = s.Name,
                    Description = s.Description,
                    Address = s.Address,
                    ProductCount = s.Products.Count,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn,
                    LastModifiedBy = s.LastModifiedBy,
                    LastModifiedOn = s.LastModifiedOn
                },
                orderByDescending: orderByRank,
                cancellationToken: cancellationToken);
    }
}
