using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Otp.VersionNeutral;

public static class Setup
{
    public static void MapOtpEndpoints(this RouteGroupBuilder rootGroup)
    {
        var otpApiGroup = rootGroup
            .MapGroup("/otp")
            .WithTags("OTP");

        Send.Endpoint.MapEndpoint(otpApiGroup);
    }
}
