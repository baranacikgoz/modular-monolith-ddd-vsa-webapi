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
using Products.Domain.ProductTemplates;

namespace Products.Endpoints.ProductTemplates.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productTemplatesApiGroup)
    {
        productTemplatesApiGroup
            .MapGet("search", SearchProductTemplatesAsync)
            .WithDescription("Search product templates.")
            .MustHavePermission(CustomActions.Search, CustomResources.ProductTemplates)
            .Produces<PaginationResponse<Response>>()
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> SearchProductTemplatesAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        ISearchLanguageResolver searchLanguageResolver,
        IOptions<FullTextSearchOptions> fullTextSearchOptions,
        CancellationToken cancellationToken)
    {
        var hasSearchTerm = !string.IsNullOrWhiteSpace(request.SearchTerm);
        var searchTerm = request.SearchTerm ?? string.Empty;
        var universalConfig = searchLanguageResolver.UniversalConfig;
        var rankWeights = fullTextSearchOptions.Value.RankWeights.ToArray();

        // Universal layer only — proper-noun fields, single language-neutral tsquery.
        Expression<Func<ProductTemplate, object>>? orderByRank = hasSearchTerm
            ? p => EF.Property<NpgsqlTsVector>(p, FullTextSearchOptions.SearchVectorColumn)
                .Rank(rankWeights, EF.Functions.WebSearchToTsQuery(universalConfig, searchTerm))
            : null;

        return await dbContext
            .ProductTemplates
            .AsNoTracking()
            .TagWith(nameof(SearchProductTemplatesAsync))
            .WhereIf(
                p => EF.Property<NpgsqlTsVector>(p, FullTextSearchOptions.SearchVectorColumn)
                    .Matches(EF.Functions.WebSearchToTsQuery(universalConfig, searchTerm)),
                hasSearchTerm)
            .WhereIf(p => EF.Functions.ILike(p.Brand, $"%{request.Brand}%"), !string.IsNullOrWhiteSpace(request.Brand))
            .WhereIf(p => EF.Functions.ILike(p.Model, $"%{request.Model}%"), !string.IsNullOrWhiteSpace(request.Model))
            .WhereIf(p => EF.Functions.ILike(p.Color, $"%{request.Color}%"), !string.IsNullOrWhiteSpace(request.Color))
            .PaginateAsync(
                request: request,
                selector: p => new Response
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Model = p.Model,
                    Color = p.Color,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn,
                    LastModifiedBy = p.LastModifiedBy,
                    LastModifiedOn = p.LastModifiedOn
                },
                orderByDescending: orderByRank,
                cancellationToken: cancellationToken);
    }
}
