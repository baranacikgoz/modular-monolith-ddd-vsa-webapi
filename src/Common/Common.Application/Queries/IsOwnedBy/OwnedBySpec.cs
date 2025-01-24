using System.Linq.Expressions;
using System.Security.Cryptography;
using Ardalis.Specification;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.Queries.IsOwnedBy;

public sealed class OwnedBySpec<TEntity, TId, TOwnershipProperty> : SingleResultSpecification<TEntity, TOwnershipProperty>
    where TId : notnull, IStronglyTypedId
    where TEntity : AuditableEntity<TId>
    where TOwnershipProperty : notnull
{
    public OwnedBySpec(IStronglyTypedId id, Expression<Func<TEntity, TOwnershipProperty>> idSelector)
    {
        Query
            .Select(idSelector)
            .Where(x => x.Id.Equals(id));
    }
}
