using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IdentityAndAuth;

public static partial class GetFirstBasicUserId
{
    public sealed record Request : IInterModuleRequest<Response>;
    public sealed record Response(Guid UserId);
}
