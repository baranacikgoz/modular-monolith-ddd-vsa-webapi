using Common.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Otp.VersionNeutral;

public static class Setup
{
    public static void MapOtpEndpoints(this RouteGroupBuilder rootGroup, UsernameSource usernameSource)
    {
        var otpApiGroup = rootGroup
            .MapGroup("/otp")
            .WithTags("OTP");

        if (usernameSource == UsernameSource.PhoneNumber)
        {
            SendForLogin.Endpoint.MapEndpoint(otpApiGroup);
            SendForRegistration.Endpoint.MapEndpoint(otpApiGroup);
        }

        // Unconditional: CreateByEmail (POST /tokens/email) always requires a verification token from
        // these two, for every caller (including staff/admin), regardless of which identifier Keycloak
        // usernames are provisioned from. Only the phone-specific self-service endpoints above are gated.
        SendForEmail.Endpoint.MapEndpoint(otpApiGroup);
        VerifyEmail.Endpoint.MapEndpoint(otpApiGroup);
    }
}
