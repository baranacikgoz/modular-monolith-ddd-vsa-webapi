using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using IAM.Application.Tokens.Features.Refresh;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Tokens.VersionNeutral.Refresh;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("refresh", Refresh)
            .WithDescription("Refresh token.")
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> Refresh(
        [FromBody] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new RefreshTokenCommand(request.RefreshToken), cancellationToken)
                .MapAsync(tokensDto => new Response
                {
                    AccessToken = tokensDto.AccessToken,
                    AccessTokenExpiresAt = tokensDto.AccessTokenExpiresAt
                });
}
