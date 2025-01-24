using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common.Application.CQS;
using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.Queries.IsOwnedBy;

public sealed record VerifyOwnershipCommand<TEntity, TId, TOwnershipProperty>(IStronglyTypedId Id, TOwnershipProperty OwnershipProperty, Expression<Func<TEntity, TOwnershipProperty>> OwnershipPropertySelector) : ICommand<Result>
    where TId : notnull, IStronglyTypedId
    where TEntity : AuditableEntity<TId>
    where TOwnershipProperty : notnull;
