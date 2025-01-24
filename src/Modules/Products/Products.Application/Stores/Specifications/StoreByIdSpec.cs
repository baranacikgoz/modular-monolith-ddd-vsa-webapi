using System.Linq.Expressions;
using Ardalis.Specification;
using Products.Domain.Stores;

namespace Products.Application.Stores.Specifications;

public sealed class StoreByIdSpec : SingleResultSpecification<Store>
{
    public StoreByIdSpec(StoreId id)
        => Query
            .Where(x => x.Id == id);
}

public sealed class StoreByIdSpec<TDto> : SingleResultSpecification<Store, TDto>
{
    public StoreByIdSpec(StoreId id, Expression<Func<Store, TDto>> selector)
        => Query
            .Select(selector)
            .Where(x => x.Id == id);
}
