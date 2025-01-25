using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using IAM.Application.OTP.Features.Send;
using IAM.Application.OTP.Features.Store;
using IAM.Application.Users.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Otp.VersionNeutral.Send;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("", SendOtp)
            .WithDescription("Send otp sms.")
            .RequireRateLimiting(Infrastructure.RateLimiting.Constants.Sms)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> SendOtp(
        [FromBody] Request request,
        [FromServices] IOtpService otpService,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await Result<string>
                .Create(() => otpService.Generate())
                .TapAsync(otp => sender.Send(new StoreOtpCommand(request.PhoneNumber, otp), cancellationToken))
                .TapAsync(otp => sender.Send(new SendOtpCommand(request.PhoneNumber, otp), cancellationToken));
}
