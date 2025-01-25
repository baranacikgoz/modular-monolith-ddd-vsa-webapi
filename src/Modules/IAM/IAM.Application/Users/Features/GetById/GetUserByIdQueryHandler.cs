using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using IAM.Application.Users.DTOs;
using IAM.Application.Users.Specifications;
using IAM.Domain.Identity;

namespace IAM.Application.Users.Features.GetById;

public sealed class GetUserByIdQueryHandler(IRepository<ApplicationUser> repository) : IQueryHandler<GetUserByIdQuery, ApplicationUserDto>
{
    public async Task<Result<ApplicationUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => await repository.SingleOrDefaultAsResultAsync(new UserByIdSpec<ApplicationUserDto>(request.Id, x => new ApplicationUserDto
        {
            Id = x.Id,
            Name = x.Name,
            LastName = x.LastName,
            PhoneNumber = x.PhoneNumber!,
            NationalIdentityNumber = x.NationalIdentityNumber,
            BirthDate = x.BirthDate,
            CreatedBy = x.Id,
            CreatedOn = DateTime.UtcNow,
            LastModifiedBy = x.Id,
            LastModifiedOn = DateTime.UtcNow
        }), cancellationToken);
}
