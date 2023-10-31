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

namespace IdentityAndAuth.Features.Tokens.Create;

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
