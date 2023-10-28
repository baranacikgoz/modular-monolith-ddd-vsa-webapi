using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Options;
using FluentValidation;
using IdentityAndAuth.Extensions;
using IdentityAndAuth;
using IdentityAndAuth.Auth;
using IdentityAndAuth.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NimbleMediator.Contracts;
using IdentityAndAuth.Features.Common.Validations;
using IdentityAndAuth.Features.Tokens.Services;
using IdentityAndAuth.Features.Users.Services;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;

namespace IdentityAndAuth.Features.Tokens;

public static class CreateTokens
{
    public sealed record Request(string PhoneVerificationToken, string PhoneNumber) : IRequest<Result<Response>>;
    public sealed record Response(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);

    public sealed class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator(IStringLocalizer<RequestValidator> localizer)
        {
            RuleFor(x => x.PhoneVerificationToken)
                .PhoneVerificationTokenValidation(localizer);

            RuleFor(x => x.PhoneNumber)
                .PhoneNumberValidation(localizer);

        }
    }
    internal sealed class RequestHandler(
        IPhoneVerificationTokenService phoneVerificationTokenService,
        ITokenService tokenService,
        IUserService userService
        ) : IRequestHandler<Request, Result<Response>>
    {
        public async ValueTask<Result<Response>> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            return await phoneVerificationTokenService
                .ValidateTokenAsync(request.PhoneNumber, request.PhoneVerificationToken, cancellationToken)
                .BindAsync(() => userService.GetByPhoneNumberAsync(request.PhoneNumber))
                .BindAsync(tokenService.GenerateTokensAndUpdateUserAsync)
                .MapAsync(tokenDto => new Response(
                    tokenDto.AccessToken,
                    tokenDto.AccessTokenExpiresAt,
                    tokenDto.RefreshToken,
                    tokenDto.RefreshTokenExpiresAt));
        }
    }

    private static async Task<IResult> CreateAsync(
        [FromBody] Request request,
        [FromServices] IMediator mediator,
        [FromServices] IResultTranslator resultTranslator,
        [FromServices] IStringLocalizer<IErrorTranslator> localizer,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync<Request, Result<Response>>(request, cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(result, localizer);
    }

    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("", CreateAsync)
            .WithDescription("Create token by validating otp.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous();
    }
}
