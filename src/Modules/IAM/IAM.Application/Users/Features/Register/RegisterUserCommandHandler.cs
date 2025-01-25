using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Common.Domain.ResultMonad;
using IAM.Domain.Identity;
using IAM.Application.Extensions;
using Common.Application.CQS;
using Common.Domain.StronglyTypedIds;

namespace IAM.Application.Users.Features.Register;

public sealed class RegisterUserCommandHandler(UserManager<ApplicationUser> userManager) : ICommandHandler<RegisterUserCommand, ApplicationUserId>
{
    public async Task<Result<ApplicationUserId>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        => await Result<ApplicationUser>
                .Create(() => ApplicationUser.Create(
                    request.Name,
                    request.LastName,
                    request.PhoneNumber,
                    request.NationalIdentityNumber,
                    DateOnly.ParseExact(request.BirthDate, Domain.Constants.TurkishDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None)))
                .BindAsync(async user =>
                {
                    var identityResult = await userManager.CreateAsync(user);
                    return identityResult.ToResult(user);
                })
                .BindAsync(async user =>
                {
                    var identityResult = await userManager.AddToRolesAsync(user, request.Roles);
                    return identityResult.ToResult(user);
                })
                .MapAsync(user => user.Id);
}
