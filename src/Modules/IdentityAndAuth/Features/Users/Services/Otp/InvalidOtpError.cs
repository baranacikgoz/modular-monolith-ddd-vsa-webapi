using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Users.Services.Otp;
public sealed record InvalidOtpError(string PhoneNumber, string Code) : Failure;
