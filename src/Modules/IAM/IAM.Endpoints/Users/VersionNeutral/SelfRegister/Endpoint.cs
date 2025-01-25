using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using MediatR;
using IAM.Application.OTP.Features.VerifyThenRemove;
using IAM.Application.Users.Features.Register;
using IAM.Application.Auth;

namespace IAM.Endpoints.Users.VersionNeutral.SelfRegister;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("register/self", RegisterAsync)
            .WithDescription("Register a new user.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> RegisterAsync(
        [FromBody] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new VerifyThenRemoveOtpCommand(request.PhoneNumber, request.Otp), cancellationToken)
                .BindAsync(() => sender.Send(new RegisterUserCommand(
                    PhoneNumber: request.PhoneNumber,
                    Name: request.Name,
                    LastName: request.LastName,
                    NationalIdentityNumber: request.NationalIdentityNumber,
                    BirthDate: request.BirthDate,
                    Roles: [CustomRoles.Basic]
                    ), cancellationToken))
                .MapAsync(id => new Response { Id = id });
}
