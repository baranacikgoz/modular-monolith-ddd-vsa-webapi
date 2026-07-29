using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using IAM.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.InterModuleRequestHandlers;

public class GetPushTokensRequestHandler(IIAMDbContext dbContext, TimeProvider timeProvider)
    : InterModuleRequestHandler<GetPushTokensRequest, GetPushTokensResponse>
{
    public override async Task<GetPushTokensResponse> HandleAsync(
        GetPushTokensRequest request, CancellationToken cancellationToken)
    {
        var now = timeProvider.GetUtcNow();

        var targets = await dbContext.Sessions
            .AsNoTracking()
            .Where(s => request.UserIds.Contains(s.UserId)
                        && s.RevokedAt == null
                        && s.AbsoluteExpiresAt > now
                        && s.PushToken != null)
            .Select(s => new PushTarget(s.UserId, s.PushToken!))
            .ToListAsync(cancellationToken);

        return new GetPushTokensResponse(targets);
    }
}
