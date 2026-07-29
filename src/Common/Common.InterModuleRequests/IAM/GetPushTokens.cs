using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IAM;

/// <summary>Resolves the live FCM push token(s) for a set of users — one token per active
/// (device, app) session, so a user logged in on two devices gets two targets.</summary>
public sealed record GetPushTokensRequest(
    IReadOnlyList<ApplicationUserId> UserIds
) : IInterModuleRequest<GetPushTokensResponse>;

public sealed record PushTarget(ApplicationUserId UserId, string Token);

public sealed record GetPushTokensResponse(IReadOnlyList<PushTarget> Targets);
