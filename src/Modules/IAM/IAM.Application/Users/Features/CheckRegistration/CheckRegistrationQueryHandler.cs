using Common.Domain.ResultMonad;
using IAM.Domain.Identity;
using Common.Application.CQS;
using Common.Application.Persistence;
using IAM.Application.Users.Specifications;

namespace IAM.Application.Users.Features.CheckRegistration;

public sealed class CheckRegistrationQueryHandler(IRepository<ApplicationUser> repository) : IQueryHandler<CheckRegistrationQuery, bool>
{
    public async Task<Result<bool>> Handle(CheckRegistrationQuery request, CancellationToken cancellationToken)
        => await repository.AnyAsyncAsResult(new UserByPhoneNumberSpec(request.PhoneNumber), cancellationToken);
}
