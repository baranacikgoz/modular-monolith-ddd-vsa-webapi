using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using IAM.Application.OTP.Features.VerifyThenRemove;
using IAM.Application.Tokens.DTOs;
using IAM.Application.Tokens.Features.Create;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Tokens.VersionNeutral.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("", Create)
            .WithDescription("Create tokens.")
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> Create(
        [FromBody] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new VerifyThenRemoveOtpCommand(request.PhoneNumber, request.Otp), cancellationToken)
                .BindAsync(() => sender.Send(new CreateTokensCommand(request.PhoneNumber), cancellationToken))
                .MapAsync(tokensDto => new Response
                {
                    AccessToken = tokensDto.AccessToken,
                    AccessTokenExpiresAt = tokensDto.AccessTokenExpiresAt,
                    RefreshToken = tokensDto.RefreshToken,
                    RefreshTokenExpiresAt = tokensDto.RefreshTokenExpiresAt
                });
}
