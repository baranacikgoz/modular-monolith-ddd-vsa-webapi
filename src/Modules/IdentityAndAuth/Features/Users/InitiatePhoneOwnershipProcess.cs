using Common.Core.Auth;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using FluentValidation;
using IdentityAndAuth;
using IdentityAndAuth.Features.Common.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users;

public static class InitiatePhoneOwnershipProcess
{
    public sealed record Request(string PhoneNumber) : IRequest<Result>;
    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator(IStringLocalizer<RequestValidator> localizer)
        {
            RuleFor(x => x.PhoneNumber)
                .PhoneNumberValidation(localizer);
        }
    }
    internal sealed class RequestHandler : IRequestHandler<Request, Result>
    {
        public async ValueTask<Result> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            // Simulate sending sms otp.
            await Task.Delay(100, cancellationToken);

            return Result.Succeeded();
        }
    }

    private static async Task<IResult> InitiatePhoneOwnershipProcessAsync(
        [FromBody] Request request,
        [FromServices] IMediator mediator,
        [FromKeyedServices(ModuleConstants.ModuleName)] IResultTranslator resultTranslator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync<Request, Result>(request, cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(result);
    }

    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("initiate-phone-ownership-process", InitiatePhoneOwnershipProcessAsync)
            .WithDescription("Initiate phone ownership process by sending sms otp.")
            .Produces(StatusCodes.Status200OK)
            .AllowAnonymous();
    }
}
