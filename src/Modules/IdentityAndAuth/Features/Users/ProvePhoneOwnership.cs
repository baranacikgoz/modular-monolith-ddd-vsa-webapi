using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using FluentValidation;
using IdentityAndAuth;
using IdentityAndAuth.Features.Common.Validations;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Services.Otp;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;
using IdentityAndAuth.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users;

public static class ProvePhoneOwnership
{
    public sealed record Request(string PhoneNumber, string Otp) : IRequest<Result<Response>>;
    public sealed record Response(bool UserExists, string PhoneVerificationToken);

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator(IStringLocalizer<RequestValidator> localizer)
        {
            RuleFor(x => x.PhoneNumber)
                .PhoneNumberValidation(localizer);

            RuleFor(x => x.Otp)
                .OtpValidation(localizer);
        }
    }

    internal sealed class RequestHandler(
        IOtpService otpService,
        IPhoneVerificationTokenService phoneVerificationTokenService,
        UserManager<ApplicationUser> userManager
    )
     : IRequestHandler<Request, Result<Response>>
    {
        async ValueTask<Result<Response>> IRequestHandler<Request, Result<Response>>.HandleAsync(Request request, CancellationToken cancellationToken)
            => await otpService.ValidateAsync(request.Otp, request.PhoneNumber, cancellationToken)
                .BindAsync(() => phoneVerificationTokenService.GetTokenAsync(request.PhoneNumber, cancellationToken))
                .MapAsync(async token => new Response(await UserExistsAsync(request.PhoneNumber, cancellationToken), token));
        private Task<bool> UserExistsAsync(string phoneNumber, CancellationToken cancellationToken)
            => userManager.Users.AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
    }

    private static async Task<IResult> ProvePhoneOwnershipAsync(
        [FromBody] Request request,
        [FromServices] IMediator mediator,
        [FromKeyedServices(ModuleConstants.ModuleName)] IResultTranslator resultTranslator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync<Request, Result<Response>>(request, cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(result);
    }

    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("prove-phone-ownership", ProvePhoneOwnershipAsync)
            .WithDescription("Prove phone ownership by validating otp.")
            .Produces<Response>(StatusCodes.Status200OK)
            .AllowAnonymous();
    }
}
