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

public static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("", CreateAsync)
            .WithDescription("Create token by validating otp.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous();
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
}
