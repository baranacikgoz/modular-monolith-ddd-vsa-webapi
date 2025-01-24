using Ardalis.Specification;
using Products.Domain.Stores;
using Products.Application.Stores.DTOs;
using Products.Application.Stores.Features.Search;
using Common.Application.Queries.Pagination;

namespace Products.Application.Stores.Specifications;

public sealed class SearchStoresSpec : PaginationSpec<Store, StoreDto>
{
    public SearchStoresSpec(SearchStoresQuery query)
        : base(query)
        => Query
            .Select(p => new StoreDto
            {
                Id = p.Id,
                OwnerId = p.OwnerId,
                Name = p.Name,
                Description = p.Description,
                Address = p.Address,
                ProductCount = p.Products.Count,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                LastModifiedBy = p.LastModifiedBy,
                LastModifiedOn = p.LastModifiedOn
            })
            .Search(s => s.Name, $"%{query.Name!}%", condition: query.Name is not null)
            .Search(s => s.Description, $"%{query.Description!}%", condition: query.Description is not null)
            .Search(s => s.Address, $"%{query.Address!}%", condition: query.Address is not null);
}
