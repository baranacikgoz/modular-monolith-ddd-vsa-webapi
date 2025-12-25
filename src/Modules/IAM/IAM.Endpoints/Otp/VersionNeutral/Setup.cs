using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Endpoint = IAM.Endpoints.Otp.VersionNeutral.Send.Endpoint;

namespace IAM.Endpoints.Otp.VersionNeutral;

public static class Setup
{
    public static void MapOtpEndpoints(this RouteGroupBuilder rootGroup)
    {
        var otpApiGroup = rootGroup
            .MapGroup("/otp")
            .WithTags("OTP");

        Endpoint.MapEndpoint(otpApiGroup);
    }
}
