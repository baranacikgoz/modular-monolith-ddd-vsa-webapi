using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Common.Endpoints;

/// <summary>
///     Resolves the readable name of whoever made a change (the "who" column) for every module that returns audit log
///     rows. All ids of a page are resolved with one <see cref="GetUsersRequest" />, never one request per row. With no
///     ids at all, IAM is not asked.
/// </summary>
public static partial class ActorNames
{
    /// <summary>
    ///     A user with an empty first and last name, or one IAM does not know, is left out of the dictionary: the
    ///     caller sees no entry and falls back to the raw id. The names are decoration, so when IAM does not answer
    ///     (timeout, faulted handler, disabled module, unreachable transport) the result is an empty dictionary and the
    ///     failure is logged. A cancelled request still throws.
    /// </summary>
    public static async Task<IReadOnlyDictionary<ApplicationUserId, string>> ResolveAsync(
        IInterModuleRequestClient<GetUsersRequest, GetUsersResponse> client,
        IEnumerable<ApplicationUserId?> userIds,
        ILogger logger,
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

        GetUsersResponse response;
        try
        {
            response = await client.SendAsync(new GetUsersRequest(distinctIds), cancellationToken);
        }
        catch (MassTransitException ex) when (!cancellationToken.IsCancellationRequested)
        {
            LogActorNamesUnavailable(logger, distinctIds.Count, ex);
            return new Dictionary<ApplicationUserId, string>();
        }

        return response.Users
            .Select(u => (u.Id, Name: $"{u.FirstName} {u.LastName}".Trim()))
            .Where(u => u.Name.Length > 0)
            .ToDictionary(u => u.Id, u => u.Name);
    }

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Could not resolve the names of {UserCount} acting users from IAM; the response carries ids only.")]
    private static partial void LogActorNamesUnavailable(ILogger logger, int userCount, Exception ex);
}
