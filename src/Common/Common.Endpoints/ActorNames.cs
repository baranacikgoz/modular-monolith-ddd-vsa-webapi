using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;

namespace Common.Endpoints;

/// <summary>
///     Resolves the readable name of whoever made a change (the "who" column) for every module that returns audit log
///     rows. All ids of a page are resolved with one <see cref="GetUsersRequest" />, never one request per row. With no
///     ids at all, IAM is not asked.
/// </summary>
public static class ActorNames
{
    /// <summary>
    ///     A user with an empty first and last name, or one IAM does not know, is left out of the dictionary: the
    ///     caller sees no entry and falls back to the raw id.
    /// </summary>
    public static async Task<IReadOnlyDictionary<ApplicationUserId, string>> ResolveAsync(
        IInterModuleRequestClient<GetUsersRequest, GetUsersResponse> client,
        IEnumerable<ApplicationUserId?> userIds,
        CancellationToken cancellationToken)
    {
        var distinctIds = userIds
            .Where(id => id is not null)
            .Select(id => id!.Value)
            .Distinct()
            .ToList();

        if (distinctIds.Count == 0)
        {
            return new Dictionary<ApplicationUserId, string>();
        }

        var response = await client.SendAsync(new GetUsersRequest(distinctIds), cancellationToken);

        return response.Users
            .Select(u => (u.Id, Name: $"{u.FirstName} {u.LastName}".Trim()))
            .Where(u => u.Name.Length > 0)
            .ToDictionary(u => u.Id, u => u.Name);
    }
}
