using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.Queries.IsOwnedBy;

public sealed class VerifyOwnershipCommandHandler<TEntity, TId, TOwnershipProperty>(IRepository<TEntity> repository) : ICommandHandler<VerifyOwnershipCommand<TEntity, TId, TOwnershipProperty>, Result>
    where TId : IStronglyTypedId
    where TEntity : AuditableEntity<TId>
    where TOwnershipProperty : notnull
{
    public async Task<Result<Result>> Handle(VerifyOwnershipCommand<TEntity, TId, TOwnershipProperty> command, CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new OwnedBySpec<TEntity, TId, TOwnershipProperty>(command.Id, command.OwnershipPropertySelector), cancellationToken)
            .BindAsync(ownershipProperty => !ownershipProperty.Equals(command.OwnershipProperty) ? Error.NotOwned<TEntity>() : Result.Success);
}
