using Common.Core.Contracts.Results;
using NimbleMediator.Contracts;

namespace IdentityAndAuth.Features.Users.SelfRegister;

public sealed record Request(
        string PhoneVerificationToken,
        string PhoneNumber,
        string FirstName,
        string LastName,
        string NationalIdentityNumber,
        string BirthDate)
        : IRequest<Result<Response>>;
