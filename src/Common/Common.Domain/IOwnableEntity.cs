using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.StronglyTypedIds;

namespace Common.Domain;

public interface IOwnableEntity
{
    ApplicationUserId OwnerId { get; }
}

/// <summary>
/// Interface for the entities those a user can have AT MOST one instance of them. (Such as store - A user can have only 1 store.)
/// </summary>
public interface ISingleOwnableEntity : IOwnableEntity
{
}

/// <summary>
/// Interface for the entities those a user can have MULTIPLE instances of them. (Such as products - A user can have many products.)
/// </summary>
public interface IMultipleOwnableEntity : IOwnableEntity
{
}
