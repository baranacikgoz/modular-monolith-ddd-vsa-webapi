using Common.IntermoduleRequests.Contracts;

namespace Common.IntermoduleRequests;

public static class IntermoduleRequestsOf
{
    public static class IdentityAndAuth
    {
        public static class GetFirstAdminUser
        {
            public sealed record Request : IIntermoduleRequest<Response>;
            public sealed record Response(Guid UserId, string Name);
        }

        public static class GetFirstBasicUser
        {
            public sealed record Request : IIntermoduleRequest<Response>;
            public sealed record Response(Guid UserId, string Name);
        }
    }
}
