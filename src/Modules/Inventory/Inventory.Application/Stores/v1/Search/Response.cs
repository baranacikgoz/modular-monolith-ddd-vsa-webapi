using Common.Domain.StronglyTypedIds;
using Inventory.Domain.Stores;

namespace Inventory.Application.Stores.v1.Search;
public sealed record Response(StoreId Id, ApplicationUserId OwnerId, string Name, string Description, Uri? LogoUrl, int ProductCount);
