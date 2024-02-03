using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IdentityAndAuth;

public static partial class GetFirstAdminUserId
{
    public sealed record Request : IInterModuleRequest<Response>;
    public sealed record Response(Guid UserId);
}
