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
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.My.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapGet("my/search", SearchMyProductsAsync)
            .WithDescription("Search my store's products.")
            .MustHavePermission(CustomActions.SearchMy, CustomResources.Products)
            .Produces<PaginationResponse<Response>>()
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> SearchMyProductsAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
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

        Expression<Func<Product, object>>? orderByRank = hasSearchTerm
            ? p => EF.Property<NpgsqlTsVector>(p, FullTextSearchOptions.SearchVectorColumn)
                .Rank(
                    rankWeights,
                    EF.Functions.WebSearchToTsQuery(universalConfig, searchTerm)
                        .Or(EF.Functions.WebSearchToTsQuery(proseConfig, searchTerm)))
            : null;

        return await dbContext
            .Products
            .AsNoTracking()
            .TagWith(nameof(SearchMyProductsAsync))
            .Where(p => p.Store.OwnerId == currentUser.Id)
            .WhereIf(
                p => EF.Property<NpgsqlTsVector>(p, FullTextSearchOptions.SearchVectorColumn)
                         .Matches(EF.Functions.WebSearchToTsQuery(universalConfig, searchTerm))
                     || EF.Property<NpgsqlTsVector>(p, FullTextSearchOptions.SearchVectorColumn)
                         .Matches(EF.Functions.WebSearchToTsQuery(proseConfig, searchTerm)),
                hasSearchTerm)
            .WhereIf(p => EF.Functions.ILike(p.Name, $"%{request.Name}%"), !string.IsNullOrWhiteSpace(request.Name))
            .WhereIf(p => EF.Functions.ILike(p.Description, $"%{request.Description}%"),
                !string.IsNullOrWhiteSpace(request.Description))
            .WhereIf(p => p.Quantity >= request.MinQuantity!, request.MinQuantity is not null)
            .WhereIf(p => p.Quantity <= request.MaxQuantity!, request.MaxQuantity is not null)
            .WhereIf(p => p.Price >= request.MinPrice!, request.MinPrice is not null)
            .WhereIf(p => p.Price <= request.MaxPrice!, request.MaxPrice is not null)
            .PaginateAsync(
                request: request,
                selector: p => new Response
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn,
                    LastModifiedBy = p.LastModifiedBy,
                    LastModifiedOn = p.LastModifiedOn
                },
                orderByDescending: orderByRank,
                cancellationToken: cancellationToken);
    }
}
