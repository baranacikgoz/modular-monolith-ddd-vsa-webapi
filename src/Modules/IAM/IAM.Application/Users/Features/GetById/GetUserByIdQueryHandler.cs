using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using IAM.Application.Persistence;
using IAM.Application.Users.DTOs;

namespace IAM.Application.Users.Features.GetById;

public sealed class GetUserByIdQueryHandler(IIAMDbContext dbContext) : IQueryHandler<GetUserByIdQuery, ApplicationUserDto>
{
    public async Task<Result<ApplicationUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => await dbContext
                .Users
                .TagWith(nameof(GetUserByIdQueryHandler), request.Id)
                .Where(u => u.Id == request.Id)
                .Select(u => new ApplicationUserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber!,
                    NationalIdentityNumber = u.NationalIdentityNumber,
                    BirthDate = u.BirthDate,
                    CreatedBy = u.Id,
                    CreatedOn = DateTime.UtcNow,
                    LastModifiedBy = u.Id,
                    LastModifiedOn = DateTime.UtcNow
                })
                .SingleAsResultAsync(cancellationToken);
}
