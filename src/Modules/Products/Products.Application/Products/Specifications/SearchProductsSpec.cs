using Ardalis.Specification;
using Common.Application.Queries.Pagination;
using Products.Application.Products.DTOs;
using Products.Application.Products.Features.Search;
using Products.Domain.Products;

namespace Products.Application.Products.Specifications;

public sealed class SearchProductsSpec : PaginationSpec<Product, ProductDto>
{
    public SearchProductsSpec(SearchProductsQuery query)
        : base(query)
        => Query
            .Select(p => new ProductDto
            {
                Id = p.Id,
                StoreId = p.StoreId,
                Name = p.Name,
                Description = p.Description,
                Quantity = p.Quantity,
                Price = p.Price,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                LastModifiedBy = p.LastModifiedBy,
                LastModifiedOn = p.LastModifiedOn
            })
            .Where(s => s.StoreId == query.StoreId, condition: query.StoreId is not null)
            .Search(s => s.Name, $"%{query.Name!}%", condition: query.Name is not null)
            .Search(s => s.Description, $"%{query.Description!}%", condition: query.Description is not null)
            .Where(s => s.Quantity >= query.MinQuantity, condition: query.MinQuantity is not null)
            .Where(s => s.Quantity <= query.MaxQuantity, condition: query.MaxQuantity is not null)
            .Where(s => s.Price >= query.MinPrice, condition: query.MinPrice is not null)
            .Where(s => s.Price <= query.MaxPrice, condition: query.MaxPrice is not null);
}
