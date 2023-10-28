using System.Globalization;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using FluentValidation;
using IdentityAndAuth.Auth;
using IdentityAndAuth.Extensions;
using IdentityAndAuth.Features.Common.Validations;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;
using IdentityAndAuth.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users;

public static class SelfRegisterUser
{
    public sealed record Request(
        string PhoneVerificationToken,
        string PhoneNumber,
        string FirstName,
        string LastName,
        string NationalIdentityNumber,
        string BirthDate)
        : IRequest<Result<Guid>>;

    public sealed class RequestValidator : AbstractValidator<Request>
    {
        private const char EmptySpace = ' ';
        private static readonly HashSet<char> _turkishAlphabetSet = new("abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ");

        public RequestValidator(IStringLocalizer<RequestValidator> localizer)
        {
            RuleFor(x => x.PhoneVerificationToken)
                .PhoneVerificationTokenValidation(localizer);

            RuleFor(x => x.PhoneNumber)
                .PhoneNumberValidation(localizer);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                    .WithMessage(localizer["İsim boş olamaz."])
                .Must(str => str.All(c => IsEligibleForFirstName(c)))
                    .WithMessage(localizer["İsim sadece Türkçe alfabesindeki karakterlerden oluşabilir."])
                .MaximumLength(ApplicationUserConstants.FirstNameMaxLength)
                    .WithMessage(localizer["İsim {0} karakterden uzun olamaz.", ApplicationUserConstants.FirstNameMaxLength]);

            RuleFor(x => x.LastName)
                .NotEmpty()
                    .WithMessage(localizer["Soyisim boş olamaz."])
                .Must(str => str.All(c => _turkishAlphabetSet.Contains(c)))
                    .WithMessage(localizer["Soyisim sadece Türkçe alfabesindeki karakterlerden oluşabilir ve boşluk içermemelidir."])
                .MaximumLength(ApplicationUserConstants.LastNameMaxLength)
                    .WithMessage(localizer["Soyisim {0} karakterden uzun olamaz.", ApplicationUserConstants.LastNameMaxLength]);

            RuleFor(x => x.NationalIdentityNumber)
                .NotEmpty()
                    .WithMessage(localizer["T.C. Kimlik numarası boş olamaz."])
                .Length(ApplicationUserConstants.NationalIdentityNumberLength)
                    .WithMessage(localizer["T.C. Kimlik numarası {0} karakter olmalıdır.", ApplicationUserConstants.NationalIdentityNumberLength])
                .Must(str => str.All(char.IsDigit))
                    .WithMessage(localizer["T.C. Kimlik numarası sadece rakamlardan oluşabilir."]);

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                    .WithMessage(localizer["Doğum tarihi boş olamaz."])
                // tryParse datetime with providing an IFormatProvider
                .Must(str => DateOnly.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    .WithMessage(localizer["Doğum tarihi, yyyy-MM-dd formatında olmalıdır."]);
        }

        // Empty space is included because we want to allow names with middle names.
        private static bool IsEligibleForFirstName(char c)
            => _turkishAlphabetSet.Contains(c) || c.Equals(EmptySpace);
    }

    internal sealed class RequestHandler(
        IPhoneVerificationTokenService phoneVerificationTokenService,
        UserManager<ApplicationUser> userManager
        ) : IRequestHandler<Request, Result<Guid>>
    {
        public async ValueTask<Result<Guid>> HandleAsync(Request request, CancellationToken cancellationToken)
            => await phoneVerificationTokenService
                    .ValidateTokenAsync(request.PhoneNumber, request.PhoneVerificationToken, cancellationToken)
                    .BindAsync(() => CreateUserAndAssignRoleAsync(request));

        private async Task<Result<Guid>> CreateUserAndAssignRoleAsync(Request request)
        {
            var user = ApplicationUser.Create(
                new()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    NationalIdentityNumber = request.NationalIdentityNumber,
                    BirthDate = DateOnly.Parse(request.BirthDate, CultureInfo.InvariantCulture)
                }
            );

            var identityResult = await userManager.CreateAsync(user);

            return await identityResult.ToResult()
                .BindAsync(() => AssignRoleToUserAsync(user, CustomRoles.Basic))
                .MapAsync(() => user.Id);
        }

        private async Task<Result> AssignRoleToUserAsync(ApplicationUser user, string role)
        {
            var identityResult = await userManager.AddToRoleAsync(user, role);
            return identityResult.ToResult();
        }
    }
    private static async Task<IResult> SelfRegisterAsync(
        [FromBody] Request request,
        [FromServices] IMediator mediator,
        [FromServices] IResultTranslator resultTranslator,
        [FromServices] IStringLocalizer<IErrorTranslator> localizer,
        CancellationToken cancellationToken)
    {
        var response = await mediator.SendAsync<Request, Result<Guid>>(request, cancellationToken);

        return resultTranslator.TranslateToMinimalApiResult(response, localizer);
    }

    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("self-register", SelfRegisterAsync)
            .AllowAnonymous()
            .WithDescription("Self register a new user.")
            .Produces<Guid>();
    }
}
